using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Crypt.Crypt.Interfaces;

namespace Crypt.Crypt
{
    public class HybridDecrypt : IHybridDecrypt<HttpRequest>
    {
        private AesCryptoServiceProvider _aes;

        public HybridDecrypt()
        {
            _aes = new AesCryptoServiceProvider
              {
                  Padding = PaddingMode.PKCS7,
                  Mode = CipherMode.CBC
              };
        }

        public string Decrypt(HttpRequest request, RSAParameters privKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048){PersistKeyInCsp = false};
            rsa.ImportParameters(privKey);

            var isDataValid = CompareHashData(request, rsa);//проверяет хеш данных
            if (!isDataValid)
            {
                throw new CryptographicException("Data is not match");
            }
            var isValidSign = VerifySignatureSimple(request);//VerifySignature(request);
            if (!isValidSign)
            {
                throw new CryptographicException("Signature is not valid");
            }
            _aes.Key = request.SessionKey;
            _aes.IV = request.VectorKey;

            using (var memory = new MemoryStream())
            {
                var crypto = new CryptoStream(memory, _aes.CreateDecryptor(), CryptoStreamMode.Write);
                crypto.Write(request.DataCrypt, 0, request.DataCrypt.Length);
                crypto.FlushFinalBlock();

                return Encoding.UTF8.GetString(memory.ToArray());
            }
        }
        private bool VerifySignatureSimple(HttpRequest request)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(request.PublicKeySign);
                
                return rsa.VerifyHash(request.HashData,"SHA256", request.Signature);
            }
        }
        private bool VerifySignature(HttpRequest request)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(request.PublicKeySign);

                var signDfmtr = new RSAPKCS1SignatureDeformatter(rsa);
                signDfmtr.SetHashAlgorithm("SHA256");

                return signDfmtr.VerifySignature(request.HashData, request.Signature);
            }
        }
        //проверяет что хеш данных один и тот же 
        private bool CompareHashData(HttpRequest request, RSACryptoServiceProvider rsa)
        {
            //расшифровываем SessionKey (раннее request.SessionKey = _rsa.Encrypt(request.SessionKey, false);)
            request.SessionKey = rsa.Decrypt(request.SessionKey, false);
            //request.SessionKey это ключ для симитричного шифрования
            using (var hmac = new HMACSHA256(request.SessionKey))
            {
                //получаем DataCrypt (раннее hash256.ComputeHash(request.DataCrypt);) 
                var dataHash = hmac.ComputeHash(request.DataCrypt);
                //проверяем хеш данных то что он один и тот же
                return ComparerHashData(dataHash, request.HashData);
            }

        }

        private bool ComparerHashData(byte[] dataHash, byte[] hashData)
        {
            var result = dataHash.Length == hashData.Length;

            if (result)
                for (int i = 0; i < dataHash.Length; i++)
                {
                    result &= dataHash[i] == hashData[i];

                }

            return result;
        }
    }
}