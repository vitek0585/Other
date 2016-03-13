using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Crypt.Crypt.Interfaces;

namespace Crypt.Crypt
{
    public class HybridEncrypt : IHybridEncrypt<HttpRequest>
    {
        private RSACryptoServiceProvider _rsa;
        private AesCryptoServiceProvider _aes;
        private RNGCryptoServiceProvider _rng;

        public HybridEncrypt()
        {
            _rng = new RNGCryptoServiceProvider();
            _aes = new AesCryptoServiceProvider
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
            _rsa = new RSACryptoServiceProvider(2048) { PersistKeyInCsp = false };
        }

        public HttpRequest Encrypt(string line, RSAParameters publicKey)
        {
            var request = new HttpRequest();

            request.DataCrypt = EncryptData(line);
            request.SessionKey = _aes.Key;
            request.VectorKey = _aes.IV;

            request.HashData = HashData(request);

            EncryptSessionKeyRSA(publicKey, request);
            var sw = new Stopwatch();
            sw.Start();
            request.Signature = SignDataRsa(request);//SignData(request);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            return request;
        }

        private void EncryptSessionKeyRSA(RSAParameters publicKey, HttpRequest request)
        {
            using (_rsa)
            {
                _rsa.ImportParameters(publicKey);
                request.SessionKey = _rsa.Encrypt(request.SessionKey, false);
            }
        }

        private byte[] HashData(HttpRequest request)
        {
            using (var hash256 = new HMACSHA256(request.SessionKey))
            {
                return hash256.ComputeHash(request.DataCrypt);
            }
        }
        private byte[] SignDataRsa(HttpRequest request)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                //var privateKey = rsa.ExportParameters(true);
                request.PublicKeySign = rsa.ExportParameters(false);

                //rsa.ImportParameters(privateKey);
                
                return rsa.SignHash(request.HashData, "SHA256");
            }
        }
        private byte[] SignData(HttpRequest request)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                var privateKey = rsa.ExportParameters(true);
                request.PublicKeySign = rsa.ExportParameters(false);

                rsa.ImportParameters(privateKey);

                var signFormatter = new RSAPKCS1SignatureFormatter(rsa);
                signFormatter.SetHashAlgorithm("SHA256");
                
                return signFormatter.CreateSignature(request.HashData);

            }
        }
       
        private byte[] EncryptData(string line)
        {
            var lineByte = Encoding.UTF8.GetBytes(line);
            
            var key = new byte[32];
            _rng.GetBytes(key);

            var iv = new byte[16];
            _rng.GetBytes(iv);

            _aes.Key = key;
            _aes.IV = iv;
            using (var memory = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memory, _aes.CreateEncryptor(), CryptoStreamMode.Write);

                cryptoStream.Write(lineByte, 0, lineByte.Length);
                cryptoStream.FlushFinalBlock();
                return memory.ToArray();
            }

        }
    }
}