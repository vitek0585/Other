using System.Security.Cryptography;

namespace Crypt.Crypt.Interfaces
{
    public interface IHybridEncrypt<out TResult> : IHybrid
    {
        TResult Encrypt(string line, RSAParameters publicKey);
    }
}