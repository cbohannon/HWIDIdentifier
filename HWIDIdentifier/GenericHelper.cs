using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace HWIDIdentifier
{
    class GenericHelper
    {
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
        public static class RandomGenerator
        {
            private static Random random1 = new Random();
            public const string alphaNumeric1 = "ABCDEF0123456789";
            private static Random random2 = new Random();
            public const string alphaNumeric2 = "abcdef0123456789";

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
