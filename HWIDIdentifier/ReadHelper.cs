using System;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

[assembly: InternalsVisibleTo("HWIDTest")]
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
        public static string DecodeProductKeyWin8AndUp(byte[] digitalProductId)
        {
            // https://stackoverflow.com/questions/10926634/how-can-i-get-windows-product-key-in-c
            string key = null;
            const int keyOffset = 52;
            byte isWin8 = (byte)((digitalProductId[66] / 6) & 1);
            digitalProductId[66] = (byte)((digitalProductId[66] & 0xf7) | (isWin8 & 2) * 4);

            // Possible alpha-numeric characters in product key.
            const string digits = "BCDFGHJKMPQRTVWXY2346789";
            int last = 0;
            for (int i = 24; i >= 0; i--)
            {
                int current = 0;
                for (int j = 14; j >= 0; j--)
                {
                    current *= 256;
                    current = digitalProductId[j + keyOffset] + current;
                    digitalProductId[j + keyOffset] = (byte)(current / 24);
                    current %= 24;
                    last = current;
                }
                key = digits[current] + key;
            }
            string keypart1 = key.Substring(1, last);
            const string insert = "N";

            if (keypart1.Length != 0) // There seems to be a difference in how keys are provided in a personal use and enterprise use
            {
                key = key.Substring(1).Replace(keypart1, keypart1 + insert);

                if (last == 0)
                {
                    key = insert + key;
                }
            }

            for (int i = 5; i < key.Length; i += 6)
            {
                key = key.Insert(i, "-");
            }

            return key;
        }
        public static string GetWindowsProductKey()
        {
            // https://stackoverflow.com/questions/10926634/how-can-i-get-windows-product-key-in-c
            const string keyPath = @"Software\Microsoft\Windows NT\CurrentVersion";
            using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default))
            using (RegistryKey subKey = key.OpenSubKey(keyPath))
            {
                if (subKey == null)
                    return "Error - Subkey not found.";

                byte[] digitalProductId = (byte[])subKey.GetValue("DigitalProductId");
                if (digitalProductId == null)
                    return "Error - Value not found.";

                string productKey = null;
                switch (Environment.OSVersion.Version.Major)
                {
                    case 6: // I don't have anyway to test on anything other than Windows 10
                        productKey = DecodeProductKeyWin8AndUp(digitalProductId);
                        break;
                    case 10: // I don't have anyway to test on anything other than Windows 10
                        productKey = DecodeProductKeyWin8AndUp(digitalProductId);
                        break;
                    default:
                        break;
                }

                return productKey;
            }
        }
    }
}
