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
        public void WriteError()
        {
            GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(@"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\000001");
            string profileKey = "HwProfileGuid";

            Assert.AreEqual(regeditObject.Write(profileKey, "Some bogus value."), "Error - Subkey not found.");
        }
    }
}
