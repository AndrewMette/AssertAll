using System;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class ExecuteShould : TestBase
    {
        [TestMethod]
        public void PassWhenThereAreNoExceptionsOrInconclusives()
        {
            AssertAll.IsFalse(false);
            AssertAll.AreEqual(1, 1);
            AssertAll.IsInstanceOfType(1, typeof(int));
            AssertAll.Execute();
        }

        [TestMethod]
        public void ThrowAssertInconclusiveExceptionWhenInconclusive()
        {
            AssertAll.IsFalse(false);
            AssertAll.AreEqual(1, 1);
            AssertAll.Inconclusive();
            Assert.ThrowsException<AssertAllInconclusiveException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenThereAreFailures()
        {
            AssertAll.IsFalse(false);
            AssertAll.Inconclusive();
            AssertAll.Fail();
            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void AllFailureAndInconclusiveMessagesAreIncluded()
        {
            var message1 = RandomValue.String();
            var message2 = RandomValue.String();
            var message3 = RandomValue.String();

            AssertAll.IsTrue(true, message1);
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

        [TestMethod]
        public void ModifyStackTraceOfException()
        {
            AssertAll.AreEqual(1, 2);
            AssertAll.IsFalse(true);
            AssertAll.IsNull(1);

            var thrownException = Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());

            Assert.IsTrue(thrownException.StackTrace.ToLower().Contains("line 66"), thrownException.StackTrace);
            Assert.IsTrue(thrownException.StackTrace.ToLower().Contains("line 67"));
            Assert.IsTrue(thrownException.StackTrace.ToLower().Contains("line 68"));
        }

        [TestMethod]
        public void NotCareIfStructValueChanged()
        {
            var testBool = true;
            AssertAll.IsTrue(testBool);
            testBool = false;
        }

        [TestMethod]
        public void NotCareIfClassValuesChanged()
        {
            var testInstance = new TestClass { Value = "stuff" };
            AssertAll.IsNotNull(testInstance);
            AssertAll.IsNotNull(testInstance?.Value);
            testInstance = null;
        }
        public class TestClass
        {
            public object Value { get; set; }
        }
    }
}
