# 📋 CHECKLIST DE TEST - Référence Rapide

## 🎯 Objectif
Tester le système de collecte de données Siemens avec 2 PCs personnels

---

## ✅ PHASE 1 : Configuration SSH (20 min)

### PC 1 - Localhost

```powershell
# 1. Installer SSH
Add-WindowsCapability -Online -Name OpenSSH.Server~~~~0.0.1.0

# 2. Démarrer SSH
Start-Service sshd
Set-Service -Name sshd -StartupType 'Automatic'

# 3. Configurer pare-feu
New-NetFirewallRule -Name sshd -DisplayName 'OpenSSH Server' `
    -Enabled True -Direction Inbound -Protocol TCP -Action Allow -LocalPort 22

# 4. Créer utilisateur test
net user testuser test123 /add

# 5. Tester
ssh testuser@localhost
```

**Résultat attendu :** Connexion SSH réussie ✅

---

### PC 2 - Distant (Optionnel)

```powershell
# Sur PC 2 : Répéter étapes 1-4 ci-dessus

# Sur PC 2 : Trouver IP
ipconfig
# Noter l'adresse IPv4 : 192.168.1.XXX

# Sur PC 1 : Tester connexion
ping 192.168.1.XXX
ssh testuser@192.168.1.XXX
```

**Résultat attendu :** Ping et SSH fonctionnent ✅

---

## ✅ PHASE 2 : Configuration Application (10 min)

### Étapes dans l'application

1. **Lancer** Projet_Siemens
2. **Ouvrir** FormFileExtraction
3. **Cliquer** sur 🧪 Mode Test Local (2 PCs)

### Dans FormLocalTestSetup

4. **PC 1 :**
   - Cliquer : 📁 Générer fichiers de test (20 fichiers)
   - Cliquer : 🗄️ Créer base de données SQLite de test

5. **PC 2 (optionnel) :**
   - Entrer l'IP du PC 2
   - Cliquer : 🔌 Tester la connexion

6. **Créer les machines :**
   - Cliquer : ✅ Créer les machines de test dans le réseau

**Résultat attendu :** 3 machines créées ✅

---

## ✅ PHASE 3 : Tests de Collecte (20 min)

### Test 1 : Collecte SSH/SFTP Locale

| Étape | Action | Résultat Attendu |
|-------|--------|------------------|
| 1 | Sélectionner `TestServer : PC1_TestServer` | ✓ |
| 2 | Cliquer `Extract Data` | Fenêtre credentials s'ouvre |
| 3 | Username: `testuser` <br> Password: `test123` <br> Port: `22` | ✓ |
| 4 | Confirmer la collecte | Test connexion... |
| 5 | Attendre 5-10 sec | Rapport s'affiche |
| 6 | Vérifier rapport | **20 fichiers collectés** ✅ |
| 7 | Ouvrir dossier résultats | `Data/PC1_TestServer/files/` |

**Fichiers attendus :**
```
Data/PC1_TestServer/files/
├── log/          (5 fichiers)
├── xml/          (5 fichiers)
├── config/       (5 fichiers)
├── nfo/          (5 fichiers)
└── collection_report.json
```

---

### Test 2 : Extraction Base de Données

| Étape | Action | Résultat Attendu |
|-------|--------|------------------|
| 1 | Sélectionner `DataBase : PC1_TestDatabase` | ✓ |
| 2 | Cliquer `Extract Data` | Connexion réussie |
| 3 | Confirmer l'extraction | Extraction en cours... |
| 4 | Attendre 10-15 sec | Rapport s'affiche |
| 5 | Vérifier rapport | **4 requêtes réussies** ✅ |
| 6 | Ouvrir dossier résultats | `Data/PC1_TestDatabase/database_results/` |

**Fichiers attendus :**
```
Data/PC1_TestDatabase/database_results/
├── production_orders.json
├── error_logs.json
├── task_status.json
├── machine_statistics.json
└── extraction_report.json
```

---

### Test 3 : Collecte SSH Distante (PC 2)

| Étape | Action | Résultat Attendu |
|-------|--------|------------------|
| 1 | Sélectionner `TestServer : PC2_TestServer` | ✓ |
| 2 | Cliquer `Extract Data` | Fenêtre credentials |
| 3 | Username: `testuser` <br> Password: `test123` <br> Port: `22` | ✓ |
| 4 | Collecte | Fichiers récupérés depuis PC 2 ✅ |

---

## ✅ PHASE 4 : Test Packaging (5 min)

### Test 4 : Package Sécurisé

| Étape | Action | Résultat Attendu |
|-------|--------|------------------|
| 1 | Sélectionner `PC1_TestServer` | ✓ |
| 2 | Cliquer `📦 Package & Encrypt` | Confirmation |
| 3 | Confirmer | Choix mot de passe |
| 4 | Choisir "Non" (auto) | Packaging... |
| 5 | Attendre 5-10 sec | Rapport + mot de passe |
| 6 | **NOTER LE MOT DE PASSE** | ⚠️ IMPORTANT |
| 7 | Vérifier fichier | `.zip.enc` créé ✅ |

**Fichiers attendus :**
```
Data/SecurePackages/
├── Siemens_Debug_PC1_TestServer_[date].zip.enc
├── PC1_TestServer_package_report.json
└── PASSWORD_[motdepasse].txt
```

---

## 🐛 Dépannage Rapide

| Problème | Solution Express |
|----------|------------------|
| SSH ne démarre pas | `Restart-Service sshd` |
| Permission denied | Vérifier username/password |
| Ping échoue vers PC2 | Même réseau ? Pare-feu désactivé temporairement ? |
| Aucun fichier collecté | Regénérer fichiers test dans FormLocalTestSetup |
| Erreur base de données | Recréer la base SQLite |

---

## 📊 Résumé Final

À la fin des tests, vous devriez avoir :

- [x] SSH fonctionnel sur PC 1 (et PC 2)
- [x] 3 machines de test créées
- [x] 20 fichiers de test générés
- [x] Base SQLite créée avec données MES
- [x] 20 fichiers collectés via SSH/SFTP
- [x] 4 fichiers JSON extraits de la base
- [x] 1 package crypté créé

---

## 🎓 Commandes Essentielles

```powershell
# Vérifier SSH
Get-Service sshd

# Redémarrer SSH
Restart-Service sshd

# Tester SSH local
ssh testuser@localhost

# Voir IP locale
ipconfig

# Tester connexion PC 2
ping 192.168.1.XXX
ssh testuser@192.168.1.XXX

# Lister utilisateurs
net user
```

---

## 📞 Support

**Documentation complète :**
- `PROCEDURE_TEST_COMPLETE_DETAILLEE.txt` - Guide de 40 pages
- `GUIDE_Configuration_Test_Local_2PCs.txt` - Guide d'installation SSH
- `README_Test_Local.md` - Démarrage rapide

**En cas de problème :**
1. Consulter la section "Résolution des problèmes"
2. Vérifier les logs de l'application
3. Contacter l'équipe dev

---

**Version :** 1.0  
**Dernière mise à jour :** 2025-01-09  
**Durée totale des tests :** ~1 heure
