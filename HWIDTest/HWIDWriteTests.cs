using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using HWIDIdentifier;
using Microsoft.Win32;

namespace HWIDTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class HWIDWriteTests
    {
        private const string testKeyPath = @"Software\HWIDIdentifierTest";

        private GenericHelper.Regedit originalHWIDRegedit;
        private GenericHelper.Regedit originalPCGuidRegedit;
        private GenericHelper.Regedit originalPCNameRegedit;
        private GenericHelper.Regedit originalProductIdRegedit;

        [TestInitialize]
        public void TestInitialize()
        {
            Registry.CurrentUser.CreateSubKey(testKeyPath);

            originalHWIDRegedit = WriteHelper.HWID.regeditObject;
            originalPCGuidRegedit = WriteHelper.PCGuid.regeditObject;
            originalPCNameRegedit = WriteHelper.PCName.regeditObject;
            originalProductIdRegedit = WriteHelper.ProductId.regeditObject;

            var testRegedit = new GenericHelper.Regedit(testKeyPath, RegistryHive.CurrentUser);
            WriteHelper.HWID.regeditObject = testRegedit;
            WriteHelper.PCGuid.regeditObject = testRegedit;
            WriteHelper.PCName.regeditObject = testRegedit;
            WriteHelper.ProductId.regeditObject = testRegedit;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            WriteHelper.HWID.regeditObject = originalHWIDRegedit;
            WriteHelper.PCGuid.regeditObject = originalPCGuidRegedit;
            WriteHelper.PCName.regeditObject = originalPCNameRegedit;
            WriteHelper.ProductId.regeditObject = originalProductIdRegedit;

            Registry.CurrentUser.DeleteSubKey(testKeyPath, false);
        }

        // Get the orignal value, then create a new value, diff the values, check null, check length
        [TestMethod]
        public void HWIDWriteNotNull()
        {
            Assert.IsNotNull(HWIDIdentifier.WriteHelper.HWID.SpoofHWID());
        }
        [TestMethod]
        public void HWIDWriteValidLength()
        {
            // The basic GUID is 36 characters (hyphenated) plus two characters for curly brackets
            const sbyte guidLength = 38;

            Assert.AreEqual(guidLength, HWIDIdentifier.WriteHelper.HWID.SpoofHWID().Length);
        }
        [TestMethod]
        public void HWIDWriteCompareOldNew()
        {
            Assert.AreNotEqual(HWIDIdentifier.ReadHelper.HWID.GetValue(), HWIDIdentifier.WriteHelper.HWID.SpoofHWID());
        }
        [TestMethod]
        public void PCGuidNotNull()
        {
            Assert.IsNotNull(HWIDIdentifier.WriteHelper.PCGuid.SpoofPCGuid());
        }
        [TestMethod]
        public void PCGuidWriteValidLength()
        {
            const sbyte pcGuidLength = 36;

            Assert.AreEqual(pcGuidLength, HWIDIdentifier.WriteHelper.PCGuid.SpoofPCGuid().Length);
        }
        [TestMethod]
        public void PCGuidWriteCompareOldNew()
        {
            Assert.AreNotEqual(HWIDIdentifier.ReadHelper.PCGuid.GetValue(), HWIDIdentifier.WriteHelper.PCGuid.SpoofPCGuid());
        }
        [TestMethod]
        public void PCNameWriteNotNull()
        {
            Assert.IsNotNull(HWIDIdentifier.WriteHelper.PCName.SpoofPCName());
        }
        [TestMethod]
        public void PCNameWriteValidLength()
        {
            // 15 characters the the maximum length and recommended length
            const sbyte pcNameLength = 15;

            Assert.AreEqual(pcNameLength, HWIDIdentifier.WriteHelper.PCName.SpoofPCName().Length);
        }
        [TestMethod]
        public void PCNameWriteCompareOldNew()
        {
            Assert.AreNotEqual(HWIDIdentifier.ReadHelper.PCName.GetValue(), HWIDIdentifier.WriteHelper.PCName.SpoofPCName());
        }
        [TestMethod]
        public void ProductIdWriteNotNull()
        {
            Assert.IsNotNull(HWIDIdentifier.WriteHelper.ProductId.SpoofProductID());
        }
        [TestMethod]
        public void ProductIdWriteValidLength()
        {
            // The length is 20 plus three hyphens
            const sbyte idLength = 23;

            Assert.AreEqual(idLength, HWIDIdentifier.WriteHelper.ProductId.SpoofProductID().Length);
        }
        [TestMethod]
        public void ProductIdWriteCompareOldNew()
        {
            Assert.AreNotEqual(HWIDIdentifier.ReadHelper.ProductId.GetValue(), HWIDIdentifier.WriteHelper.ProductId.SpoofProductID());
        }
    }
}
