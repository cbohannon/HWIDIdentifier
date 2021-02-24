﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWIDIdentifier
{
    class WriteHelper
    {
        public static class HWID
        {
            public static GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(@"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001");
            public static readonly string profileKey = "HwProfileGuid";
            public static string SetValue(object value)
            {
                return regeditObject.Write(profileKey, value);
            }
            public static string SpoofHWID()
            {                
                return SetValue("{" + Guid.NewGuid().ToString() + "}"); ;
            }
        }
        public static class PCGuid
        {
            public static GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(@"SOFTWARE\Microsoft\Cryptography");
            public static readonly string machineKey = "MachineGuid";
            public static string SetValue(object value)
            {
                return regeditObject.Write(machineKey, value);
            }
            public static string SpoofPCGuid()
            {
                return SetValue(Guid.NewGuid().ToString());
            }
        }
        public static class PCName
        {
            public static GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(@"SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName");
            public static readonly string nameKey = "ComputerName";
            public static string SetValue(object value)
            {
                return regeditObject.Write(nameKey, value);
            }
            public static string SpoofPCName()
            {
                return SetValue("DESKTOP-" + GenericHelper.RandomGenerator.GenerateString(15));
            }
        }
    }
}
