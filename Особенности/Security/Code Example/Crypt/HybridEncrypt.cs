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
        //асимитричное - публичный и приватные ключи 
        private RSACryptoServiceProvider _rsa;
        //симитричное шифрование - используется один и тот же ключ
        private AesCryptoServiceProvider _aes;
        //случайная последовательность байт
        private RNGCryptoServiceProvider _rng;

        public HybridEncrypt()
        {
            _rng = new RNGCryptoServiceProvider();
            _aes = new AesCryptoServiceProvider
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
            //сохранять ли ключи после их выдачи PersistKeyInCsp
            _rsa = new RSACryptoServiceProvider(2048) { PersistKeyInCsp = false };
        }

        public HttpRequest Encrypt(string line, RSAParameters publicKey)
        {
            var request = new HttpRequest();

            request.DataCrypt = EncryptData(line);//(1) возвращает хешированые данные на основе симетрич. шифр.
            request.SessionKey = _aes.Key;//передаем ключ (на этапе 1) для расшифровки симитричным шифр. (данный
            //ключ шифруется ассиметрич. шифр.)
            request.VectorKey = _aes.IV;//передаем вектор (на этапе 1) для расшифровки симитричным шифр.

            request.HashData = HashData(request);//получили хеш данных (чтобы при расшифровки проверить что хеши совпадают
            //и данные не были изменены)

            EncryptSessionKeyRsa(publicKey, request);//зашифровываем асимет. шифр. SessionKey
            var sw = new Stopwatch();
            sw.Start();
            //создаем подпись для HashData и получаем request.PublicKeySign
            request.Signature = SignDataRsa(request);//SignData(request);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            return request;
        }
        //(3)
        private void EncryptSessionKeyRsa(RSAParameters publicKey, HttpRequest request)
        {
            using (_rsa)
            {
                _rsa.ImportParameters(publicKey);
                request.SessionKey = _rsa.Encrypt(request.SessionKey, false);
            }
        }
        //(2)
        private byte[] HashData(HttpRequest request)
        {
            //создаем хеш на основе ассимитричного ключа
            using (var hash256 = new HMACSHA256(request.SessionKey))
            {
                //создаем хеш шифрованных данных (чтобы при расшифровки проверить что хеши совпадают
                //и данные не были изменены)
                return hash256.ComputeHash(request.DataCrypt);
            }
        }
        //делаем подпись при помощи стандартного метода rsa
        private byte[] SignDataRsa(HttpRequest request)
        {
            //асимитричное - публичный и приватные ключи 
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                //var privateKey = rsa.ExportParameters(true);
                request.PublicKeySign = rsa.ExportParameters(false);

                //rsa.ImportParameters(privateKey);
                
                return rsa.SignHash(request.HashData, "SHA256");
            }
        }
        //делаем подпись при помощи RSAPKCS1SignatureFormatter и private key
        private byte[] SignData(HttpRequest request)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                //получаем закрытый ключ
                var privateKey = rsa.ExportParameters(true);
                //получаем открытый ключ
                request.PublicKeySign = rsa.ExportParameters(false);
                //вставляем закрытый ключ
                rsa.ImportParameters(privateKey);

                var signFormatter = new RSAPKCS1SignatureFormatter(rsa);
                signFormatter.SetHashAlgorithm("SHA256");
                //создаем подпись для данных которую можно будет расшифровать только при помощи закрытого ключа
                return signFormatter.CreateSignature(request.HashData);

            }
        }
        //(1) шифрование данных симитричным алгоритмом (используется один и тот же ключ для шифрования и расшифрования данных)
        private byte[] EncryptData(string line)
        {
            //переволим строку в байты
            var lineByte = Encoding.UTF8.GetBytes(line);
            
            //заполняем массив байт случайной последовательностью (ключ для симитричного шифрования)
            var key = new byte[32];
            _rng.GetBytes(key);

            //заполняем массив байт случайной последовательностью (вектор иниц. для симитричного шифрования)
            var iv = new byte[16];
            _rng.GetBytes(iv);

            _aes.Key = key;
            _aes.IV = iv;
            using (var memory = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memory, _aes.CreateEncryptor(), CryptoStreamMode.Write);
                //шифруем симитричным алгоритмом
                cryptoStream.Write(lineByte, 0, lineByte.Length);
                cryptoStream.FlushFinalBlock();
                return memory.ToArray();
            }

        }
    }
}