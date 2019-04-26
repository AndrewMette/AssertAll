using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests
{
    [TestClass]
    public class IsNotInstanceOfTypeShould
    {
        [TestMethod]
        public void PassWhenTypesAreTheSame()
        {
            AssertAll.IsNotInstanceOfType("1", typeof(int));
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenTypesAreNotTheSame()
        {
            AssertAll.IsNotInstanceOfType(1, typeof(int));

            Assert.ThrowsException<AssertFailedException>(() => AssertAll.Execute());
        }
    }
}
