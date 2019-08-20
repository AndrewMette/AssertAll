using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAllTests.StringTests
{
    [TestClass]
    public class ContainsShould : TestBase
    {
        [TestMethod]
        public void PassWhenContains()
        {
            var start = RandomValue.String();
            var middle = RandomValue.String();
            var end = RandomValue.String();
            AssertAll.Strings.Contains($"{start}{middle}{end}", middle);

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenDoesNotContain()
        {
            AssertAll.Strings.Contains(RandomValue.String(), RandomValue.String());

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}
