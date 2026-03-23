# 🧪 Configuration Test Local - 2 PCs

## ⚡ Démarrage Rapide (5 minutes)

### PC 1 (Votre ordinateur)

```powershell
# 1. Installer SSH (PowerShell Admin)
Add-WindowsCapability -Online -Name OpenSSH.Server~~~~0.0.1.0
Start-Service sshd
Set-Service -Name sshd -StartupType 'Automatic'

# 2. Créer utilisateur de test
net user testuser test123 /add

# 3. Autoriser SSH dans le pare-feu
New-NetFirewallRule -Name sshd -DisplayName 'OpenSSH Server' -Enabled True -Direction Inbound -Protocol TCP -Action Allow -LocalPort 22
```

### Dans l'application

1. **Lancer l'application** Projet_Siemens
2. Ouvrir **FormFileExtraction**
3. Cliquer sur **🧪 Mode Test Local (2 PCs)**
4. Générer les fichiers et créer la base de test
5. Créer les machines de test
6. Tester la collecte !

---

## 📋 Ce qui est créé automatiquement

### Machines de test
- ✅ **PC1_TestServer** (localhost) - Serveur SSH local avec fichiers de test
- ✅ **PC1_TestDatabase** (localhost) - Base de données SQLite avec données MES
- ✅ **PC2_TestServer** (IP à configurer) - Serveur SSH distant

### Fichiers de test (générés automatiquement)
```
TestData/
├── PC1_TestServer/
│   └── test_files/
│       ├── test_file_001.log
│       ├── test_file_002.xml
│       ├── test_file_003.config
│       └── ... (20 fichiers au total)
└── PC1_TestDatabase/
    └── database/
        └── test_mes_database.db
```

### Contenu réaliste
- **Fichiers .log** : Journaux système avec timestamps
- **Fichiers .xml** : Configurations système
- **Fichiers .config** : Paramètres d'application
- **Fichiers .nfo** : Informations machine
- **Base SQLite** : Tables MES complètes (ProductionOrders, SystemLogs, MachineStats, etc.)

---

## 🔧 Configuration PC 2 (Optionnel)

### Sur le PC 2
```powershell
# Même installation SSH que PC 1
Add-WindowsCapability -Online -Name OpenSSH.Server~~~~0.0.1.0
Start-Service sshd
net user testuser test123 /add

# Trouver l'IP du PC 2
ipconfig
# Noter l'adresse IPv4 (ex: 192.168.1.105)
```

### Dans l'application (PC 1)
1. Dans **FormLocalTestSetup**, entrer l'IP du PC 2
2. Tester la connexion
3. Créer les machines de test

---

## ✅ Tests disponibles

### Test 1 : Collecte SSH/SFTP locale
```
Machine : PC1_TestServer
Credentials : testuser / test123
Résultat : Collecte de 20 fichiers de test
```

### Test 2 : Extraction base de données
```
Machine : PC1_TestDatabase
Résultat : Extraction de données MES au format JSON
```

### Test 3 : Collecte SSH distante (si PC 2 configuré)
```
Machine : PC2_TestServer
Credentials : testuser / test123
Résultat : Collecte de fichiers depuis le PC 2
```

---

## 🐛 Problèmes courants

| Problème | Solution |
|----------|----------|
| "Impossible de se connecter" | `Restart-Service sshd` |
| "Ping échoue vers PC 2" | Vérifier que les 2 PCs sont sur le même réseau |
| "Permission denied" | Vérifier username/password : `testuser` / `test123` |
| "Aucun fichier collecté" | Cliquer sur "Générer fichiers de test" dans FormLocalTestSetup |

---

## 📚 Documentation complète

Voir : `Documentation/GUIDE_Configuration_Test_Local_2PCs.txt`

---

## 🎯 Prochaines étapes

Une fois les tests locaux validés, vous pourrez :
1. ✅ Tester toutes les fonctionnalités (collecte, extraction, packaging)
2. ✅ Débugger sans accès aux serveurs réels
3. ✅ Développer de nouvelles features en local
4. ✅ Préparer la migration vers l'environnement de production

---

## 🤝 Support

En cas de problème :
1. Consulter le guide complet
2. Vérifier les logs de l'application
3. Contacter l'équipe dev

**Bon test ! 🚀**
