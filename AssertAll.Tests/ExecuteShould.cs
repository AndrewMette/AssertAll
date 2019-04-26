using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAll.Tests
{
    [TestClass]
    public class ExecuteShould
    {
        [TestMethod]
        public void PassWhenThereAreNoExceptionsOrInconclusives()
        {
            AssertAll.IsFalse(false);
            AssertAll.AreEqual(1,1);
            AssertAll.IsInstanceOfType(1, typeof(int));
            AssertAll.Execute();
        }

        [TestMethod]
        public void ThrowAssertInconclusiveExceptionWhenInconclusive()
        {
            AssertAll.IsFalse(false);
            AssertAll.AreEqual(1, 1);
            AssertAll.Inconclusive();
            Assert.ThrowsException<AssertInconclusiveException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenThereAreFailures()
        {
            AssertAll.IsFalse(false);
            AssertAll.Inconclusive();
            AssertAll.Fail();
            Assert.ThrowsException<AssertFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void AllFailureAndInconclusiveMessagesAreIncluded()
        {
            var message1 = RandomValue.String();
            var message2 = RandomValue.String();
            var message3 = RandomValue.String();

            AssertAll.IsTrue(true,message1);
            AssertAll.Fail(message2);
            AssertAll.Inconclusive(message3);

            try
            {
                AssertAll.Execute();
            }
            catch (Exception e)
            {
                var message = e.Message;
                Assert.IsFalse(message.Contains(message1));
                Assert.IsTrue(message.Contains(message2));
                Assert.IsTrue(message.Contains(message3));
            }
        }
    }
}
