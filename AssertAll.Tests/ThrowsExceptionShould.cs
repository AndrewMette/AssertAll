using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests
{
    [TestClass]
    public class ThrowsExceptionShould
    {
        [TestMethod]
        public void PassWhenExceptionOfCorrectTypeIsThrown()
        {
            var emptyList = new List<object>();
            AssertAll.ThrowsException<InvalidOperationException>(() => emptyList.Single());

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenNoExceptionIsThrown()
        {
            var list = new List<object>(){new object()};
            AssertAll.ThrowsException<InvalidOperationException>(() => list.Single());

            Assert.ThrowsException<AssertFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenWrongExceptionIsThrown()
        {
            var emptyList = new List<object>();
            AssertAll.ThrowsException<NullReferenceException>(() => emptyList.Single());

            Assert.ThrowsException<AssertFailedException>(() => AssertAll.Execute());
        }
    }
}