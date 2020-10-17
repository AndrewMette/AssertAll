using System;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using AssertAllTests.ExtensionMethods;
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
            AssertAllInconclusiveException ex =
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
        public void IncludeAllFailureAndInconclusiveMessagesOnTheirOwnLine()
        {
            var message1 = RandomValue.String();
            var message2 = RandomValue.String();
            var message3 = RandomValue.String();

            AssertAll.IsTrue(true, message1);
            AssertAll.Fail(message2);
            AssertAll.Inconclusive(message3);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());

            string[] messageLines = ex.Message.Split(Environment.NewLine);

            Assert.That.ArrayLacksStringContaining(message1, messageLines, message: "Unexpected message from passed assertion found in results");
            Assert.That.ArrayHasDiscreteStringsContaining(
                    new string[] { message2, message3},
                    messageLines,
                    message: "Messages from failed assertions are missing or do not appear on their own lines");
        }

        //TODO: Brittle test, possibly mislabelled or is testing incomplete feature
        [TestMethod]
        public void ModifyStackTraceOfException()
        {
            AssertAll.AreEqual(1, 2);
            AssertAll.IsFalse(true);
            AssertAll.IsNull(1);

            var thrownException = Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());

            Assert.IsTrue(thrownException.StackTrace.ToLower().Contains("line 68"), thrownException.StackTrace);
            Assert.IsTrue(thrownException.StackTrace.ToLower().Contains("line 69"));
            Assert.IsTrue(thrownException.StackTrace.ToLower().Contains("line 70"));
            Assert.Inconclusive("This test is sensitive to unrelated class changes, and may be incomplete");
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
