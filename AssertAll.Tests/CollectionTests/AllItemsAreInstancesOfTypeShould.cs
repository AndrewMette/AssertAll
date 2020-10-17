using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
{
    [TestClass]
    public class AllItemsAreInstancesOfTypeShould : TestBase
    {
        [TestMethod]
        public void FailWhenNotOfType()
        {
            var list1 = new List<string> {"1"};
            string message = "list contains items of wrong type";
            AssertAll.Collections.AllItemsAreInstancesOfType(list1, typeof(char), message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Collections.AllItemsAreInstancesOfType", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void PassWhenChildType()
        {
            var list1 = new List<ChildType> { new ChildType() };
            AssertAll.Collections.AllItemsAreInstancesOfType(list1, typeof(ParentType));
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenParentType()
        {
            var list1 = new List<ParentType> { new ParentType() };
            string message = "list contains items of wrong type";
            AssertAll.Collections.AllItemsAreInstancesOfType(list1, typeof(ChildType), message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Collections.AllItemsAreInstancesOfType", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void PassWhenOfType()
        {
            var list1 = new List<string> { "1", "2" };
            AssertAll.Collections.AllItemsAreInstancesOfType(list1, typeof(string));
            AssertAll.Execute();
        }

        public class ParentType { }
        public class ChildType : ParentType { }
    }
}
