using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Helpers;

namespace Queris.ExceptionNotifier.Common.Crytpo
{
    public class Crypto : ICrypto
    {
        public string Decode(string str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : CryptoHelper.Decrypt(str);
        }

        public string Encode(string str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : CryptoHelper.Encrypt(str);
        }
    }
}
