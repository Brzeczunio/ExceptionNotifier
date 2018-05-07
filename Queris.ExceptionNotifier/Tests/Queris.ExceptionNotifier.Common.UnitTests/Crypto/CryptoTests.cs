using FluentAssertions;
using NUnit.Framework;

namespace Queris.ExceptionNotifier.Common.UnitTests.Crypto
{
    [TestFixture]
    [Category("Queris.ExceptionNotifier.Common.UnitTests")]
    public class CryptoTests
    {
        [Test]
        [Category("Crypto.Decode.UnitTests")]
        public void Crypto_Decode_ReturnDecodedPassword()
        {
            const string expected = "haslotestowe";
            const string encodedPassword = "+hAXpd+5jDY50Ekni6elPQ==";
            var crypto = new Crytpo.Crypto();

            var actual = crypto.Decode(encodedPassword);

            actual.Should().Match(expected);
        }

        [Test]
        [Category("Crypto.Encode.UnitTests")]
        public void Crypto_Encode_ReturnEncodedPassword()
        {
            const string expected = "+hAXpd+5jDY50Ekni6elPQ==";
            const string password = "haslotestowe";
            var crypto = new Crytpo.Crypto();

            var actual = crypto.Encode(password);

            actual.Should().Match(expected);
        }

        [Test]
        [Category("Crypto.Decode.UnitTests")]
        public void Crypto_DecodeNullEncodedPassword_ReturnNull()
        {
            const string encodedPassword = null;
            var crypto = new Crytpo.Crypto();

            var actual = crypto.Decode(encodedPassword);

            actual.Should().BeNull("because the value is null");
        }

        [Test]
        [Category("Crypto.Encode.UnitTests")]
        public void Crypto_EncodeNukkPassword_ReturnNull()
        {
            const string password = null;
            var crypto = new Crytpo.Crypto();

            var actual = crypto.Encode(password);

            actual.Should().BeNull("because the value is null");
        }
    }
}
