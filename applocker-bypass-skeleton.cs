using System;
using System.Collections;
using System.Data.SqlClient;

namespace CLMBypass_SQLDirtree
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    [System.ComponentModel.RunInstaller(true)]
    public class Sample : System.Configuration.Install.Installer
    {
        public override void Uninstall(IDictionary savedState)
        {
            // Enter your normal code flow here. When running the following command, it will execute code within this class vs Program.Main
            // C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\InstallUtil.exe /logfile= /LogToConsole=false /U C:\\windows\\temp\\clmBypass.exe
        }
    }
}
