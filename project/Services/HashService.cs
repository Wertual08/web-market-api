using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Api.Services {
    public class HashService {
        private RNGCryptoServiceProvider RNGCSP = new();
        private int SaltLength;
        private int IterationCount;
        private int HashLenght;
        
        private static void ToBytes(byte[] bytes, int offset, int value) {
            bytes[offset + 0] = (byte)((value >> 24) & 0xff);
            bytes[offset + 1] = (byte)((value >> 16) & 0xff);
            bytes[offset + 2] = (byte)((value >> 8) & 0xff);
            bytes[offset + 3] = (byte)((value) & 0xff);
        }
        
        private static int FromBytes(byte[] bytes, int offset) {
            return
                ((int)bytes[offset + 0] << 24) |
                ((int)bytes[offset + 1] << 16) |
                ((int)bytes[offset + 2] << 8) |
                ((int)bytes[offset + 3]);
        }

        public HashService(HashServiceConfig config) {
            if (config is null) {
                config = new();
            }

            SaltLength = config.SaltLength;
            IterationCount = config.IterationCount;
            HashLenght = config.HashLenght;
        }

        public string Make(string password) {
            byte[] alg = Encoding.UTF8.GetBytes("PBKDF2");

            byte[] lengths = new byte[sizeof(int) * 2];
            ToBytes(lengths, sizeof(int) * 0, IterationCount);
            ToBytes(lengths, sizeof(int) * 1, HashLenght);

            byte[] salt = new byte[SaltLength];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(salt);
            }
            
            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: IterationCount,
                numBytesRequested: HashLenght
            );

            return $"{Convert.ToBase64String(alg)}.{Convert.ToBase64String(lengths)}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }
        
        public static bool Check(string hash, string password) {
            var parts = hash.Split('.');

            string alg = Encoding.UTF8.GetString(Convert.FromBase64String(parts[0]));

            switch (alg) {
                case "PBKDF2": {
                    byte[] lengths = Convert.FromBase64String(parts[1]);
                    int iterationCount = FromBytes(lengths, sizeof(int) * 0);
                    int hashLength = FromBytes(lengths, sizeof(int) * 1);
                    Console.WriteLine($"{iterationCount}: {hashLength}");
                    byte[] salt = Convert.FromBase64String(parts[2]);

                    byte[] validHash = Convert.FromBase64String(parts[3]);

                    byte[] pwdHash = KeyDerivation.Pbkdf2(
                        password: password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: iterationCount,
                        numBytesRequested: hashLength
                    ); 

                    Console.WriteLine($"{validHash}: {pwdHash}");

                    return validHash.SequenceEqual(pwdHash);
                } 

                default: throw new NotImplementedException();
            }
        }
    } 
}