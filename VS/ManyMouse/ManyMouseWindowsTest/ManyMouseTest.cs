using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;

namespace ManyMouseWindowsTest
{
    [TestClass]
    public class ManyMouseTest
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool SetDllDirectory(string lpPathName);

        [DllImport("ManyMouse")]
        private static extern int ManyMouse_Init();

        [DllImport("ManyMouse")]
        private static extern IntPtr ManyMouse_DriverName();//The string is in UTF-8 format. 

        [DllImport("ManyMouse")]
        private static extern void ManyMouse_Quit();

        [DllImport("ManyMouse", CharSet = CharSet.Ansi)]
        private static extern IntPtr ManyMouse_DeviceName(uint index);

        //[DllImport("ManyMouse")]
        //private static extern int ManyMouse_PollEvent(ref ManyMouseEvent mouseEvent);

        private static readonly string MANYMOUSEDLL = "ManyMouse";

        private void loadDLL()
        {
            var current = Directory.GetCurrentDirectory();
            var dlldir = current + @"\..\..\..\x64\Release";
            var dll = dlldir + $"\\{MANYMOUSEDLL}.dll";
            var exists = File.Exists(dll);
            SetDllDirectory(dlldir);
        }

        public static string StringFromNativeUtf8(IntPtr nativeUtf8)
        {
            int len = 0;
            while (Marshal.ReadByte(nativeUtf8, len) != 0) ++len;
            if (len == 0) return string.Empty;
            byte[] buffer = new byte[len - 1];
            Marshal.Copy(nativeUtf8, buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer);
        }

        [TestMethod]
        public void GetDeviceNames()
        {
            loadDLL();
            ManyMouse_Quit();

            var numMice = ManyMouse_Init();
            Assert.IsTrue(numMice >= 0);

            var driverName = StringFromNativeUtf8(ManyMouse_DriverName());
            Console.WriteLine(driverName);

            for(var i = 0; i < numMice; i++)
            {
                IntPtr mouseNamePtr = ManyMouse_DeviceName((uint)i);
                Console.WriteLine($"{i}: {Marshal.PtrToStringAnsi(mouseNamePtr)}");
            }

        }

        [TestMethod]
        public void ReleaseDLL()
        {
            var dllSourcePath = Environment.CurrentDirectory + @"\..\..\..\x64\Release\" + $"{MANYMOUSEDLL}.dll";
            Assert.IsTrue(File.Exists(dllSourcePath));

            var destPath = Environment.CurrentDirectory + @"\..\..\..\..\..\Unity\Move\Assets\Plugins";
            Assert.IsTrue(Directory.Exists(destPath));

            File.Copy(dllSourcePath, destPath + $"\\{MANYMOUSEDLL}_64.dll", true);
        }
    }
}