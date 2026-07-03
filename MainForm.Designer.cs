namespace AdLapsBitlockerTool
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblComputerName = new System.Windows.Forms.Label();
            this.txtComputerName = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();

            this.lblOuPath = new System.Windows.Forms.Label();
            this.txtOuPath = new System.Windows.Forms.TextBox();
            this.btnSearchOu = new System.Windows.Forms.Button();

            this.btnImportCsv = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnCopyRow = new System.Windows.Forms.Button();

            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // lblComputerName
            // 
            this.lblComputerName.AutoSize = true;
            this.lblComputerName.Location = new System.Drawing.Point(20, 25);
            this.lblComputerName.Name = "lblComputerName";
            this.lblComputerName.Size = new System.Drawing.Size(107, 15);
            this.lblComputerName.TabIndex = 0;
            this.lblComputerName.Text = "Computername:";
            // 
            // txtComputerName
            // 
            this.txtComputerName.Location = new System.Drawing.Point(140, 22);
            this.txtComputerName.Name = "txtComputerName";
            this.txtComputerName.Size = new System.Drawing.Size(200, 23);
            this.txtComputerName.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(350, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 26);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Suchen";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // lblOuPath
            // 
            this.lblOuPath.AutoSize = true;
            this.lblOuPath.Location = new System.Drawing.Point(20, 60);
            this.lblOuPath.Name = "lblOuPath";
            this.lblOuPath.Size = new System.Drawing.Size(95, 15);
            this.lblOuPath.TabIndex = 3;
            this.lblOuPath.Text = "OU Pfad (LDAP):";
            // 
            // txtOuPath
            // 
            this.txtOuPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOuPath.Location = new System.Drawing.Point(140, 57);
            this.txtOuPath.Name = "txtOuPath";
            this.txtOuPath.Size = new System.Drawing.Size(460, 23);
            this.txtOuPath.TabIndex = 4;
            this.txtOuPath.Text = "OU=..Computerkonten,DC=STADT,DC=KA-ZSN,DC=MAN";
            // 
            // btnSearchOu
            // 
            this.btnSearchOu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchOu.Location = new System.Drawing.Point(620, 55);
            this.btnSearchOu.Name = "btnSearchOu";
            this.btnSearchOu.Size = new System.Drawing.Size(140, 26);
            this.btnSearchOu.TabIndex = 5;
            this.btnSearchOu.Text = "OU durchsuchen";
            this.btnSearchOu.UseVisualStyleBackColor = true;
            // 
            // btnImportCsv
            // 
            this.btnImportCsv.Location = new System.Drawing.Point(20, 95);
            this.btnImportCsv.Name = "btnImportCsv";
            this.btnImportCsv.Size = new System.Drawing.Size(120, 30);
            this.btnImportCsv.TabIndex = 6;
            this.btnImportCsv.Text = "CSV Importieren";
            this.btnImportCsv.UseVisualStyleBackColor = true;
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Location = new System.Drawing.Point(150, 95);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(120, 30);
            this.btnExportCsv.TabIndex = 7;
            this.btnExportCsv.Text = "CSV Exportieren";
            this.btnExportCsv.UseVisualStyleBackColor = true;
            // 
            // btnCopyRow
            // 
            this.btnCopyRow.Location = new System.Drawing.Point(280, 95);
            this.btnCopyRow.Name = "btnCopyRow";
            this.btnCopyRow.Size = new System.Drawing.Size(180, 30);
            this.btnCopyRow.TabIndex = 8;
            this.btnCopyRow.Text = "Zeile in Zwischenablage";
            this.btnCopyRow.UseVisualStyleBackColor = true;
            // 
            // dgvResults
            // 
            this.dgvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(20, 140);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowTemplate.Height = 25;
            this.dgvResults.Size = new System.Drawing.Size(740, 270);
            this.dgvResults.TabIndex = 9;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(20, 418);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 15);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Bereit";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.btnCopyRow);
            this.Controls.Add(this.btnExportCsv);
            this.Controls.Add(this.btnImportCsv);
            this.Controls.Add(this.btnSearchOu);
            this.Controls.Add(this.txtOuPath);
            this.Controls.Add(this.lblOuPath);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtComputerName);
            this.Controls.Add(this.lblComputerName);
            this.MinimumSize = new System.Drawing.Size(750, 400);
            this.Name = "MainForm";
            this.Text = "AD LAPS & BitLocker Abfrage-Tool";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblComputerName;
        private System.Windows.Forms.TextBox txtComputerName;
        private System.Windows.Forms.Button btnSearch;

        private System.Windows.Forms.Label lblOuPath;
        private System.Windows.Forms.TextBox txtOuPath;
        private System.Windows.Forms.Button btnSearchOu;

        private System.Windows.Forms.Button btnImportCsv;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Button btnCopyRow;

        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label lblStatus;
    }
}