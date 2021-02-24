using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace HWIDIdentifier
{
    class ReadHelper
    {
        public static class HWID
        {
            public static GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(@"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001");
            public static readonly string profileKey = "HwProfileGuid";

            public static string GetValue()
            {
                return regeditObject.Read(profileKey);
            }
        }
        public static class PCGuid
        {
            // Note that in the HWIDIdentifier project poperties, I had to uncheck Prefer 32-bit for this method call to work properly
            public static GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(@"SOFTWARE\Microsoft\Cryptography");
            public static readonly string machineKey = "MachineGuid";

            public static string GetValue()
            {
                return regeditObject.Read(machineKey);
            }
        }
        public static class PCName
        {
            public static GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(@"SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName");
            public static readonly string nameKey = "ComputerName";
            public static string GetValue()
            {
                return regeditObject.Read(nameKey);
            }
        }
        public static class ProductId
        {
            public static GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(@"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");
            public static readonly string productKey = "ProductID";
            public static string GetValue()
            {
                return regeditObject.Read(productKey);
            }
        }
    }
}
