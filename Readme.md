## AD LAPS & BitLocker Abfrage-Tool

A powerful, asynchronous Windows Forms desktop application to query Local Administrator Password Solution (LAPS) passwords and BitLocker Recovery Keys directly from Active Directory.

> 🌐 **Note on Language:** The user interface of this application is entirely in **German** (`de-DE`).

---

## 🇬🇧 English Description

### Features
- **German User Interface:** Built natively for German-speaking IT environments.
- **Single Computer Search:** Instantly retrieve LAPS passwords and BitLocker recovery keys for a specific hostname.
- **OU Batch Scanning:** Asynchronously browse an entire Active Directory Organizational Unit (OU) with live progress bar feedback.
- **CSV Import:** Load a list of computer names from a CSV file to perform bulk lookups.
- **CSV Export:** Save all retrieved information (Hostname, LAPS Password, BitLocker Key, and Status) into a structured CSV file.
- **Clipboard Integration:** Easily copy selected rows directly into the clipboard for quick access.
- **LAPS Decryption Support:** Automatically handles legacy LAPS attributes (`ms-Mcs-AdmPwd`), new LAPS attributes (`msLAPS-Password`), and local PowerShell fallback decryption (`msLAPS-EncryptedPassword`).

### Technical Specifications
- **Framework:** .NET 10.0 (Windows Forms)
- **Dependencies:** `System.DirectoryServices`
- **Architecture:** Fully asynchronous thread management (`Task.Run`, `Progress<T>`) ensuring a responsive and stutter-free user interface during heavy LDAP directory lookups.

---

## 🇩🇪 Deutsche Beschreibung

### Funktionen
- **Deutsche Benutzeroberfläche:** Entwickelt nativ für deutschsprachige IT-Umgebungen.
- **Einzelsuche:** Schnelles Abfragen des LAPS-Passworts und BitLocker-Wiederherstellungsschlüssels für einen bestimmten Computernamen.
- **OU Durchsuchen:** Asynchrones Auslesen einer kompletten Active Directory Organizational Unit (OU) inklusive Live-Fortschrittsanzeige.
- **CSV-Import:** Importieren einer Liste von Computernamen aus einer CSV-Datei für Massenabfragen.
- **CSV-Export:** Exportieren aller abgefragten Informationen (Hostname, LAPS-Passwort, BitLocker-Key und Status) in eine übersichtliche CSV-Datei.
- **Zwischenablage:** Markierte Zeilen können bequem per Knopfdruck in die Zwischenablage kopiert werden.
- **LAPS-Kompatibilität:** Unterstützt klassisches LAPS (`ms-Mcs-AdmPwd`), das neue Windows LAPS (`msLAPS-Password`) sowie die lokale Entschlüsselung über PowerShell Fallback (`msLAPS-EncryptedPassword`).

### Technische Details
- **Framework:** .NET 10.0 (Windows Forms)
- **Abhängigkeiten:** `System.DirectoryServices`
- **Architektur:** Vollständig asynchrones Thread-Management (`Task.Run`, `Progress<T>`), wodurch die Benutzeroberfläche auch bei großen LDAP-Abfragen flüssig und blockierungsfrei bleibt.
