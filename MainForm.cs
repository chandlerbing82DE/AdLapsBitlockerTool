using System;
using System.Drawing;
using System.DirectoryServices;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace AdLapsBitlockerTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ApplyModernTheme();
            SetupDataGridView();

            // Podpięcie przycisków
            btnSearch.Click += BtnSearch_Click;
            btnSearchOu.Click += BtnSearchOu_Click;
            btnImportCsv.Click += BtnImportCsv_Click;
            btnExportCsv.Click += BtnExportCsv_Click;
            btnCopyRow.Click += BtnCopyRow_Click;

            // Kopiowanie wiersza po dwukliku
            dgvResults.CellDoubleClick += (s, e) => { BtnCopyRow_Click(s, e); };

            txtComputerName.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnSearch.PerformClick(); } };
        }

        private void SetupDataGridView()
        {
            dgvResults.Columns.Clear();
            dgvResults.Columns.Add("Hostname", "Hostname");
            dgvResults.Columns.Add("LapsPwd", "LAPS Passwort");
            dgvResults.Columns.Add("BitLockerKey", "BitLocker Key");
            dgvResults.Columns.Add("Status", "Status / Fehler");
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtComputerName.Text)) return;
            dgvResults.Rows.Clear();
            SearchAD(true, txtComputerName.Text.Trim());
        }

        private void BtnSearchOu_Click(object sender, EventArgs e)
        {
            dgvResults.Rows.Clear();
            SearchAD(false, string.Empty);
        }

        // Główna funkcja odpytująca AD o komputery (LAPS + BitLocker)
        private void SearchAD(bool singleComputer, string computerName)
        {
            Cursor.Current = Cursors.WaitCursor;
            lblStatus.Text = "Suche läuft...";
            int count = 0;

            string ldapPath = txtOuPath.Text.Trim();
            if (!ldapPath.StartsWith("LDAP://", StringComparison.OrdinalIgnoreCase))
                ldapPath = "LDAP://" + ldapPath;

            try
            {
                using (DirectoryEntry root = new DirectoryEntry(ldapPath))
                using (DirectorySearcher searcher = new DirectorySearcher(root))
                {
                    searcher.Filter = singleComputer ? $"(&(objectCategory=computer)(name={computerName}))" : "(objectCategory=computer)";
                    searcher.SizeLimit = 5000;

                    searcher.PropertiesToLoad.Add("name");
                    searcher.PropertiesToLoad.Add("ms-Mcs-AdmPwd");
                    searcher.PropertiesToLoad.Add("msLAPS-Password");
                    searcher.PropertiesToLoad.Add("msLAPS-EncryptedPassword");

                    foreach (SearchResult result in searcher.FindAll())
                    {
                        // 1. Nazwa komputera
                        string pcName = result.Properties.Contains("name") ? result.Properties["name"][0].ToString() : "Unbekannt";

                        // 2. HASŁO LAPS
                        string laps = "Kein Passwort (oder keine Rechte)";

                        if (result.Properties.Contains("msLAPS-EncryptedPassword"))
                        {
                            laps = DecryptLapsPasswordLocally(pcName);
                        }
                        else if (result.Properties.Contains("msLAPS-Password"))
                        {
                            laps = result.Properties["msLAPS-Password"][0]?.ToString() ?? "";
                        }
                        else if (result.Properties.Contains("ms-Mcs-AdmPwd"))
                        {
                            laps = result.Properties["ms-Mcs-AdmPwd"][0]?.ToString() ?? "";
                        }

                        // 3. Klucz BitLocker
                        string bitlocker = "Kein Key";
                        string status = "OK";

                        try
                        {
                            using (DirectoryEntry pcEntry = result.GetDirectoryEntry())
                            {
                                bool keyFound = false;
                                foreach (DirectoryEntry child in pcEntry.Children)
                                {
                                    if (child.SchemaClassName == "msFVE-RecoveryInformation")
                                    {
                                        if (child.Properties.Contains("msFVE-RecoveryPassword") && child.Properties["msFVE-RecoveryPassword"].Value != null)
                                        {
                                            bitlocker = child.Properties["msFVE-RecoveryPassword"].Value.ToString();
                                        }
                                        else
                                        {
                                            bitlocker = "Keine Control Access Rechte";
                                        }
                                        keyFound = true;
                                        break;
                                    }
                                }
                                if (!keyFound) status = "Kein BitLocker";
                            }
                        }
                        catch (Exception ex)
                        {
                            bitlocker = "Lesefehler";
                            status = "Fehler: " + ex.Message;
                        }

                        dgvResults.Rows.Add(pcName, laps, bitlocker, status);
                        count++;
                    }
                }
                lblStatus.Text = $"Fertig. {count} Objekte gefunden.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler bei der LDAP-Verbindung:\n{ex.Message}", "AD Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Ein Fehler ist aufgetreten.";
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private string DecryptLapsPasswordLocally(string computerName)
        {
            try
            {
                using Process process = new Process();
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = $"-NoProfile -Command \"(Get-LapsADPassword -Identity '{computerName}' -AsPlainText).Password\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                string decoded = output.Trim();
                if (!string.IsNullOrEmpty(decoded))
                    return decoded;

                return "Keine Berechtigung zum Entschlüsseln";
            }
            catch
            {
                return "Entschlüsselungsfehler (PowerShell)";
            }
        }

        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            if (dgvResults.Rows.Count == 0) return;

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV-Datei (*.csv)|*.csv", FileName = "Ergebnisse_LAPS_Bitlocker.csv" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Hostname;LAPS Passwort;BitLocker Key;Status / Fehler");

                    foreach (DataGridViewRow row in dgvResults.Rows)
                    {
                        sb.AppendLine($"{row.Cells[0].Value};{row.Cells[1].Value};{row.Cells[2].Value};{row.Cells[3].Value}");
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Export erfolgreich abgeschlossen.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnImportCsv_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "CSV-Datei (*.csv)|*.csv" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    dgvResults.Rows.Clear();
                    string[] lines = File.ReadAllLines(ofd.FileName);

                    foreach (string line in lines)
                    {
                        string pcName = line.Split(';')[0].Trim();
                        if (!string.IsNullOrWhiteSpace(pcName) && pcName.ToLower() != "hostname")
                        {
                            SearchAD(true, pcName);
                        }
                    }
                }
            }
        }

        private void BtnCopyRow_Click(object sender, EventArgs e)
        {
            if (dgvResults.SelectedRows.Count > 0)
            {
                var row = dgvResults.SelectedRows[0];
                string rowData = $"{row.Cells[0].Value} | LAPS: {row.Cells[1].Value} | BL: {row.Cells[2].Value}";
                Clipboard.SetText(rowData);
                lblStatus.Text = "Zeile in die Zwischenablage kopiert.";
            }
        }

        private void ApplyModernTheme()
        {
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            this.ForeColor = Color.FromArgb(51, 51, 51);
            this.StartPosition = FormStartPosition.CenterScreen;

            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream? stream = assembly.GetManifestResourceStream("AdLapsBitlockerTool.symbol.ico"))
                {
                    if (stream != null)
                    {
                        this.Icon = new Icon(stream);
                    }
                }
            }
            catch { }

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.ForeColor = Color.White;
                    btn.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
                    btn.Cursor = Cursors.Hand;

                    if (btn.Name == "btnImportCsv" || btn.Name == "btnExportCsv" || btn.Name == "btnCopyRow")
                    {
                        btn.BackColor = Color.FromArgb(80, 80, 80);
                        btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(100, 100, 100);
                    }
                    else
                    {
                        btn.BackColor = Color.FromArgb(0, 120, 212);
                        btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(16, 110, 190);
                    }
                }
                else if (ctrl is TextBox txt)
                {
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.BackColor = Color.FromArgb(250, 250, 250);
                    txt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
                }
                else if (ctrl is DataGridView dgv)
                {
                    dgv.BackgroundColor = Color.White;
                    dgv.BorderStyle = BorderStyle.None;
                    dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    dgv.GridColor = Color.FromArgb(230, 230, 230);
                    dgv.RowHeadersVisible = false;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.ReadOnly = true;
                    dgv.AllowUserToResizeRows = false;
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 248, 255);
                    dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
                    dgv.DefaultCellStyle.Padding = new Padding(5);
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(50, 50, 50);
                    dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F);
                    dgv.ColumnHeadersHeight = 40;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
        }
    }
}