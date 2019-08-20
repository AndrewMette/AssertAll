using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AssertAllNuget.Exceptions;
using AssertAllNuget.ExtensionMethods;
using AssertAllNuget.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Threading;

[assembly:InternalsVisibleTo("AssertAllTests")]
namespace AssertAllNuget
{
    public partial class AssertAll
    {
        #region Properties 
        internal static bool ReadyForUsage = false;
        internal static readonly List<AssertStatement> RegisteredAssertStatements;
        private static TestInfo CurrentTestInfo()
        {
            var stackTrace = new StackTrace(true);

            var frames = stackTrace.GetFrames()?
                .Where(x => x.GetMethod().DeclaringType?.Namespace?.StartsWith("System") == false
                            && x.GetMethod().DeclaringType?.Namespace?.StartsWith("Microsoft") == false
                            && x.GetMethod().DeclaringType?.Namespace != "AssertAllNuget"
                            && x.GetMethod().IsStatic);

            if (frames.Any() == false)
            {
                frames = stackTrace.GetFrames()?
                    .Where(x => x.GetMethod().DeclaringType?.Namespace?.StartsWith("System") == false
                                && x.GetMethod().DeclaringType?.Namespace?.StartsWith("Microsoft") == false
                                && x.GetMethod().DeclaringType?.Namespace != "AssertAllNuget");
            }
            
            var frame = frames.Last();
            var file = frame.GetFileName();
            var lineNumber = frame.GetFileLineNumber();
            var method = frame.GetMethod();

            var nameSpace = method?.DeclaringType?.Namespace;
            var currentTest = $"{nameSpace}.{method?.DeclaringType?.Name}.{method?.Name}";

            return new TestInfo{File = file, LineNumber = lineNumber, TestName = currentTest};
        }

        #endregion

        #region Constructor 
        static AssertAll()
        {
            RegisteredAssertStatements = new List<AssertStatement>();
        }
        #endregion

        #region Execute 
        internal static void Execute()
        {
            var assertExceptions = new List<UnitTestAssertException>();
            var failure = false;
            List<string> stackTraceItems = new List<string>();
            foreach (var registeredAssertStatement in RegisteredAssertStatements)
            {
                try
                {
                    if (registeredAssertStatement.Action != null)
                    {
                        registeredAssertStatement.Action.Invoke();
                    }
                    else
                    {
                        var context = new JoinableTaskContext();
                        var factory = new JoinableTaskFactory(context);
                        factory.Run(async delegate
                        {
                            await registeredAssertStatement.Func.Invoke();
                        });
                    }
                }
                catch (AssertFailedException e)
                {
                    assertExceptions.Add(e);
                    var stackTraceLine =
                        $"at {registeredAssertStatement.TestName}() in {registeredAssertStatement.File}:line {registeredAssertStatement.LineNumber}";
                    stackTraceItems.Add(stackTraceLine);
                    failure = true;
                }
                catch (AssertInconclusiveException e)
                {
                    assertExceptions.Add(e);
                }
            }

            RegisteredAssertStatements.Clear();

            if (assertExceptions.Count > 0)
            {
                var allMessages = new List<string>();
                var counter = 1;
                foreach (var assertException in assertExceptions)
                {
                    string replaced;
                    if (assertException.Message.Contains("CollectionAssert"))
                    {
                        replaced = assertException.Message.Replace("CollectionAssert.", "AssertAll.Collections.");
                    }
                    else
                    {
                        replaced = assertException.Message.Replace("Assert.", "AssertAll.");
                    }
                    allMessages.Add($"({counter}) {replaced}");
                    counter++;
                }

                var bigMessage = string.Join(" ", allMessages);
                var stackTrace = string.Join(Environment.NewLine, stackTraceItems);
                
                if (failure)
                {
                    throw new AssertAllFailedException(bigMessage, stackTrace);
                }

                throw new AssertAllInconclusiveException(bigMessage, stackTrace);
            }
        }
        #endregion

        #region Assert Methods 
        public static void AreEqual(object expected, object actual, string message = null)
        {
            if (message == null)
            {
                RegisterAction(() => Assert.AreEqual(expected, actual));
            }
            else
            {
                RegisterAction(() => Assert.AreEqual(expected, actual, message));
            }
        }

        public static void AreNotEqual(object notExpected, object actual, string message = null)
        {
            if (message == null)
            {
                RegisterAction(() => Assert.AreNotEqual(notExpected, actual));
            }
            else
            {
                RegisterAction(() => Assert.AreNotEqual(notExpected, actual, message));
            }
        }

        public static void AreSame(object expected, object actual, string message = null)
        {
            if (message == null)
            {
                RegisterAction(() => Assert.AreSame(expected, actual));
            }
            else
            {
                RegisterAction(() => Assert.AreSame(expected, actual, message));
            }
        }

        public static void AreNotSame(object notExpected, object actual, string message = null)
        {
            if (message == null)
            {
                RegisterAction(() => Assert.AreNotSame(notExpected, actual));
            }
            else
            {
                RegisterAction(() => Assert.AreNotSame(notExpected, actual));
            }
        }

        public static void Fail(string message = null)
        {
            if (message == null)
            {
                RegisterAction(Assert.Fail);
            }
            else
            {
                RegisterAction(() => Assert.Fail(message));
            }
        }

        public static void Inconclusive(string message = null)
        {
            if (message == null)
            {
                RegisterAction(Assert.Inconclusive);
            }
            else
            {
                RegisterAction(() => Assert.Inconclusive(message));
            }
        }

        public static void IsInstanceOfType(object value, Type expectedType, string message = null)
        {
            if (message == null)
            {
                RegisterAction(() => Assert.IsInstanceOfType(value, expectedType));
            }
            else
            {
                RegisterAction(() => Assert.IsInstanceOfType(value, expectedType, message));
            }
        }

        public static void IsNotInstanceOfType(object value, Type wrongType, string message = null)
        {
            if (message == null)
            {
                RegisterAction(() => Assert.IsNotInstanceOfType(value, wrongType));
            }
            else
            {
                RegisterAction(() => Assert.IsNotInstanceOfType(value, wrongType, message));
            }
        }

        public static void IsNull(object value, string message = null)
        {
            if (message == null)
            {
                RegisterAction(() => Assert.IsNull(value));
            }
            else
            {
                RegisterAction(() => Assert.IsNull(value, message));
            }
        }

        public static void IsNotNull(object value, string message = null)
        {
            if (message == null)
            {
                RegisterAction(() => Assert.IsNotNull(value));
            }
            else
            {
                RegisterAction(() => Assert.IsNotNull(value, message));
            }
        }

        public static void IsTrue(bool condition, string message = null)
        {
            if (message == null)
            {
                RegisterAction(() => Assert.IsTrue(condition));
            }
            else
            {
                RegisterAction(() => Assert.IsTrue(condition, message));
            }
        }

        public static void IsFalse(bool condition, string message = null)
        {
            if (message == null)
            {
                RegisterAction(() => Assert.IsFalse(condition));
            }
            else
            {
                RegisterAction(() => Assert.IsFalse(condition, message));
            }
        }

        public static void ThrowsException<T>(Action action, string message = null) where T : Exception
        {
            if (message == null)
            {
                RegisterAction(() => Assert.ThrowsException<T>(action));
            }
            else
            {
                RegisterAction(() => Assert.ThrowsException<T>(action, message));
            }
        }

        public static void ThrowsExceptionWithInnerException<T>(Action action, string message = null) where T : Exception
        {
            if (message == null)
            {
                RegisterAction(() => Assert.That.ThrowsExceptionWithInnerException<T>(action));
            }
            else
            {
                RegisterAction(() => Assert.That.ThrowsExceptionWithInnerException<T>(action, message));
            }
        }

        public static void ExceptionMessageContains(Action action, string contains, string message = null)
        {
            if (message == null)
            {
                RegisterAction(() => Assert.That.ExceptionMessageContains(action, contains));
            }
            else
            {
                RegisterAction(() => Assert.That.ExceptionMessageContains(action, contains, message));
            }
        }

        public static void ExceptionMessageEquals(Action action, string equals, string message = null)
        {
            if (message == null)
            {
                RegisterAction(() => Assert.That.ExceptionMessageEquals(action, equals));
            }
            else
            {
                RegisterAction(() => Assert.That.ExceptionMessageEquals(action, equals, message));
            }
        }

        public static void ThrowsExceptionAsync<T>(Func<Task> func, string message = null) where T : Exception
        {
            if (message == null)
            {
                RegisterFunc(async () => await Assert.ThrowsExceptionAsync<T>(func));
            }
            else
            {
                RegisterFunc(async () => await Assert.ThrowsExceptionAsync<T>(func, message));
            }
        }

        public static void ThrowsExceptionWithInnerExceptionAsync<T>(Func<Task> func, string message = null) where T : Exception
        {
            if (message == null)
            {
                RegisterFunc(async () => await Assert.That.ThrowsExceptionWithInnerExceptionAsync<T>(func));
            }
            else
            {
                RegisterFunc(async () => await Assert.That.ThrowsExceptionWithInnerExceptionAsync<T>(func, message));
            }
        }

        public static void ExceptionMessageContainsAsync(Func<Task> func, string contains, string message = null)
        {
            if (message == null)
            {
                RegisterFunc(async () => await Assert.That.ExceptionMessageContainsAsync(func, contains));
            }
            else
            {
                RegisterFunc(async () => await Assert.That.ExceptionMessageContainsAsync(func, contains, message));
            }
        }

        public static void ExceptionMessageEqualsAsync(Func<Task> func, string equals, string message = null)
        {
            if (message == null)
            {
                RegisterFunc(async () => await Assert.That.ExceptionMessageEqualsAsync(func, equals));
            }
            else
            {
                RegisterFunc(async () => await Assert.That.ExceptionMessageEqualsAsync(func, equals, message));
            }
        }

        #endregion

        #region Singleton Instance For Extension Methods 
        private static AssertAll _that;
        public static AssertAll That => _that ?? (_that = new AssertAll());
        #endregion

        #region Private Methods 
        private static void RegisterAction(Action action)
        {
            if (ReadyForUsage == false)
            {
                throw new InvalidOperationException(
                    "AssertAll statements can only be used in a test with the AssertAllTestMethod attribute");
            }

            var testInfo = CurrentTestInfo();
            RegisteredAssertStatements.Add(new AssertStatement
            {
                TestName = testInfo.TestName,
                File = testInfo.File,
                LineNumber = testInfo.LineNumber,
                Action = action
            });
        }

        private static void RegisterFunc(Func<Task> func)
        {
            if (ReadyForUsage == false)
            {
                throw new InvalidOperationException(
                    "AssertAll statements can only be used in a test with the AssertAllTestMethod attribute");
            }

            var testInfo = CurrentTestInfo();
            RegisteredAssertStatements.Add(new AssertStatement
            {
                TestName = testInfo.TestName,
                File = testInfo.File,
                LineNumber = testInfo.LineNumber,
                Func = func
            });
        }
        #endregion

        public partial class Collections 
        {
        }

        public partial class Strings
        {

        }
    }
}