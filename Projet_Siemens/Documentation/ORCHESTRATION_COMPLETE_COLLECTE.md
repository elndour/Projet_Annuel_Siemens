# 🎯 ORCHESTRATION COMPLÈTE DE LA COLLECTE DE DONNÉES

## Vue d'ensemble du système

Ce document explique comment les deux types de collecte (Base de données + Fichiers distants) sont orchestrés dans votre application.

---

## 📊 Architecture du flux de données

```
┌──────────────────────────────────────────────────────────────────────────┐
│                         INTERFACE UTILISATEUR                             │
│                        FormFileExtraction.cs                              │
└──────────────────────┬───────────────────────────────────────────────────┘
                       │
                       │ 1️⃣ User sélectionne une machine
                       │    dans la liste déroulante
                       ▼
        ┌──────────────────────────────────┐
        │  Quelle type de machine ?        │
        └──────┬───────────────────┬───────┘
               │                   │
       ┌───────▼───────┐   ┌──────▼────────┐
       │  type="DataBase"│  │ type="TestServer"│
       │                │   │ ou autre      │
       └───────┬────────┘   └──────┬────────┘
               │                   │
               │                   │
    ┌──────────▼─────────┐  ┌─────▼──────────────┐
    │ ExtractDatabaseData │  │ ExtractServerFiles │
    │      (méthode)     │  │     (méthode)      │
    └──────────┬─────────┘  └─────┬──────────────┘
               │                   │
               │                   │
               │                   │
    ┌──────────▼─────────────┐    │
    │   DatabaseHelper       │    │
    │ (BDD\DatabaseHelper.cs)│    │
    └──────────┬─────────────┘    │
               │                   │
               │                   │
        ┌──────▼────────┐    ┌────▼─────────────┐
        │ Connexion     │    │  FileCollector   │
        │ Oracle/SQLite │    │ (SSH\FileCollector)│
        └──────┬────────┘    └────┬─────────────┘
               │                   │
        ┌──────▼────────┐    ┌────▼─────────────┐
        │ Exécution     │    │  SSHHelper +     │
        │ requêtes SQL  │    │  SFTPHelper      │
        │ prédéfinies   │    │                  │
        └──────┬────────┘    └────┬─────────────┘
               │                   │
        ┌──────▼────────┐    ┌────▼─────────────┐
        │ Export JSON   │    │ Téléchargement   │
        │ avec metadata │    │ fichiers (.log,  │
        │               │    │ .xml, .config)   │
        └──────┬────────┘    └────┬─────────────┘
               │                   │
               │                   │
        ┌──────▼────────┐    ┌────▼─────────────┐
        │ Data/         │    │ Data/            │
        │ [MachineID]/  │    │ [MachineID]/     │
        │ database_results/│ │ files/           │
        └──────┬────────┘    └────┬─────────────┘
               │                   │
               └───────────┬───────┘
                           │
                    ┌──────▼──────┐
                    │  PACKAGING  │
                    │  (optionnel)│
                    └──────┬──────┘
                           │
                    ┌──────▼──────────────────┐
                    │ SecurePackageManager    │
                    │ (Security\)             │
                    └──────┬──────────────────┘
                           │
                    ┌──────▼──────────────────┐
                    │ 1. ZipPackager          │
                    │    → Compression ZIP    │
                    │ 2. AESEncryption        │
                    │    → Chiffrement AES-256│
                    └──────┬──────────────────┘
                           │
                    ┌──────▼──────────────────┐
                    │ Data/SecurePackages/    │
                    │ Siemens_Debug_*.zip.enc │
                    └─────────────────────────┘
```

---

## 🔍 Détail du flux pour CHAQUE type de machine

### 1️⃣ Type "DataBase" → Extraction SQL

```csharp
// FormFileExtraction.cs (ligne ~200)
private void ExtractDatabaseData(DataBase? dbMachine)
{
    // ✅ ÉTAPE 1 : Créer la connexion
    Connection connectionInfo = new Connection(
        dbMachine.ip,
        dbMachine.instanceName,
        dbMachine.username,
        dbMachine.password,
        dbMachine.sshPort.ToString()
    );
    
    DatabaseHelper dbHelper = new DatabaseHelper(connectionInfo);
    
    // ✅ ÉTAPE 2 : Tester la connexion
    if (!dbHelper.TestConnection())
    {
        // ❌ Échec → Arrêter
        return;
    }
    
    // ✅ ÉTAPE 3 : Demander confirmation à l'utilisateur
    // "Voulez-vous extraire : ordres de production, logs, tâches, etc."
    
    // ✅ ÉTAPE 4 : Lancer l'extraction complète
    ExtractionReport report = dbHelper.ExecuteFullMESDataCollection(
        dbMachine.id, 
        baseDirectory
    );
    
    // ✅ ÉTAPE 5 : Afficher le rapport
    ShowExtractionReport(report);
    
    // ✅ ÉTAPE 6 : Ouvrir le dossier des résultats
    System.Diagnostics.Process.Start("explorer.exe", resultsFolder);
}
```

**Ce qui se passe dans `DatabaseHelper.ExecuteFullMESDataCollection()` :**

```csharp
// DatabaseHelper.cs (ligne ~290)
public ExtractionReport ExecuteFullMESDataCollection(string databaseId, string outputBaseDirectory)
{
    // 🗂️ Créer le dossier de sortie
    string outputDir = Path.Combine(outputBaseDirectory, databaseId, "database_results");
    Directory.CreateDirectory(outputDir);
    
    // 📋 Obtenir la liste des requêtes prédéfinies
    var mesQueries = GetStandardMESQueries();  // 4 requêtes par défaut
    
    // 🔄 Pour chaque requête :
    foreach (var query in mesQueries)
    {
        // 1. Exécuter la requête SQL
        // 2. Convertir les résultats en JSON
        // 3. Sauvegarder dans un fichier
        bool success = ExecuteSqlQueryAndSaveJson(
            query.SqlQuery, 
            outputPath, 
            query.QueryName
        );
        
        // Enregistrer le résultat dans le rapport
        report.Queries.Add(queryResult);
    }
    
    // 📊 Sauvegarder le rapport d'extraction
    SaveExtractionReport(report, "extraction_report.json");
    
    return report;
}
```

**Requêtes MES prédéfinies (4 fichiers JSON générés) :**

| Requête                | Fichier de sortie           | Description                          |
|------------------------|----------------------------|--------------------------------------|
| Production Orders      | `production_orders.json`   | Ordres de production (30 jours)      |
| Error Logs             | `error_logs.json`          | Logs d'erreurs critiques (1000 max)  |
| Task Status            | `task_status.json`         | Tâches MES actives                   |
| Machine Statistics     | `machine_statistics.json`  | Stats machines (7 derniers jours)    |

**Structure d'un fichier JSON généré :**

```json
{
  "metadata": {
    "queryName": "Production Orders",
    "extractionDate": "2025-01-09 14:35:12",
    "database": "MES_PROD_DB",
    "server": "192.168.1.50",
    "rowCount": 150,
    "columnCount": 7,
    "columns": ["order_id", "product_id", "quantity_planned", ...]
  },
  "data": [
    {
      "order_id": "ORD-001",
      "product_id": "PROD-A123",
      "quantity_planned": 1000,
      "quantity_produced": 980,
      "start_time": "2025-01-01 08:00:00",
      "end_time": "2025-01-01 16:30:00",
      "status": "COMPLETED"
    },
    ...
  ]
}
```

---

### 2️⃣ Type "TestServer" (ou autre) → Collecte de fichiers SSH/SFTP

```csharp
// FormFileExtraction.cs (ligne ~50)
private void ExtractServerFiles(Machine serverMachine)
{
    // ✅ ÉTAPE 1 : Demander les credentials SSH
    var credForm = new SSHCredentialsForm(serverMachine.ip);
    if (credForm.ShowDialog() != DialogResult.OK)
        return;  // Annulé par l'utilisateur
    
    // ✅ ÉTAPE 2 : Créer le FileCollector
    var collector = new FileCollector(
        serverMachine,
        baseDirectory,
        credForm.Username,    // Ex: "testuser"
        credForm.Password,    // Ex: "test123"
        credForm.Port         // Ex: 22
    );
    
    // ✅ ÉTAPE 3 : Tester la connexion SSH + SFTP
    if (!collector.TestConnections())
    {
        // ❌ Échec → Arrêter
        MessageBox.Show("Impossible de se connecter au serveur !");
        return;
    }
    
    // ✅ ÉTAPE 4 : Demander confirmation
    // "Voulez-vous collecter .log, .xml, .config, .nfo ?"
    
    // ✅ ÉTAPE 5 : Lancer la collecte
    var report = collector.CollectAllFiles((status) => {
        // Callback pour afficher le statut en temps réel
        Application.DoEvents();
    });
    
    // ✅ ÉTAPE 6 : Afficher le rapport
    ShowFileCollectionReport(report);
    
    // ✅ ÉTAPE 7 : Sauvegarder le rapport JSON
    collector.SaveReport(report, "collection_report.json");
    
    // ✅ ÉTAPE 8 : Ouvrir le dossier des résultats
    System.Diagnostics.Process.Start("explorer.exe", resultsFolder);
}
```

**Ce qui se passe dans `FileCollector.CollectAllFiles()` :**

```csharp
// FileCollector.cs (ligne ~70)
public FileCollectionReport CollectAllFiles(Action<string> statusCallback = null)
{
    // 🔌 Tester les connexions SSH et SFTP
    if (!TestConnections())
    {
        return report with error;
    }
    
    // 📁 Créer le répertoire de sortie
    string machineOutputDir = Path.Combine(baseOutputDirectory, machine.id, "files");
    Directory.CreateDirectory(machineOutputDir);
    
    // 🔍 Collecter les fichiers par type
    CollectFilesByExtension("log", machineOutputDir);   // *.log
    CollectFilesByExtension("xml", machineOutputDir);   // *.xml
    CollectFilesByExtension("config", machineOutputDir); // *.config
    CollectFilesByExtension("nfo", machineOutputDir);   // *.nfo
    
    // 📊 Générer le rapport
    report.EndTime = DateTime.Now;
    report.Success = true;
    return report;
}

private void CollectFilesByExtension(string extension, string outputDir)
{
    // 🔎 Chercher les fichiers sur le serveur distant
    var remoteFiles = FindRemoteFiles($"*.{extension}");
    
    // 📥 Télécharger chaque fichier via SFTP
    foreach (var remoteFile in remoteFiles)
    {
        string localPath = Path.Combine(outputDir, extension, Path.GetFileName(remoteFile));
        sftpHelper.DownloadFile(remoteFile, localPath);
    }
}
```

**Résultat de la collecte (structure de dossiers) :**

```
Data/
└── PC1_TestServer/
    └── files/
        ├── log/
        │   ├── test_file_001.log
        │   ├── test_file_005.log
        │   └── ...
        ├── xml/
        │   ├── test_file_002.xml
        │   └── ...
        ├── config/
        │   ├── test_file_003.config
        │   └── ...
        ├── nfo/
        │   ├── test_file_004.nfo
        │   └── ...
        └── collection_report.json  ← Rapport de collecte
```

---

## 🔐 Phase 3 : Packaging et Cryptage (Optionnel)

Après avoir collecté les données (SQL OU fichiers), l'utilisateur peut créer un **package sécurisé**.

```csharp
// FormFileExtraction.cs (ligne ~350)
private void packageButton_Click(object sender, EventArgs e)
{
    Machine selectedMachine = (Machine)machinesList.SelectedItem;
    
    // ✅ ÉTAPE 1 : Vérifier que des données existent
    string machineDataPath = Path.Combine(baseDirectory, selectedMachine.id);
    if (!Directory.Exists(machineDataPath))
    {
        MessageBox.Show("Aucune donnée à packager !");
        return;
    }
    
    // ✅ ÉTAPE 2 : Demander un mot de passe (ou générer automatiquement)
    string password = GenerateOrAskPassword();
    
    // ✅ ÉTAPE 3 : Créer le package sécurisé
    var packageManager = new SecurePackageManager();
    
    string outputPackage = packageManager.CreateSecurePackage(
        machineDataPath,              // Dossier source
        selectedMachine.id,           // ID de la machine
        password,                     // Mot de passe AES
        Path.Combine(baseDirectory, "SecurePackages")  // Dossier de sortie
    );
    
    // ✅ ÉTAPE 4 : Afficher le rapport
    ShowPackageReport(outputPackage, password);
    
    // ✅ ÉTAPE 5 : Ouvrir le dossier du package
    System.Diagnostics.Process.Start("explorer.exe", packagesFolder);
}
```

**Ce qui se passe dans `SecurePackageManager.CreateSecurePackage()` :**

```csharp
// SecurePackageManager.cs
public string CreateSecurePackage(string sourceFolder, string machineId, string password, string outputDir)
{
    // 📦 ÉTAPE 1 : Compression ZIP
    string tempZipPath = ZipPackager.CreateZipFromFolder(
        sourceFolder,
        $"Siemens_Debug_{machineId}_{timestamp}.zip"
    );
    // Résultat : fichier .zip temporaire (non crypté)
    
    // 🔐 ÉTAPE 2 : Chiffrement AES-256
    string encryptedPath = Path.ChangeExtension(tempZipPath, ".zip.enc");
    AESEncryption.EncryptFile(tempZipPath, encryptedPath, password);
    // Résultat : fichier .zip.enc (crypté)
    
    // 🗑️ ÉTAPE 3 : Supprimer le ZIP temporaire
    File.Delete(tempZipPath);
    
    // 💾 ÉTAPE 4 : Sauvegarder le mot de passe dans un fichier séparé
    File.WriteAllText($"PASSWORD_{password}.txt", password);
    
    return encryptedPath;
}
```

**Résultat final du packaging :**

```
Data/
└── SecurePackages/
    ├── Siemens_Debug_PC1_TestServer_20250109_144015.zip.enc  ← Fichier crypté
    ├── PC1_TestServer_package_report.json                    ← Rapport de packaging
    └── PASSWORD_XkP9#mN2$vL8@wQ5.txt                         ← Mot de passe AES
```

---

## 🎬 Scénario complet d'utilisation

### Scénario 1 : Collecte complète d'un serveur MES

```
┌─────────────────────────────────────────────────────────────────┐
│ 1. User ouvre FormFileExtraction                               │
│ 2. User sélectionne "TestServer : PC1_TestServer"              │
│ 3. User clique sur "Extract Data"                              │
│ 4. Popup demande credentials SSH (testuser / test123)          │
│ 5. Connexion SSH testée → ✅ OK                                │
│ 6. Popup de confirmation : "Collecter .log, .xml, .config ?"   │
│ 7. User clique "Oui"                                            │
│ 8. Collecte via SFTP : 20 fichiers téléchargés                 │
│ 9. Rapport affiché : "✓ 20 fichiers collectés"                │
│ 10. Dossier Data/PC1_TestServer/files/ s'ouvre                 │
│                                                                 │
│ 11. User sélectionne "DataBase : PC1_TestDatabase"             │
│ 12. User clique sur "Extract Data"                             │
│ 13. Connexion SQLite testée → ✅ OK                            │
│ 14. Popup de confirmation : "Extraire ordres, logs, tâches ?"  │
│ 15. User clique "Oui"                                           │
│ 16. Exécution de 4 requêtes SQL                                │
│ 17. Export de 4 fichiers JSON                                  │
│ 18. Rapport affiché : "✓ 4 requêtes réussies"                 │
│ 19. Dossier Data/PC1_TestDatabase/database_results/ s'ouvre    │
│                                                                 │
│ 20. User sélectionne "TestServer : PC1_TestServer" à nouveau   │
│ 21. User clique sur "📦 Package & Encrypt"                     │
│ 22. Popup : "Fournir votre mot de passe ? → Non (auto)"       │
│ 23. Création du package :                                      │
│     - Compression ZIP (2.45 MB)                                │
│     - Chiffrement AES-256 (2.46 MB)                            │
│ 24. Mot de passe généré : XkP9#mN2$vL8@wQ5                     │
│ 25. Rapport affiché avec le mot de passe                       │
│ 26. Dossier Data/SecurePackages/ s'ouvre                       │
└─────────────────────────────────────────────────────────────────┘
```

### Scénario 2 : Extraction complète de TOUTES les machines

Si vous voulez collecter **tout** d'un coup (tous les serveurs + toutes les DB), vous pourriez créer un bouton "Extract All Machines" qui boucle :

```csharp
private void ExtractAllMachines()
{
    foreach (Machine machine in parentForm.network.machines)
    {
        if (machine.type == "DataBase")
        {
            ExtractDatabaseData(machine as DataBase);
        }
        else
        {
            ExtractServerFiles(machine);
        }
    }
    
    // Ensuite, packager tous les résultats ensemble
    PackageAllData();
}
```

---

## 📂 Structure finale des données collectées

```
Data/
├── PC1_TestServer/                      ← Serveur 1
│   └── files/
│       ├── log/
│       │   ├── system.log
│       │   └── error.log
│       ├── xml/
│       │   └── config.xml
│       ├── config/
│       │   └── app.config
│       ├── nfo/
│       │   └── machine_info.nfo
│       └── collection_report.json       ← Rapport de collecte SSH
│
├── PC1_TestDatabase/                    ← Base de données 1
│   └── database_results/
│       ├── production_orders.json       ← Données SQL
│       ├── error_logs.json
│       ├── task_status.json
│       ├── machine_statistics.json
│       └── extraction_report.json       ← Rapport d'extraction SQL
│
├── PC2_TestServer/                      ← Serveur 2 (si configuré)
│   └── files/
│       └── ...
│
└── SecurePackages/                      ← Packages sécurisés
    ├── Siemens_Debug_PC1_TestServer_20250109_144015.zip.enc
    ├── Siemens_Debug_PC1_TestDatabase_20250109_145020.zip.enc
    ├── PC1_TestServer_package_report.json
    ├── PC1_TestDatabase_package_report.json
    ├── PASSWORD_XkP9#mN2$vL8@wQ5.txt
    └── PASSWORD_mN8@vQ2$kL9#pW3.txt
```

---

## 🔑 Points clés de l'orchestration

| Aspect                | Base de données                     | Fichiers distants                  |
|-----------------------|-------------------------------------|------------------------------------|
| **Classe principale** | `DatabaseHelper`                   | `FileCollector`                    |
| **Protocole**         | SQL (Oracle/SQLite)                | SSH + SFTP                         |
| **Authentification**  | User/Password DB                   | User/Password SSH                  |
| **Type de données**   | Résultats de requêtes SQL          | Fichiers binaires/texte            |
| **Format de sortie**  | JSON avec metadata                 | Fichiers originaux + rapport JSON  |
| **Dossier de sortie** | `Data/[MachineID]/database_results/` | `Data/[MachineID]/files/`         |
| **Rapport généré**    | `extraction_report.json`           | `collection_report.json`           |
| **Nombre de fichiers**| ~4 fichiers JSON (requêtes fixes)  | Variable (dépend du serveur)       |

---

## 🚀 Avantages de cette architecture

### ✅ Séparation des responsabilités
- **FormFileExtraction** : Interface utilisateur (orchestration de haut niveau)
- **DatabaseHelper** : Logique d'extraction SQL
- **FileCollector** : Logique de collecte SSH/SFTP
- **SecurePackageManager** : Logique de packaging/cryptage

### ✅ Réutilisabilité
Chaque composant peut être utilisé indépendamment :
```csharp
// Utiliser DatabaseHelper seul
var dbHelper = new DatabaseHelper(connectionInfo);
dbHelper.ExecuteFullMESDataCollection("PROD_DB", "C:\\Output");

// Utiliser FileCollector seul
var collector = new FileCollector(machine, "C:\\Output", "user", "pass");
collector.CollectAllFiles();

// Utiliser SecurePackageManager seul
var packager = new SecurePackageManager();
packager.CreateSecurePackage("C:\\Data", "MachineX", "password", "C:\\Packages");
```

### ✅ Extensibilité
Facile d'ajouter :
- De nouvelles requêtes SQL (modifier `GetStandardMESQueries()`)
- De nouveaux types de fichiers (ajouter dans `CollectFilesByExtension()`)
- De nouveaux formats d'export (PDF, CSV, Excel)

### ✅ Testabilité
Environnement de test complet avec :
- SQLite mock pour tester sans vraie base Oracle
- SSH localhost pour tester sans serveurs distants
- Générateur de fichiers de test automatique

---

## 🎯 Conclusion

Votre système combine **deux stratégies de collecte complémentaires** :

1. **Collecte structurée** (SQL) → Données MES tabulaires (ordres, logs, stats)
2. **Collecte de fichiers** (SSH) → Documents, configs, logs textuels

Ensuite, tout peut être **packageé et crypté** pour l'archivage ou le transport sécurisé.

Cette architecture permet de faire une **extraction complète d'un système MES industriel** sans accès direct aux serveurs de production, en utilisant seulement des connexions SSH et SQL standards. 🏭

---

**Document généré le 2025-01-09**  
**Projet : Siemens MES Data Collection System**
