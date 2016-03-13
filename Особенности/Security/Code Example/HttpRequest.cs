using System.Security.Cryptography;

namespace Crypt
{
    public class HttpRequest
    {
        public byte[] SessionKey { get; set; }
        public byte[] VectorKey { get; set; }
        public byte[] HashData { get; set; }
        public byte[] DataCrypt { get; set; }

        public byte[] Signature { get; set; }
        public RSAParameters PublicKeySign { get; set; }
    }
}