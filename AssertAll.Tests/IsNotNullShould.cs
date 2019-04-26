using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests
{
    [TestClass]
    public class IsNotNullShould
    {
        [TestMethod]
        public void PassWhenNull()
        {
            AssertAll.IsNotNull(1);
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenNotNull()
        {
            AssertAll.IsNotNull(null);

            Assert.ThrowsException<AssertFailedException>(() => AssertAll.Execute());
        }
    }
}
