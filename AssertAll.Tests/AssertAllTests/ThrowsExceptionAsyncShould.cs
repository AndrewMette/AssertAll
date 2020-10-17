using System;
using System.Threading.Tasks;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
{
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
            string message = "exception not thrown";
            AssertAll.ThrowsExceptionAsync<NotImplementedException>(async () => await ThrowContrivedException(false), message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.ThrowsException", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void FailWhenWrongExceptionIsThrown()
        {
            string message = "incorrect exception thrown";
            AssertAll.ThrowsExceptionAsync<NullReferenceException>(async () => await ThrowContrivedException(true), message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.ThrowsException", "Failure message assertion name was not altered");
            StringAssert.Contains(ex.Message, $"{message}{Environment.NewLine}Exception Message:", "Failure message missing or followed by unexpected text");
        }

        private static async Task ThrowContrivedException(bool throwException)
        {
            if (throwException)
            {
                throw new NotImplementedException();
            }
        }
    }
}
