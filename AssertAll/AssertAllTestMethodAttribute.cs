using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllNuget
{
    public class AssertAllTestMethodAttribute : TestMethodAttribute
    {
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            AssertAll.ReadyForUsage = true;
            AssertAll.RegisteredAssertStatements.Clear();

            var results = base.Execute(testMethod).ToList();
            var firstResult = results.First();

            if (firstResult.TestFailureException == null || firstResult.TestFailureException?.InnerException is UnitTestAssertException)
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                try
                {
                    AssertAll.Execute();
                }
                catch (AssertAllException exception)
                {
                    stopWatch.Stop();
                    var result = new TestResult
                    {
                        Duration = stopWatch.Elapsed,
                        Outcome = exception.GetType() == typeof(AssertAllFailedException)
                            ? UnitTestOutcome.Failed
                            : UnitTestOutcome.Inconclusive,
                        TestFailureException = exception
                    };

                    if (firstResult.Outcome == UnitTestOutcome.Passed)
                    {
                        results = new List<TestResult>
                        {
                            result
                        };
                    }
                    else
                    {
                        results.Add(result);
                    }
                }
                finally
                {
                    if (stopWatch.IsRunning)
                    {
                        stopWatch.Stop();
                    }
                }
            }

            AssertAll.ReadyForUsage = false;
            return results.ToArray();
        }
    }
}
