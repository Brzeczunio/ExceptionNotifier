using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Crytpo;
using System;
using System.Runtime.InteropServices;

namespace Queris.ExceptionNotifier.CryptoDecoder
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        internal static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        internal static extern bool SetClipboardData(uint uFormat, IntPtr data);

        private static readonly ICrypto CryptoEncoder = new Crypto();

        [STAThread]
        static void Main()
        {
            Console.Write(@"Write encrypted password: ");

            var encodePassword = Console.ReadLine();
            var decodedPassword = CryptoEncoder.Decode(encodePassword);

            OpenClipboard(IntPtr.Zero);
            var ptr = Marshal.StringToHGlobalUni(decodedPassword);
            SetClipboardData(13, ptr);
            CloseClipboard();
            Marshal.FreeHGlobal(ptr);

            Console.WriteLine($@"Your decoded password: {decodedPassword}");
            Console.WriteLine($@"{Environment.NewLine}Password was copied to clipboard{Environment.NewLine}{Environment.NewLine}Press enter to exit..");
            Console.ReadKey();
        }
    }
}
