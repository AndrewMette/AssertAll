using System;
using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAllTests.ExtensionMethodTests
{
    [TestClass]
    public class ExceptionHasInnerExceptionShould : TestBase
    {
        [TestMethod]
        public void PassWhenInnerExceptionIsOfCorrectType()
        {
            AssertAll.ThrowsExceptionWithInnerException<InvalidOperationException>(() =>
                ThrowExceptionWithInnerInvalidOperationException(true));

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenInnerExceptionIsNotCorrectType()
        {
            AssertAll.ThrowsExceptionWithInnerException<ArgumentException>(() =>
                ThrowExceptionWithInnerInvalidOperationException(true));

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenNoExceptionIsThrown()
        {
            var list = new List<object>() { new object() };
            AssertAll.ThrowsExceptionWithInnerException<InvalidOperationException>(() =>
                ThrowExceptionWithInnerInvalidOperationException(false));

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenInnerExceptionIsNull()
        {
            AssertAll.ThrowsExceptionWithInnerException<ArgumentException>(() => 
                throw new Exception(RandomValue.String()));

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }


        private void ThrowExceptionWithInnerInvalidOperationException(bool shouldThrow)
        {
            if (shouldThrow)
            {
                throw new Exception(RandomValue.String(), new InvalidOperationException());
            }
        }
    }
}
