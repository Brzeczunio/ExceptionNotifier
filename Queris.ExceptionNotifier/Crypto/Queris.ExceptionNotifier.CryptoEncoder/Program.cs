using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Crytpo;
using System;
using System.Runtime.InteropServices;

namespace Queris.ExceptionNotifier.CryptoEncoder
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
            Console.Write(@"Write password: ");

            var password = Console.ReadLine();
            var encodedPassword = CryptoEncoder.Encode(password);

            OpenClipboard(IntPtr.Zero);
            var ptr = Marshal.StringToHGlobalUni(encodedPassword);
            SetClipboardData(13, ptr);
            CloseClipboard();
            Marshal.FreeHGlobal(ptr);

            Console.WriteLine($@"Your encoded password: {encodedPassword}");
            Console.WriteLine($@"{Environment.NewLine}Password was copied to clipboard{Environment.NewLine}{Environment.NewLine}Press enter to exit..");
            Console.ReadKey();
        }
    }
}
