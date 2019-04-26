using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests
{
    [TestClass]
    public class IsNullShould
    {
        [TestMethod]
        public void PassWhenNull()
        {
            AssertAll.IsNull(null);
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenNotNull()
        {
            AssertAll.IsNull(1);

            Assert.ThrowsException<AssertFailedException>(() => AssertAll.Execute());
        }
    }
}
