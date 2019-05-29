using System.Collections.Generic;
using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests.CollectionTests
{
    [TestClass]
    public class AllItemsAreInstancesOfTypeShould : TestBase
    {
        [TestMethod]
        public void FailWhenNotOfType()
        {
            var list1 = new List<string> {"1"};
            AssertAll.Collections.AllItemsAreInstancesOfType(list1, typeof(char));

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
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
            AssertAll.Collections.AllItemsAreInstancesOfType(list1, typeof(ChildType));

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
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
