using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests
{
    [Ignore]
    [TestClass]
    public class ThrowsExceptionAsyncShould
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

            Assert.ThrowsException<AssertFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenWrongExceptionIsThrown()
        {
            AssertAll.ThrowsExceptionAsync<NullReferenceException>(async () => await ThrowContrivedException(true));

            Assert.ThrowsException<AssertFailedException>(() => AssertAll.Execute());
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
