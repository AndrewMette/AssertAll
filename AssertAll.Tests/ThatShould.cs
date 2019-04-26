using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests
{
    [TestClass]
    public class ThatShould
    {
        [TestMethod]
        public void PassWhenExtensionMethodPasses()
        {
            AssertAll.That.TestMethod(true);

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenExtensionMethodFails()
        {
            AssertAll.That.TestMethod(false);

            Assert.ThrowsException<AssertFailedException>(() => AssertAll.Execute());
        }
    }

    internal static class ExtensionMethods
    {
        internal static void TestMethod(this AssertAll source, bool passes)
        {
            if (passes == false)
            {
                AssertAll.Fail();
            }
        }
    }
}
