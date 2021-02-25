﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
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
                // GetWindowsProductKey();
                return regeditObject.Read(productKey);
            }
        }
        public static string DecodeProductKeyWin8AndUp(byte[] digitalProductId)
        {
            // https://stackoverflow.com/questions/10926634/how-can-i-get-windows-product-key-in-c
            var key = String.Empty;
            const int keyOffset = 52;
            var isWin8 = (byte)((digitalProductId[66] / 6) & 1);
            digitalProductId[66] = (byte)((digitalProductId[66] & 0xf7) | (isWin8 & 2) * 4);

            // Possible alpha-numeric characters in product key.
            const string digits = "BCDFGHJKMPQRTVWXY2346789";
            int last = 0;
            for (var i = 24; i >= 0; i--)
            {
                var current = 0;
                for (var j = 14; j >= 0; j--)
                {
                    current = current * 256;
                    current = digitalProductId[j + keyOffset] + current;
                    digitalProductId[j + keyOffset] = (byte)(current / 24);
                    current = current % 24;
                    last = current;
                }
                key = digits[current] + key;
            }
            var keypart1 = key.Substring(1, last);
            const string insert = "N";
            key = key.Substring(1).Replace(keypart1, keypart1 + insert);
            if (last == 0)
                key = insert + key;
            for (var i = 5; i < key.Length; i += 6)
            {
                key = key.Insert(i, "-");
            }
            return key;
        }
        public static string GetWindowsProductKey()
        {
            // https://stackoverflow.com/questions/10926634/how-can-i-get-windows-product-key-in-c
            var key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default); 
            const string keyPath = @"Software\Microsoft\Windows NT\CurrentVersion";
            var digitalProductId = (byte[])key.OpenSubKey(keyPath).GetValue("DigitalProductId");

            var isWin8OrUp =
                (Environment.OSVersion.Version.Major == 6 && System.Environment.OSVersion.Version.Minor >= 2)
                ||
                (Environment.OSVersion.Version.Major > 6);

            var productKey = isWin8OrUp ? DecodeProductKeyWin8AndUp(digitalProductId) : DecodeProductKeyWin8AndUp(digitalProductId);
            return productKey;
        }
    }
}
