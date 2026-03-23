# 🔧 Guide de Dépannage - Tests Locaux 2 PCs

## Table des Matières

1. [Problèmes SSH](#problèmes-ssh)
2. [Problèmes Réseau](#problèmes-réseau)
3. [Problèmes Base de Données](#problèmes-base-de-données)
4. [Problèmes de Collecte](#problèmes-de-collecte)
5. [Problèmes de Packaging](#problèmes-de-packaging)
6. [Problèmes Application](#problèmes-application)

---

## 🔐 Problèmes SSH

### ❌ Erreur : "Impossible de se connecter au serveur !"

**Symptômes :**
- Message d'erreur dans l'application après avoir entré les credentials
- Test de connexion échoue

**Causes possibles et solutions :**

#### 1. Service SSH non démarré

```powershell
# Vérifier le statut
Get-Service sshd

# Si "Stopped", démarrer
Start-Service sshd

# Configurer démarrage automatique
Set-Service -Name sshd -StartupType 'Automatic'
```

#### 2. Pare-feu bloque le port 22

```powershell
# Vérifier les règles
Get-NetFirewallRule -Name sshd

# Si absente, créer la règle
New-NetFirewallRule -Name sshd -DisplayName 'OpenSSH Server' `
    -Enabled True -Direction Inbound -Protocol TCP -Action Allow -LocalPort 22
```

#### 3. Credentials incorrects

```powershell
# Vérifier que l'utilisateur existe
net user testuser

# Réinitialiser le mot de passe
net user testuser NouveauMotDePasse123!
```

#### 4. Port SSH non standard

**Vérification :**
- Par défaut, SSH utilise le port 22
- Si modifié, vérifier `C:\ProgramData\ssh\sshd_config`

```powershell
# Ouvrir le fichier de config
notepad C:\ProgramData\ssh\sshd_config

# Chercher la ligne :
Port 22
```

---

### ❌ Erreur : "Permission denied"

**Symptômes :**
- Connexion refuse après avoir entré le mot de passe
- Message "Access denied" ou "Permission denied"

**Solutions :**

#### 1. Vérifier l'authentification par mot de passe

Éditer `C:\ProgramData\ssh\sshd_config` (en Admin) :

```
# S'assurer que ces lignes sont présentes et décommentées :
PasswordAuthentication yes
PubkeyAuthentication yes
```

Redémarrer SSH :
```powershell
Restart-Service sshd
```

#### 2. Compte désactivé ou expiré

```powershell
# Vérifier le statut du compte
net user testuser

# Réactiver si nécessaire
net user testuser /active:yes
```

#### 3. Mot de passe expiré

```powershell
# Définir un nouveau mot de passe
net user testuser NouveauPassword123!

# Désactiver l'expiration
wmic UserAccount where Name="testuser" set PasswordExpires=False
```

---

### ❌ Erreur : "Host key verification failed"

**Symptômes :**
- Message d'avertissement de sécurité SSH
- Connexion refusée automatiquement

**Solution :**

```powershell
# Supprimer l'ancienne clé SSH
Remove-Item ~\.ssh\known_hosts -Force

# Ou éditer le fichier et supprimer la ligne concernée
notepad ~\.ssh\known_hosts
```

---

## 🌐 Problèmes Réseau

### ❌ Erreur : Ping échoue vers le PC 2

**Symptômes :**
- `ping 192.168.1.XXX` : "Délai d'attente de la demande dépassé"
- PC 2 non accessible

**Diagnostic :**

```powershell
# Test 1 : Ping localhost
ping localhost
# ✅ Doit fonctionner

# Test 2 : Ping IP locale du PC 1
ping 192.168.1.YYY  # IP de votre PC 1
# ✅ Doit fonctionner

# Test 3 : Ping PC 2
ping 192.168.1.XXX
# ❌ Si échoue : problème réseau
```

**Solutions :**

#### 1. Vérifier que les 2 PCs sont sur le même réseau

Sur PC 1 :
```powershell
ipconfig
# Noter : 192.168.1.YYY
```

Sur PC 2 :
```powershell
ipconfig
# Noter : 192.168.1.XXX
```

**Les 3 premiers chiffres doivent être identiques !**
- ✅ Bon : `192.168.1`.YYY et `192.168.1`.XXX
- ❌ Mauvais : `192.168.1`.YYY et `192.168.0`.XXX

#### 2. Désactiver temporairement le pare-feu (pour tester)

Sur PC 2, PowerShell Admin :
```powershell
# Désactiver (temporaire !)
Set-NetFirewallProfile -Profile Domain,Public,Private -Enabled False

# Tester le ping depuis PC 1

# Réactiver après
Set-NetFirewallProfile -Profile Domain,Public,Private -Enabled True
```

Si ça marche maintenant → c'est le pare-feu !

#### 3. Créer une règle de pare-feu pour ICMP

Sur PC 2, PowerShell Admin :
```powershell
New-NetFirewallRule -DisplayName "Allow ICMP Ping" `
    -Direction Inbound -Protocol ICMPv4 -IcmpType 8 -Action Allow
```

#### 4. Vérifier le câble/WiFi

- **WiFi** : Les 2 PCs sont sur le même réseau WiFi ?
- **Câble** : Les 2 PCs sont sur le même switch/routeur ?
- **Isolation réseau** : Certains réseaux publics isolent les appareils

---

### ❌ Erreur : "Network is unreachable"

**Symptômes :**
- Impossible de joindre le PC 2
- Message "Network is unreachable"

**Solutions :**

#### 1. Vérifier la passerelle par défaut

```powershell
ipconfig /all
# Chercher "Passerelle par défaut"
```

Les 2 PCs doivent avoir la même passerelle (ex: 192.168.1.1)

#### 2. Réinitialiser la stack réseau

```powershell
# Réinitialiser Winsock
netsh winsock reset

# Réinitialiser TCP/IP
netsh int ip reset

# Redémarrer le PC
Restart-Computer
```

---

## 🗄️ Problèmes Base de Données

### ❌ Erreur : "Impossible de se connecter à la base de données !"

**Symptômes :**
- Message lors de la sélection de `PC1_TestDatabase`
- Erreur SQLite

**Solutions :**

#### 1. Vérifier que le fichier .db existe

Ouvrir l'Explorateur :
```
C:\Users\LENOVO\Desktop\Projet_Annuel_Siemens\TestData\PC1_TestDatabase\database\
```

Le fichier `test_mes_database.db` doit exister.

**Si absent :**
- Retourner dans FormLocalTestSetup
- Cliquer sur "🗄️ Créer base de données SQLite de test"

#### 2. Vérifier les permissions du fichier

```powershell
# Donner les permissions complètes
icacls "C:\...\test_mes_database.db" /grant Users:F
```

#### 3. Fichier corrompu

```powershell
# Supprimer et recréer
Remove-Item "C:\...\test_mes_database.db" -Force

# Retourner dans l'app et recréer la base
```

---

### ❌ Erreur : "No such table: production_orders"

**Symptômes :**
- Erreur SQL lors de l'extraction
- Table manquante

**Solution :**

La base n'a pas été créée correctement. Recréer :

1. Supprimer le fichier .db
2. Dans FormLocalTestSetup
3. Cliquer "🗄️ Créer base de données SQLite de test"

---

### ❌ Erreur : "database is locked"

**Symptômes :**
- Erreur lors de l'extraction
- Message "database is locked"

**Solutions :**

#### 1. Fermer les connexions existantes

```powershell
# Tuer tous les processus qui utilisent SQLite
taskkill /F /IM sqlite3.exe
```

#### 2. Redémarrer l'application

Fermez Visual Studio et relancez.

---

## 📁 Problèmes de Collecte

### ❌ Erreur : "Aucun fichier collecté" ou 0 fichiers

**Symptômes :**
- Rapport indique : `TOTAL : 0 fichiers`
- Dossier `Data/PC1_TestServer/files/` vide

**Solutions :**

#### 1. Vérifier que les fichiers de test existent

```
C:\Users\LENOVO\Desktop\Projet_Annuel_Siemens\TestData\PC1_TestServer\test_files\
```

**Si vide :**
- FormLocalTestSetup → "📁 Générer fichiers de test"

#### 2. Vérifier les chemins de collecte

Le `FileCollector` cherche dans des chemins spécifiques :

Par défaut :
- `C:\TestData\Siemens_Files\`
- `C:\Users\[user]\Documents\Siemens_Files\`
- Home directory de l'utilisateur SSH

**Solution temporaire :** Créer un lien symbolique

```powershell
# En Admin
New-Item -ItemType SymbolicLink -Path "C:\TestData" `
    -Target "C:\Users\LENOVO\Desktop\Projet_Annuel_Siemens\TestData"
```

#### 3. Examiner les logs

Vérifier les erreurs dans le fichier `collection_report.json`

---

### ❌ Erreur : "Access denied" lors de la collecte SFTP

**Symptômes :**
- Connexion SSH OK
- Mais collecte échoue avec "Access denied"

**Solutions :**

#### 1. Vérifier les permissions du dossier

Sur le serveur (PC où SSH est installé) :

```powershell
# Donner les permissions au dossier
icacls "C:\TestData" /grant testuser:F /T
```

#### 2. Vérifier le shell par défaut

Éditer `C:\ProgramData\ssh\sshd_config` :

```
# S'assurer que le subsystem SFTP est activé
Subsystem sftp sftp-server.exe
```

Redémarrer SSH :
```powershell
Restart-Service sshd
```

---

### ❌ Erreur : "Timeout" pendant la collecte

**Symptômes :**
- La collecte démarre
- Puis timeout après quelques secondes

**Solutions :**

#### 1. Augmenter le timeout SSH

Dans le code (pour les développeurs) :

```csharp
// FileCollector.cs
private const int SSH_TIMEOUT = 300; // 5 minutes au lieu de 30 secondes
```

#### 2. Réduire le nombre de fichiers à collecter

Créer moins de fichiers de test :

```csharp
// TestEnvironmentSetup.cs
private const int FILE_COUNT = 10; // Au lieu de 20
```

---

## 📦 Problèmes de Packaging

### ❌ Erreur : "Aucune donnée trouvée pour la machine"

**Symptômes :**
- Message lors du clic sur "📦 Package & Encrypt"
- Aucun package créé

**Solution :**

Vous devez d'abord **collecter des données** avant de créer un package :

1. Sélectionner la machine (ex: PC1_TestServer)
2. Cliquer "Extract Data"
3. Attendre que la collecte soit terminée
4. **ENSUITE** cliquer "📦 Package & Encrypt"

---

### ❌ Erreur : "Erreur lors de la compression ZIP"

**Symptômes :**
- Packaging échoue à l'étape de compression

**Solutions :**

#### 1. Vérifier l'espace disque

```powershell
Get-PSDrive C | Select-Object Used,Free
```

Assurez-vous d'avoir au moins **100 MB** libres.

#### 2. Fermer les fichiers ouverts

Fermez tous les fichiers dans `Data/PC1_TestServer/files/`

#### 3. Permissions

```powershell
# Donner les permissions
icacls "C:\...\Data\PC1_TestServer" /grant Users:F /T
```

---

### ❌ Erreur : "Erreur lors du cryptage"

**Symptômes :**
- ZIP créé OK
- Mais cryptage échoue

**Solutions :**

#### 1. Vérifier la bibliothèque de cryptage

Dans Visual Studio, vérifiez les packages NuGet :
- Assurez-vous que les bibliothèques de cryptage sont installées

#### 2. Mot de passe trop court

Si vous avez fourni un mot de passe manuel, assurez-vous qu'il fait **au moins 16 caractères**.

---

## 🖥️ Problèmes Application

### ❌ Erreur : "FormLocalTestSetup n'apparaît pas"

**Symptômes :**
- Le bouton "🧪 Mode Test Local" ne fait rien
- Aucune fenêtre ne s'ouvre

**Solutions :**

#### 1. Vérifier les erreurs de compilation

```
Build → Rebuild Solution
```

Vérifiez qu'il n'y a pas d'erreurs.

#### 2. Vérifier que la classe existe

```
Projet_Siemens\Interface\FormLocalTestSetup.cs
```

Le fichier doit exister.

---

### ❌ Erreur : "Les machines de test n'apparaissent pas dans la liste"

**Symptômes :**
- Après avoir créé les machines
- La liste reste vide dans FormFileExtraction

**Solutions :**

#### 1. Vérifier le retour dans FormFileExtraction

Le code `testModeButton_Click` doit rafraîchir la liste :

```csharp
if (testSetupForm.DialogResult == DialogResult.OK)
{
    machinesList.DataSource = null;
    machinesList.DataSource = new BindingList<Machine>(parentForm.network.machines);
}
```

#### 2. Redémarrer l'application

Fermez et relancez depuis Visual Studio.

---

### ❌ Erreur : "System.NullReferenceException"

**Symptômes :**
- Exception non gérée
- Application crash

**Diagnostic :**

Regardez la stack trace dans Visual Studio :

```
Exception non gérée : System.NullReferenceException
à Projet_Siemens.Interface.FormFileExtraction.ExtractServerFiles(...) ligne XXX
```

**Solutions courantes :**

1. **Vérifier que parentForm n'est pas null**
2. **Vérifier que selectedMachine n'est pas null**
3. **Vérifier que les chemins existent**

---

## 📊 Cas Pratiques

### Scénario 1 : "Tout fonctionne sur localhost mais pas sur PC 2"

**Diagnostic :**

1. **Test ping :**
   ```powershell
   ping 192.168.1.XXX
   ```
   Si échoue → Problème réseau (voir [Problèmes Réseau](#problèmes-réseau))

2. **Test SSH manuel :**
   ```powershell
   ssh testuser@192.168.1.XXX
   ```
   Si refuse → Problème SSH sur PC 2 (voir [Problèmes SSH](#problèmes-ssh))

3. **Vérifier que SSH est installé et démarré sur PC 2**

---

### Scénario 2 : "La collecte fonctionne mais l'extraction de base échoue"

**Diagnostic :**

1. **Vérifier que la base existe :**
   ```
   C:\...\TestData\PC1_TestDatabase\database\test_mes_database.db
   ```

2. **Tester la connexion SQLite :**
   - Téléchargez [DB Browser for SQLite](https://sqlitebrowser.org/)
   - Ouvrez le fichier .db
   - Vérifiez que les tables existent

3. **Recréer la base si corrompue**

---

### Scénario 3 : "Le package se crée mais je ne trouve pas le mot de passe"

**Solutions :**

1. **Le mot de passe est affiché dans la MessageBox**
   - Notez-le IMMÉDIATEMENT !

2. **Vérifier le fichier de rapport :**
   ```
   Data/SecurePackages/PC1_TestServer_package_report.json
   ```
   Le mot de passe est dedans (en clair).

3. **Fichier PASSWORD_xxx.txt créé automatiquement**
   ```
   Data/SecurePackages/PASSWORD_XkP9#mN2$vL8@wQ5.txt
   ```

---

## 🆘 Commandes d'Urgence

Si rien ne fonctionne, exécutez ces commandes dans l'ordre :

```powershell
# 1. Redémarrer SSH
Restart-Service sshd

# 2. Réinitialiser le mot de passe
net user testuser Test@2025! /add

# 3. Vérifier le pare-feu
Get-NetFirewallRule -Name sshd

# 4. Tester SSH
ssh testuser@localhost

# 5. Si tout échoue : désinstaller et réinstaller SSH
Remove-WindowsCapability -Online -Name OpenSSH.Server~~~~0.0.1.0
Add-WindowsCapability -Online -Name OpenSSH.Server~~~~0.0.1.0
Start-Service sshd
```

---

## 📞 Obtenir de l'Aide

Si le problème persiste :

1. **Consultez les logs de l'application**
2. **Notez le message d'erreur EXACT**
3. **Notez les étapes pour reproduire le problème**
4. **Contactez l'équipe de développement**

**Informations utiles à fournir :**
- Version de Windows
- Message d'erreur complet
- Étapes de reproduction
- Captures d'écran si possible

---

**Version :** 1.0  
**Dernière mise à jour :** 2025-01-09
