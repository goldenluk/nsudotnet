using System;
using System.IO;
using System.Security.Cryptography;

namespace Khadzhiev.Nsudotnet.Enigma
{
    internal class Program
    {
        private static string _mode;
        private static string _inputFile;
        private static string _outputFile;
        private static string _keyFile;
        private static string _algorithm;

        private static void PrintUsage()
        {
            Console.WriteLine("Incorrect arguments");
        }

        private static void Main(string[] args)
        {
            if (args.Length == 4 || (args.Length == 5))
            {
                _keyFile = null;
                _mode = args[0];
                _inputFile = args[1];
                _algorithm = args[2];
                
              
                if (args.Length == 5)
                {
                    _keyFile = args[3];
                    _outputFile = args[4];
                }
                else
                {
                    _outputFile = args[3];
                }
               
                
                
                if (_mode != "encrypt" && _mode != "decrypt")
                {
                    return;
                }

                
                if (_mode == "encrypt")
                {
                    Encrypt(_inputFile, _algorithm, _outputFile);
                }
                else
                {
                    Decrypt(_inputFile, _algorithm, _keyFile, _outputFile);
                }
          
            }
            else
            {
                PrintUsage();
            }
        }

        public static Tuple<string, string> ReadKey(int length, string keyFile)
        {
            using (var keyStream = new FileStream(keyFile, FileMode.Open, FileAccess.Read))
            {
                
                string key;
                string iv;
                using (var reader = new StreamReader(keyStream))
                {
                    key = reader.ReadLine();
                    iv = reader.ReadLine();
                }
                var tuple = new Tuple<string, string>(key, iv);
                return tuple;
            }
        }


        public static void WriteKey(string key, string dir, string iv)
        {
            var keys = dir + "\\key.txt";
            using (var fsKeys = new FileStream(keys, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fsKeys))
                {
                    writer.WriteLine(key);
                    writer.WriteLine(iv);
                }
            }
        }


        public static void WriteCryptoData(FileStream fsInput, FileStream fsOutput, ICryptoTransform cryptoTransform)
        {
            using (var cryptostream = new CryptoStream(fsOutput, cryptoTransform, CryptoStreamMode.Write))
            {
                fsInput.CopyTo(cryptostream);
                
            }
        }


        private static SymmetricAlgorithm GetServiceProvider(string alorithm)
        {
            switch (alorithm.ToUpper())
            {
                case "AES":
                    return new AesCryptoServiceProvider();
                case "DES":
                    return new DESCryptoServiceProvider();
                case "RC2":
                    return new RC2CryptoServiceProvider();
                case "RIJNDAEL":
                    return new RijndaelManaged();
                default:
                    throw new Exception("Incorrect algorithm");
            }
        }


        public static void Encrypt(string inputFile, string algorithm, string outputFile)
        {
            using (var provider = GetServiceProvider(algorithm))
            {
               

                var key = Convert.ToBase64String(provider.Key);
                var iv = Convert.ToBase64String(provider.IV);

                using (var fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                {
                    using (var fsOutput = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        var dir = Path.GetDirectoryName(outputFile);
                        WriteKey(key, dir, iv);
                        
                        
                        var aesEncrypt = provider.CreateEncryptor();
                        WriteCryptoData(fsInput, fsOutput, aesEncrypt);
                    }
                }
            }
        }


        public static void Decrypt(string inputFile, string algorithm, string key, string outputFile)
        {
            var length = 8; //for DES and RC2
            if (algorithm.ToUpper() == "AES" || algorithm.ToUpper() == "RIJNDAEL")
                length *= 2;
            var keyIv = ReadKey(length, key);
            using (var provider = GetServiceProvider(algorithm))
            {
                provider.Key = Convert.FromBase64String(keyIv.Item1);
                provider.IV = Convert.FromBase64String(keyIv.Item2);
                using (var fsRead = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                {
                    using (var fsWrite = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        using (var decrypt = provider.CreateDecryptor())
                        {
                            using (var cryptoStream = new CryptoStream(fsRead, decrypt, CryptoStreamMode.Read))
                            {
                                cryptoStream.CopyTo(fsWrite);
                            }
                        }
                    }
                }
            }
        }
    }
}