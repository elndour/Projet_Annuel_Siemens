# ❓ FAQ - Questions Fréquentes sur les Tests Locaux

## Table des Matières

- [Questions Générales](#questions-générales)
- [Installation et Configuration](#installation-et-configuration)
- [Utilisation](#utilisation)
- [Sécurité](#sécurité)
- [Performance](#performance)
- [Dépannage](#dépannage)

---

## 🌟 Questions Générales

### Q1 : Pourquoi créer un environnement de test local ?

**R :** Pour plusieurs raisons :

✅ **Développement sans risque**
- Tester sans toucher aux serveurs de production
- Pas besoin d'accès VPN ou serveurs distants
- Développer offline

✅ **Rapidité**
- Tests instantanés (pas de latence réseau)
- Itérations rapides
- Débogage en temps réel

✅ **Apprentissage**
- Comprendre le fonctionnement SSH/SFTP
- Tester différents scénarios
- Expérimenter sans conséquences

---

### Q2 : Ai-je vraiment besoin de 2 PCs ?

**R :** **NON**, un seul PC suffit !

- **PC 1 seul** : Vous pouvez tout tester avec localhost (127.0.0.1)
  - Collecte SSH locale ✅
  - Extraction base de données ✅
  - Packaging ✅

- **PC 2 (optionnel)** : Utile pour tester la collecte distante
  - Simulation d'un vrai environnement réseau
  - Test de connectivité réseau
  - Validation SSH sur réseau local

---

### Q3 : Combien de temps prend la configuration initiale ?

**R :** Environ **30-45 minutes** pour la première fois :

| Phase | Temps |
|-------|-------|
| Installation SSH | 10 min |
| Configuration utilisateur | 5 min |
| Tests de connexion | 5 min |
| Configuration app | 10 min |
| Premier test complet | 10 min |

**Les fois suivantes : ~5 minutes** (juste lancer l'app)

---

### Q4 : Est-ce que cela fonctionne sur Windows 10 et 11 ?

**R :** **OUI** ! Compatible avec :

- ✅ Windows 10 (version 1809 ou plus récente)
- ✅ Windows 11 (toutes versions)
- ✅ Windows Server 2019/2022

**Prérequis :** OpenSSH (inclus dans Windows 10/11 récents)

---

## 🔧 Installation et Configuration

### Q5 : SSH est-il déjà installé sur mon PC ?

**R :** Vérifiez avec cette commande PowerShell :

```powershell
Get-WindowsCapability -Online | Where-Object Name -like 'OpenSSH*'
```

**Résultat :**
- `State : Installed` → ✅ Déjà installé
- `State : NotPresent` → ❌ À installer

---

### Q6 : Puis-je utiliser un autre port que 22 pour SSH ?

**R :** **OUI**, mais déconseillé pour les tests locaux.

Si vous voulez utiliser le port 2222 par exemple :

1. **Éditer la config SSH** (`C:\ProgramData\ssh\sshd_config`) :
   ```
   Port 2222
   ```

2. **Redémarrer SSH** :
   ```powershell
   Restart-Service sshd
   ```

3. **Ouvrir le pare-feu** :
   ```powershell
   New-NetFirewallRule -Name sshd -DisplayName 'OpenSSH Custom' `
       -Enabled True -Direction Inbound -Protocol TCP -Action Allow -LocalPort 2222
   ```

4. **Dans l'app, entrer le port 2222** au lieu de 22

---

### Q7 : Quel mot de passe utiliser pour l'utilisateur de test ?

**R :** Pour les tests locaux, utilisez un mot de passe simple :

- ✅ `test123` (si pas de politique de mot de passe)
- ✅ `Test@2025!` (si politique stricte)

⚠️ **IMPORTANT :** Ce mot de passe est **uniquement pour les tests**. N'utilisez JAMAIS un mot de passe réel de production !

---

### Q8 : Puis-je utiliser mon propre compte Windows ?

**R :** **OUI**, mais déconseillé.

**Avantages :**
- Pas besoin de créer un utilisateur
- Accès direct à vos fichiers

**Inconvénients :**
- ❌ Sécurité (votre mot de passe utilisé pour les tests)
- ❌ Permissions trop élevées
- ❌ Mélange de fichiers perso et fichiers de test

**Recommandation :** Créez un utilisateur dédié `testuser`

---

## 💻 Utilisation

### Q9 : Combien de fichiers de test sont générés ?

**R :** **20 fichiers** par défaut :

| Type | Quantité | Taille |
|------|----------|--------|
| .log | 5 | ~1 Ko chacun |
| .xml | 5 | ~1 Ko chacun |
| .config | 5 | ~1 Ko chacun |
| .nfo | 5 | ~2 Ko chacun |

**Total : ~30 Ko**

Vous pouvez modifier ce nombre dans le code (`TestEnvironmentSetup.cs`)

---

### Q10 : La base de données SQLite contient quelles données ?

**R :** La base de test contient des **données MES réalistes** :

| Table | Données |
|-------|---------|
| `ProductionOrders` | 150 ordres de production (30 jours) |
| `SystemLogs` | 500 logs système |
| `TaskStatus` | 50 tâches MES |
| `MachineStats` | Statistiques de 10 machines |
| `StopEvents` | 100 événements d'arrêt |
| `QualityMetrics` | Métriques de qualité |

**Taille totale : ~2-3 MB**

---

### Q11 : Puis-je tester plusieurs fois de suite ?

**R :** **OUI** ! L'environnement de test est conçu pour être réutilisable :

1. **Collecte multiple :** Vous pouvez collecter les mêmes fichiers plusieurs fois
2. **Extraction multiple :** La base de données peut être interrogée indéfiniment
3. **Packaging multiple :** Créez autant de packages que nécessaire

**Note :** Les fichiers précédents sont écrasés à chaque collecte.

---

### Q12 : Que faire des packages cryptés créés ?

**R :** Plusieurs options :

1. **Test de décryptage :** Utilisez-les pour tester le décryptage
2. **Archivage :** Conservez-les comme exemples
3. **Suppression :** Supprimez-les si vous n'en avez plus besoin

⚠️ **N'oubliez pas de noter le mot de passe !**

---

### Q13 : Les données collectées sont-elles réelles ?

**R :** **NON**, ce sont des **données de test simulées** :

- Noms de machines : `PC1_TestServer`, `PC2_TestServer`
- Ordres de production : Générés aléatoirement
- Logs : Contenu simulé
- Timestamps : Dates récentes réalistes

**Aucune donnée de production réelle n'est utilisée.**

---

## 🔒 Sécurité

### Q14 : Est-ce sécurisé d'installer SSH sur mon PC ?

**R :** **OUI**, si configuré correctement :

✅ **Bonnes pratiques :**
- Utilisez un mot de passe fort pour l'utilisateur SSH
- Activez le pare-feu Windows
- N'exposez PAS le port 22 sur Internet
- Utilisez SSH uniquement sur le réseau local

❌ **À éviter :**
- Ne pas utiliser de mots de passe faibles
- Ne pas désactiver le pare-feu complètement
- Ne pas ouvrir SSH sur Internet

**Pour les tests locaux (localhost), il n'y a AUCUN risque.**

---

### Q15 : Mon antivirus bloque-t-il SSH ?

**R :** **Parfois oui**.

**Solutions :**

1. **Autoriser sshd.exe** dans l'antivirus
2. **Ajouter une exception** pour le port 22
3. **Désactiver temporairement** l'antivirus (pour tester)

**Chemins à autoriser :**
- `C:\Windows\System32\OpenSSH\sshd.exe`
- `C:\Windows\System32\OpenSSH\sftp-server.exe`

---

### Q16 : Le mot de passe de cryptage est-il stocké quelque part ?

**R :** **OUI**, dans plusieurs endroits :

1. **MessageBox** après le packaging (notez-le !)
2. **Fichier JSON** : `package_report.json`
3. **Fichier texte** : `PASSWORD_xxx.txt` (créé automatiquement)

⚠️ **ATTENTION :** Ces fichiers contiennent le mot de passe en clair. Sécurisez-les !

---

### Q17 : Puis-je décrypter un package plus tard ?

**R :** **OUI**, avec le mot de passe.

**Outils pour décrypter :**

1. **En ligne de commande (OpenSSL) :**
   ```bash
   openssl enc -d -aes-256-cbc -in package.zip.enc -out package.zip -k "MotDePasse"
   ```

2. **Dans l'application** (fonctionnalité à venir)

3. **Script PowerShell personnalisé**

---

## ⚡ Performance

### Q18 : Combien de temps prend la collecte SSH ?

**R :** Très rapide en local :

| Opération | Temps |
|-----------|-------|
| Connexion SSH | 1-2 sec |
| Collecte 20 fichiers | 3-5 sec |
| Organisation | 1 sec |
| **Total** | **5-10 sec** |

**Sur réseau local (PC 2) :** +2-3 secondes

---

### Q19 : Pourquoi l'extraction de base de données prend-elle plus de temps ?

**R :** Plusieurs requêtes SQL sont exécutées :

| Requête | Temps approximatif |
|---------|-------------------|
| Production Orders | 3 sec |
| Error Logs | 2.5 sec |
| Task Status | 3 sec |
| Machine Stats | 3.5 sec |

**Total : ~12-15 secondes**

C'est normal car chaque requête :
1. Interroge la base
2. Convertit les données en JSON
3. Sauvegarde dans un fichier

---

### Q20 : Puis-je réduire le temps de collecte ?

**R :** **OUI**, plusieurs options :

1. **Réduire le nombre de fichiers** :
   ```csharp
   // TestEnvironmentSetup.cs
   private const int FILE_COUNT = 10; // Au lieu de 20
   ```

2. **Augmenter le timeout SSH** (si nécessaire)

3. **Optimiser les requêtes SQL** (pour les devs)

---

## 🐛 Dépannage

### Q21 : Que faire si "ssh: command not found" ?

**R :** SSH n'est pas dans le PATH ou pas installé.

**Vérification :**
```powershell
where.exe ssh
```

**Si absent, installer :**
```powershell
Add-WindowsCapability -Online -Name OpenSSH.Client~~~~0.0.1.0
```

---

### Q22 : "Connection refused" - Que faire ?

**R :** Checklist :

- [ ] Service SSH démarré ? `Get-Service sshd`
- [ ] Pare-feu configuré ? `Get-NetFirewallRule -Name sshd`
- [ ] Port correct (22) ?
- [ ] IP correcte ?

**Commande de diagnostic :**
```powershell
Test-NetConnection -ComputerName localhost -Port 22
```

---

### Q23 : Les fichiers ne sont pas collectés - Pourquoi ?

**R :** Causes possibles :

1. **Fichiers de test non générés**
   → FormLocalTestSetup → "Générer fichiers de test"

2. **Mauvais chemin de collecte**
   → Le FileCollector cherche dans des chemins spécifiques

3. **Permissions insuffisantes**
   → Vérifier les permissions du dossier

**Solution rapide :** Créer un lien symbolique :
```powershell
New-Item -ItemType SymbolicLink -Path "C:\TestData" `
    -Target "C:\Users\LENOVO\Desktop\Projet_Annuel_Siemens\TestData"
```

---

### Q24 : "Database is locked" - Comment résoudre ?

**R :** SQLite permet une seule connexion d'écriture.

**Solutions :**

1. **Fermer les connexions** :
   ```powershell
   taskkill /F /IM sqlite3.exe
   ```

2. **Redémarrer l'application**

3. **Attendre quelques secondes** et réessayer

---

### Q25 : Comment réinitialiser complètement l'environnement de test ?

**R :** Étapes pour tout effacer :

```powershell
# 1. Supprimer les données de test
Remove-Item "C:\...\TestData" -Recurse -Force

# 2. Supprimer les résultats
Remove-Item "C:\...\Data" -Recurse -Force

# 3. Supprimer l'utilisateur SSH
net user testuser /delete

# 4. Arrêter SSH (optionnel)
Stop-Service sshd

# 5. Relancer l'app et reconfigurer
```

---

## 📚 Ressources

### Q26 : Où trouver plus d'informations ?

**R :** Documentation disponible :

| Document | Description |
|----------|-------------|
| `PROCEDURE_TEST_COMPLETE_DETAILLEE.txt` | Guide complet 40 pages |
| `CHECKLIST_TEST_RAPIDE.md` | Référence rapide |
| `DIAGRAMME_FLUX_TESTS.txt` | Diagramme visuel |
| `TROUBLESHOOTING_GUIDE.md` | Guide de dépannage |
| `GUIDE_Configuration_Test_Local_2PCs.txt` | Guide SSH détaillé |
| `README_Test_Local.md` | Démarrage rapide |

---

### Q27 : Comment contribuer à améliorer les tests ?

**R :** Plusieurs façons :

1. **Signaler des bugs** : GitHub Issues
2. **Proposer des améliorations** : Pull Requests
3. **Améliorer la documentation** : Éditer les fichiers .md
4. **Créer de nouveaux tests** : Ajouter des scénarios

---

### Q28 : Y a-t-il des vidéos tutorielles ?

**R :** Pas encore, mais vous pouvez :

1. **Suivre le guide étape par étape** (`PROCEDURE_TEST_COMPLETE_DETAILLEE.txt`)
2. **Utiliser la checklist** (`CHECKLIST_TEST_RAPIDE.md`)
3. **Consulter les captures d'écran** (dans la doc)

---

## 🎯 Cas d'Usage

### Q29 : Puis-je utiliser cet environnement pour former de nouveaux développeurs ?

**R :** **OUI** ! C'est un excellent cas d'usage :

✅ **Avantages :**
- Environnement sûr (pas de production)
- Apprentissage hands-on
- Documentation complète
- Pas besoin d'accès serveurs

**Exercices suggérés :**
1. Configurer SSH de zéro
2. Collecter des fichiers via SFTP
3. Extraire des données d'une base
4. Créer un package sécurisé
5. Débugger des erreurs de connexion

---

### Q30 : Puis-je adapter cet environnement pour d'autres projets ?

**R :** **OUI** ! Le code est modulaire :

**Composants réutilisables :**
- `FileCollector` : Collecte SSH/SFTP générique
- `SQLiteTestDatabaseCreator` : Création de bases de test
- `SecurePackageManager` : Packaging et cryptage
- `AESEncryption` : Cryptage AES-256

**Exemples d'adaptation :**
- Collecter d'autres types de fichiers (.csv, .json, etc.)
- Utiliser PostgreSQL ou MySQL au lieu de SQLite
- Ajouter d'autres formats de packaging (tar.gz, 7z, etc.)
- Implémenter d'autres méthodes de cryptage

---

## 📞 Support

### Besoin d'aide ?

1. **Consultez d'abord :**
   - Cette FAQ
   - Le guide de dépannage (`TROUBLESHOOTING_GUIDE.md`)
   - La procédure complète (`PROCEDURE_TEST_COMPLETE_DETAILLEE.txt`)

2. **Toujours bloqué ?**
   - Vérifiez les logs de l'application
   - Notez le message d'erreur exact
   - Contactez l'équipe de développement

3. **Informations à fournir :**
   - Version de Windows
   - Message d'erreur complet
   - Étapes pour reproduire
   - Captures d'écran

---

**Version :** 1.0  
**Dernière mise à jour :** 2025-01-09  
**Auteurs :** Équipe Projet Siemens

---

💡 **Astuce :** Gardez ce document à portée de main lors de vos premiers tests !
