﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using HWIDIdentifier;
using System;

namespace HWIDTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class GenericHelperTest
    {
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
        public void WriteNotNull()
        {
            GenericHelper.Regedit regeditObject = new GenericHelper.Regedit(@"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001");
            string profileKey = "HwProfileGuid";

            Assert.IsNotNull(regeditObject.Write(profileKey, "{" + Guid.NewGuid().ToString() + "}"));
        }
    }
}
