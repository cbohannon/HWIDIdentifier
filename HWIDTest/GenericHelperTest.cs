using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using HWIDIdentifier;
using System;
using Microsoft.Win32;

namespace HWIDTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class GenericHelperTest
    {
        private const string testKeyPath = @"Software\HWIDIdentifierTest";
        private const string testValueName = "TestValue";

        [TestInitialize]
        public void TestInitialize()
        {
            Registry.CurrentUser.CreateSubKey(testKeyPath);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Registry.CurrentUser.DeleteSubKey(testKeyPath, false);
        }
        [TestMethod]
        public void RandomGeneratorNotNull()
        {
            Assert.IsNotNull(GenericHelper.RandomGenerator.GenerateString(15));
        }
        [TestMethod]
        public void RandomGeneratorCorrectLength()
        {
            const int size = 15;
            Assert.AreEqual(size, GenericHelper.RandomGenerator.GenerateString(size).Length);
        }
        [TestMethod]
        public void RandomGeneratorValidCharacters()
        {
            string result = GenericHelper.RandomGenerator.GenerateString(100);
            foreach (char c in result)
            {
                Assert.IsTrue(GenericHelper.RandomGenerator.alphaNumeric1.Contains(c));
            }
        }
        [TestMethod]
        public void ReadNotNull()
        {
            GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(@"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001");
            string profileKey = "HwProfileGuid";

            Assert.IsNotNull(regeditObject.Read(profileKey));
        }
        [TestMethod]
        public void ReadError()
        {
            GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(@"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\000001");
            string profileKey = "HwProfileGuid";

            Assert.AreEqual(regeditObject.Read(profileKey), "Error - Subkey not found.");
        }
        [TestMethod]
        public void ReadErrorException()
        {
            GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(@"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001");
            string profileKey = "XHwProfileGuidX";

            Assert.IsTrue(regeditObject.Read(profileKey).Contains("Error - "));
        }
        [TestMethod]
        public void WriteNotNull()
        {
            GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(testKeyPath, RegistryHive.CurrentUser);

            Assert.IsNotNull(regeditObject.Write(testValueName, "{" + Guid.NewGuid().ToString() + "}"));
        }
        [TestMethod]
        public void WriteReadBack()
        {
            GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(testKeyPath, RegistryHive.CurrentUser);
            string testValue = "{" + Guid.NewGuid().ToString() + "}";

            regeditObject.Write(testValueName, testValue);

            Assert.AreEqual(testValue, regeditObject.Read(testValueName));
        }
        [TestMethod]
        public void WriteError()
        {
            GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(@"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\000001");
            string profileKey = "HwProfileGuid";

            Assert.AreEqual(regeditObject.Write(profileKey, "Some bogus value."), "Error - Subkey not found.");
        }
    }
}
