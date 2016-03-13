using System.Security.Cryptography;

namespace Crypt.Crypt.Interfaces
{
    public interface IHybridDecrypt<in TItem>:IHybrid
    {
        string Decrypt(TItem request, RSAParameters privKey);
    }
}