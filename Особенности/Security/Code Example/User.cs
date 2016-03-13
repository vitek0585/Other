using System;
using System.Security.Cryptography;
using Crypt.Crypt;
using Crypt.Crypt.Interfaces;

namespace Crypt
{
    public class User<TResult>
    {
        private IHybridEncrypt<TResult> _encrypt;
        public User(IHybridEncrypt<TResult> encrypt)
        {
            _encrypt = encrypt;
        }

        public TResult CreateRequest(RSAParameters publicKey)
        {
            var line = Console.ReadLine();
            return _encrypt.Encrypt(line, publicKey);
        }
    }


}