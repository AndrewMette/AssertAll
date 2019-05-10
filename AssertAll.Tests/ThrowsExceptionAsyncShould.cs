using System;
using System.Threading.Tasks;
using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests
{
    [Ignore]
    [TestClass]
    public class ThrowsExceptionAsyncShould : TestBase
    {
        [TestMethod]
        public void PassWhenExceptionOfCorrectTypeIsThrown()
        {
            AssertAll.ThrowsExceptionAsync<NotImplementedException>(async () => await ThrowContrivedException(true));
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenNoExceptionIsThrown()
        {
            AssertAll.ThrowsExceptionAsync<NotImplementedException>(async () => await ThrowContrivedException(false));

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenWrongExceptionIsThrown()
        {
            AssertAll.ThrowsExceptionAsync<NullReferenceException>(async () => await ThrowContrivedException(true));

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        private async Task ThrowContrivedException(bool throwException)
        {
            if (throwException)
            {
                throw new NotImplementedException();
            }
        }
}
}
