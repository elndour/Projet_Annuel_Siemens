using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Projet_Siemens.Security
{
    /// <summary>
    /// Gère le cryptage et décryptage AES-256 des fichiers
    /// Utilise AES-256-CBC avec clé dérivée de mot de passe (PBKDF2)
    /// </summary>
    public class AESEncryption
    {
        private const int KeySize = 256; // AES-256
        private const int BlockSize = 128; // AES standard
        private const int Iterations = 10000; // PBKDF2 iterations
        private const int SaltSize = 32; // 32 bytes
        private const int IVSize = 16; // 16 bytes (AES block size)

        /// <summary>
        /// Crypte un fichier avec AES-256
        /// </summary>
        /// <param name="inputFilePath">Fichier source (ex: .zip)</param>
        /// <param name="outputFilePath">Fichier crypté de sortie (ex: .zip.enc)</param>
        /// <param name="password">Mot de passe pour le cryptage</param>
        /// <returns>True si succès, False sinon</returns>
        public bool EncryptFile(string inputFilePath, string outputFilePath, string password)
        {
            try
            {
                // Vérifier que le fichier source existe
                if (!File.Exists(inputFilePath))
                {
                    MessageBox.Show($"Le fichier source n'existe pas :\n{inputFilePath}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Vérifier le mot de passe
                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Le mot de passe ne peut pas être vide", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Générer un salt aléatoire
                byte[] salt = GenerateRandomBytes(SaltSize);

                // Dériver la clé du mot de passe
                byte[] key = DeriveKeyFromPassword(password, salt);

                // Générer un IV aléatoire
                byte[] iv = GenerateRandomBytes(IVSize);

                // Créer le fichier de sortie
                using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create))
                {
                    // Écrire le salt en premier (pour pouvoir décrypter plus tard)
                    outputStream.Write(salt, 0, salt.Length);

                    // Écrire l'IV
                    outputStream.Write(iv, 0, iv.Length);

                    // Créer le crypteur AES
                    using (Aes aes = Aes.Create())
                    {
                        aes.KeySize = KeySize;
                        aes.BlockSize = BlockSize;
                        aes.Key = key;
                        aes.IV = iv;
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;

                        // Créer le stream crypté
                        using (CryptoStream cryptoStream = new CryptoStream(outputStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            // Lire et crypter le fichier source
                            using (FileStream inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                            {
                                inputStream.CopyTo(cryptoStream);
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du cryptage :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Décrypte un fichier AES-256
        /// </summary>
        /// <param name="inputFilePath">Fichier crypté (.enc)</param>
        /// <param name="outputFilePath">Fichier décrypté de sortie</param>
        /// <param name="password">Mot de passe de décryptage</param>
        /// <returns>True si succès, False sinon</returns>
        public bool DecryptFile(string inputFilePath, string outputFilePath, string password)
        {
            try
            {
                // Vérifier que le fichier source existe
                if (!File.Exists(inputFilePath))
                {
                    MessageBox.Show($"Le fichier crypté n'existe pas :\n{inputFilePath}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Vérifier le mot de passe
                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Le mot de passe ne peut pas être vide", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (FileStream inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                {
                    // Lire le salt
                    byte[] salt = new byte[SaltSize];
                    inputStream.Read(salt, 0, SaltSize);

                    // Lire l'IV
                    byte[] iv = new byte[IVSize];
                    inputStream.Read(iv, 0, IVSize);

                    // Dériver la clé du mot de passe
                    byte[] key = DeriveKeyFromPassword(password, salt);

                    // Créer le décrypteur AES
                    using (Aes aes = Aes.Create())
                    {
                        aes.KeySize = KeySize;
                        aes.BlockSize = BlockSize;
                        aes.Key = key;
                        aes.IV = iv;
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;

                        // Créer le stream décrypté
                        using (CryptoStream cryptoStream = new CryptoStream(inputStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            // Écrire le fichier décrypté
                            using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create))
                            {
                                cryptoStream.CopyTo(outputStream);
                            }
                        }
                    }
                }

                return true;
            }
            catch (CryptographicException)
            {
                MessageBox.Show("Mot de passe incorrect ou fichier corrompu", "Erreur de décryptage", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du décryptage :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Génère une clé cryptographique à partir d'un mot de passe
        /// Utilise PBKDF2 (Password-Based Key Derivation Function 2)
        /// </summary>
        private byte[] DeriveKeyFromPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(KeySize / 8); // KeySize est en bits, GetBytes attend des bytes
            }
        }

        /// <summary>
        /// Génère des bytes aléatoires cryptographiquement sûrs
        /// </summary>
        private byte[] GenerateRandomBytes(int length)
        {
            byte[] bytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            return bytes;
        }

        /// <summary>
        /// Génère un mot de passe aléatoire sécurisé
        /// </summary>
        /// <param name="length">Longueur du mot de passe (minimum 16)</param>
        /// <returns>Mot de passe généré</returns>
        public string GenerateSecurePassword(int length = 32)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=[]{}|;:,.<>?";
            
            if (length < 16)
                length = 16;

            StringBuilder password = new StringBuilder();
            byte[] randomBytes = GenerateRandomBytes(length);

            foreach (byte b in randomBytes)
            {
                password.Append(validChars[b % validChars.Length]);
            }

            return password.ToString();
        }

        /// <summary>
        /// Crypte un fichier et ajoute l'extension .enc
        /// </summary>
        public string EncryptFileWithExtension(string inputFilePath, string password)
        {
            string outputPath = inputFilePath + ".enc";
            
            if (EncryptFile(inputFilePath, outputPath, password))
            {
                return outputPath;
            }

            return null;
        }

        /// <summary>
        /// Décrypte un fichier .enc et retire l'extension
        /// </summary>
        public string DecryptFileWithExtension(string encryptedFilePath, string password)
        {
            if (!encryptedFilePath.EndsWith(".enc"))
            {
                MessageBox.Show("Le fichier doit avoir l'extension .enc", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            string outputPath = encryptedFilePath.Substring(0, encryptedFilePath.Length - 4); // Retire ".enc"

            if (DecryptFile(encryptedFilePath, outputPath, password))
            {
                return outputPath;
            }

            return null;
        }

        /// <summary>
        /// Calcule le hash SHA-256 d'un fichier (pour vérification d'intégrité)
        /// </summary>
        public string CalculateFileHash(string filePath)
        {
            try
            {
                using (FileStream stream = File.OpenRead(filePath))
                {
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        byte[] hashBytes = sha256.ComputeHash(stream);
                        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du calcul du hash :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
