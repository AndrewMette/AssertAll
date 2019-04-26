using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests
{
    [TestClass]
    public class IsInstanceOfTypeShould
    {
        [TestMethod]
        public void PassWhenTypesAreTheSame()
        {
            AssertAll.IsInstanceOfType(1, typeof(int));
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenTypesAreNotTheSame()
        {
            AssertAll.IsInstanceOfType("1", typeof(int));

            Assert.ThrowsException<AssertFailedException>(() => AssertAll.Execute());
        }
    }
}
