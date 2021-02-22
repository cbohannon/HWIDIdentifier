using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace HWIDIdentifier
{
    class Helper
    {
        public static class HWID
        {
            public static Regedit regeditObject = new Regedit(@"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001");
            public static readonly string profileKey = "HwProfileGuid";

            public static string GetValue()
            {
                return regeditObject.Read(profileKey);
            }
        }

        public static class PCGuid
        {
            // Note that in the HWIDIdentifier project poperties, I had to uncheck Prefer 32-bit for this method call to work properly
            public static Regedit regeditObject = new Regedit(@"SOFTWARE\Microsoft\Cryptography");
            public static readonly string machineKey = "MachineGuid";

            public static string GetValue()
            {
                return regeditObject.Read(machineKey);
            }
        }
        public class Regedit
        {
            private string regeditPath = string.Empty;
            public Regedit(string regeditPath)
            {
                this.regeditPath = regeditPath;
            }

            public string Read(string keyName)
            {
                try
                {
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regeditPath))
                    {
                        if (key != null)
                        {
                            return key.GetValue(keyName).ToString();
                        }
                        else
                        {
                            return "Error - Subkey not found.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "Error - " + ex.Message;
                }
            }
        }
    }
}
