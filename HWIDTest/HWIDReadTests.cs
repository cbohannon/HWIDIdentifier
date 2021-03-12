using Microsoft.VisualStudio.TestTools.UnitTesting;
using HWIDIdentifier;

namespace HWIDTest
{
    [TestClass]
    public class HWIDReadTests
    {
        [TestMethod]
        public void HWIDReadNotNull()
        {
            // This isn't a great test but the return value will be different on different computers
            Assert.IsNotNull(HWIDIdentifier.ReadHelper.HWID.GetValue());
        }
        [TestMethod]
        public void HWIDReadValidLength()
        {
            // The basic GUID is 36 characters (hyphenated) plus two characters for curly brackets
            const sbyte guidLength = 38;

            Assert.AreEqual(guidLength, HWIDIdentifier.ReadHelper.HWID.GetValue().Length);
        }
        [TestMethod]
        public void PCGuidReadNotNull()
        {
            // Same as above, let's just ensure the return is not null
            Assert.IsNotNull(HWIDIdentifier.ReadHelper.PCGuid.GetValue());
        }
        [TestMethod]
        public void PCGuidReadValidLength()
        {
            // The basic GUID is 36 characters (hyphenated)
            const sbyte guidLength = 36;

            Assert.AreEqual(guidLength, HWIDIdentifier.ReadHelper.PCGuid.GetValue().Length);
        }
        [TestMethod]
        public void PCNameNotNull()
        {
            // The PC Name can really be anything so we'll just check NOT null
            Assert.IsNotNull(HWIDIdentifier.ReadHelper.PCName.GetValue());
        }
        [TestMethod]
        public void ProductIdNotNull()
        {
            // Same as above, let's just ensure the return is not null
            Assert.IsNotNull(HWIDIdentifier.ReadHelper.ProductId.GetValue());
        }
        [TestMethod]
        public void ProductIdValidLength()
        {
            // The length is 20 plus three hyphens
            const sbyte idLength = 23;
         
            Assert.AreEqual(idLength, HWIDIdentifier.ReadHelper.ProductId.GetValue().Length);
        }
        [TestMethod]
        public void WindowsProductKeyValidLength()
        {
            // A valid Windows product key is 29 characters (hyphenated)
            const sbyte productKeyLength = 29;
            
            Assert.AreEqual(productKeyLength, HWIDIdentifier.ReadHelper.GetWindowsProductKey().Length);
        }
    }
}
