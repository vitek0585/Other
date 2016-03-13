using System.Security.Cryptography;
using Crypt.Crypt.Interfaces;

namespace Crypt
{
    public class Server<TItem>
    {
        private IHybridDecrypt<TItem> _decrypt; 

        private RSAParameters _pubKey;
        private RSAParameters _privKey;
        public Server(IHybridDecrypt<TItem> decrypt)
        {
            RSACryptoServiceProvider _rsa;
            _decrypt = decrypt;
            _rsa = new RSACryptoServiceProvider(2048) { PersistKeyInCsp = false };
            _pubKey = _rsa.ExportParameters(false);
            _privKey = _rsa.ExportParameters(true);
        }

        public RSAParameters GetPuplicKey()
        {
            return _pubKey;
        }

        public string DataRead(TItem request)
        {
            return _decrypt.Decrypt(request,_privKey);
        }
         
    }
}