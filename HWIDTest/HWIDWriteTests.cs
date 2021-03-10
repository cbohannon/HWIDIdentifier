using Microsoft.VisualStudio.TestTools.UnitTesting;
using HWIDIdentifier;

namespace HWIDTest
{
    [TestClass]
    public class HWIDWriteTests
    {
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
    }
}
