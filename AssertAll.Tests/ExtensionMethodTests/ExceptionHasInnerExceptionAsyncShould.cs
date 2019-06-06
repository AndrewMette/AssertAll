using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAll.Tests.ExtensionMethodTests
{
    [TestClass]
    public class ExceptionHasInnerExceptionAsyncShould : TestBase
    {
        [TestMethod]
        public void PassWhenInnerExceptionIsOfCorrectType()
        {
            AssertAll.ThrowsExceptionWithInnerExceptionAsync<InvalidOperationException>(async () =>
                await ThrowExceptionWithInnerInvalidOperationException(true));

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenInnerExceptionIsNotCorrectType()
        {
            AssertAll.ThrowsExceptionWithInnerExceptionAsync<ArgumentException>(async () =>
                await ThrowExceptionWithInnerInvalidOperationException(true));

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenNoExceptionIsThrown()
        {
            var list = new List<object>() { new object() };
            AssertAll.ThrowsExceptionWithInnerExceptionAsync<InvalidOperationException>(async () =>
                ThrowExceptionWithInnerInvalidOperationException(false));

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenInnerExceptionIsNull()
        {
            AssertAll.ThrowsExceptionWithInnerExceptionAsync<ArgumentException>(async () => 
                await ThrowNewException());

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }


        private async Task ThrowExceptionWithInnerInvalidOperationException(bool shouldThrow)
        {
            if (shouldThrow)
            {
                throw new Exception(RandomValue.String(), new InvalidOperationException());
            }
        }

        private async Task ThrowNewException()
        {
            throw new Exception();
        }
    }
}
