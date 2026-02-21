using System;
using Microsoft.Win32;

namespace HWIDIdentifier
{
    class GenericHelper
    {
        public class Regedit
        {
            private string regeditPath = string.Empty;
            private RegistryHive registryHive;
            public Regedit(string regeditPath, RegistryHive hive = RegistryHive.LocalMachine)
            {
                this.regeditPath = regeditPath;
                this.registryHive = hive;
            }
            public string Read(string keyName)
            {
                try
                {
                    using (RegistryKey baseKey = RegistryKey.OpenBaseKey(registryHive, RegistryView.Default))
                    using (RegistryKey key = baseKey.OpenSubKey(regeditPath))
                    {
                        if (key != null)
                        {
                            object value = key.GetValue(keyName);
                            if (value == null)
                                return "Error - Value not found.";
                            return value.ToString();
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
            public string Write(string keyName, object value)
            {
                try
                {
                    using (RegistryKey baseKey = RegistryKey.OpenBaseKey(registryHive, RegistryView.Default))
                    using (RegistryKey key = baseKey.OpenSubKey(regeditPath, true))
                    {
                        if (key != null)
                        {
                            key.SetValue(keyName, value);
                            return value.ToString();
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
        public static class RandomGenerator
        {
            private static Random random1 = new Random();
            public const string alphaNumeric1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // BCDFGHJKMPQRTVWXY2346789
            private static Random random2 = new Random();
            public const string alphaNumeric2 = "abcdefghijklmnopqrstuvwxyz0123456789"; // bcdfghjkmpqrtvwxy2346789

            public static string GenerateString(int size)
            {
                char[] array = new char[size];
                for (int i = 0; i < size; i++)
                {
                    array[i] = alphaNumeric1[random1.Next(alphaNumeric1.Length)];
                }
                return new string(array);
            }
        }
    }
}
