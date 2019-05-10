using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AssertAll.Exceptions;
using AssertAll.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly:InternalsVisibleTo("AssertAll.Tests")]
namespace AssertAll
{
    /// <summary>
    /// Run all of your MSTest assert statements and have each failure message reported summarily.
    /// </summary>
    public class AssertAll
    {
        #region Properties 

        internal static bool ReadyForUsage = false;
        internal static readonly List<AssertStatement> RegisteredAssertStatements;
        private static TestInfo CurrentTestInfo
        {
            get
            {
                var stackTrace = new StackTrace(true);

                var frames = stackTrace.GetFrames()?
                    .Where(x => x.GetMethod()?.DeclaringType?.Namespace?.StartsWith("System") == false
                            && x.GetMethod()?.DeclaringType?.Namespace?.StartsWith("Microsoft") == false
                            && x.GetMethod()?.DeclaringType?.Namespace != "AssertAll");

                var frame = frames.Last();
                var file = frame.GetFileName();
                var lineNumber = frame.GetFileLineNumber();
                var method = frame.GetMethod();

                var nameSpace = method?.DeclaringType?.Namespace;
                var currentTest = $"{nameSpace}.{method?.DeclaringType?.Name}.{method?.Name}";

                return new TestInfo{File = file, LineNumber = lineNumber, TestName = currentTest};
            }
        }

        #endregion

        #region Constructor 
        static AssertAll()
        {
            RegisteredAssertStatements = new List<AssertStatement>();
        }
        #endregion

        #region Execute 
        /// <summary>
        /// Executes all the Assert statements that have been registered for the current test. Not calling this method will cause tests to pass erroneously.
        /// </summary>
        internal static void Execute()
        {
            var assertExceptions = new List<UnitTestAssertException>();
            var failure = false;
            List<string> stackTraceItems = new List<string>();
            foreach (var registeredAssertStatement in RegisteredAssertStatements)
            {
                try
                {
                    registeredAssertStatement.Action.Invoke();
                }
                catch (AssertFailedException e)
                {
                    assertExceptions.Add(e);
                    var stackTraceLine = $"at {registeredAssertStatement.TestName}() in {registeredAssertStatement.File}:line {registeredAssertStatement.LineNumber}";
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
                    var replaced = assertException.Message.Replace("Assert.","AssertAll.");
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
        /// <summary>
        /// Tests whether the specified objects are equal and throws an exception if the two objects are not equal.Different numeric types are treated as unequal even if the logical values are equal. 42L is not equal to 42.
        /// </summary>
        public static void AreEqual(object expected, object actual, string message = null)
        {
            if (message == null)
            {
                Register(() => Assert.AreEqual(expected, actual));
            }
            else
            {
                Register(() => Assert.AreEqual(expected, actual, message));
            }
        }

        /// <summary>
        /// Tests whether the specified objects are unequal and throws an exception if the two objects are equal. Different numeric types are treated as unequal even if the logical values are equal. 42L is not equal to 42.
        /// </summary>
        public static void AreNotEqual(object notExpected, object actual, string message = null)
        {
            if (message == null)
            {
                Register(() => Assert.AreNotEqual(notExpected, actual));
            }
            else
            {
                Register(() => Assert.AreNotEqual(notExpected, actual, message));
            }
        }

        /// <summary>
        /// Tests whether the specified objects both refer to the same object and throws an exception if the two inputs do not refer to the same object.
        /// </summary>
        public static void AreSame(object expected, object actual, string message = null)
        {
            if (message == null)
            {
                Register(() => Assert.AreSame(expected, actual));
            }
            else
            {
                Register(() => Assert.AreSame(expected, actual, message));
            }
        }

        /// <summary>
        /// Tests whether the specified objects refer to different objects and throws an exception if the two inputs refer to the same object.
        /// </summary>
        public static void AreNotSame(object notExpected, object actual, string message = null)
        {
            if (message == null)
            {
                Register(() => Assert.AreNotSame(notExpected, actual));
            }
            else
            {
                Register(() => Assert.AreNotSame(notExpected, actual));
            }
        }

        /// <summary>
        /// Throws an AssertFailedException.
        /// </summary>
        public static void Fail(string message = null)
        {
            if (message == null)
            {
                Register(() => Assert.Fail());
            }
            else
            {
                Register(() => Assert.Fail(message));
            }
        }

        /// <summary>
        /// Throws an AssertInconclusiveException.
        /// </summary>
        public static void Inconclusive(string message = null)
        {
            if (message == null)
            {
                Register(() => Assert.Inconclusive());
            }
            else
            {
                Register(() => Assert.Inconclusive(message));
            }
        }

        /// <summary>
        /// Tests whether the specified object is an instance of the expected type and throws an exception if the expected type is not in the inheritance hierarchy of the object.
        /// </summary>
        public static void IsInstanceOfType(object value, Type expectedType, string message = null)
        {
            if (message == null)
            {
                Register(() => Assert.IsInstanceOfType(value, expectedType));
            }
            else
            {
                Register(() => Assert.IsInstanceOfType(value, expectedType, message));
            }
        }

        /// <summary>
        /// Tests whether the specified object is not an instance of the wrong type and throws an exception if the specified type is in the inheritance hierarchy of the object.
        /// </summary>
        public static void IsNotInstanceOfType(object value, Type wrongType, string message = null)
        {
            if (message == null)
            {
                Register(() => Assert.IsNotInstanceOfType(value, wrongType));
            }
            else
            {
                Register(() => Assert.IsNotInstanceOfType(value, wrongType, message));
            }
        }

        /// <summary>
        /// Tests whether the specified object is null and throws an exception if it is not.
        /// </summary>
        public static void IsNull(object value, string message = null)
        {
            if (message == null)
            {
                Register(() => Assert.IsNull(value));
            }
            else
            {
                Register(() => Assert.IsNull(value, message));
            }
        }

        /// <summary>
        /// Tests whether the specified object is non-null and throws an exception if it is null.
        /// </summary>
        public static void IsNotNull(object value, string message = null)
        {
            if (message == null)
            {
                Register(() => Assert.IsNotNull(value));
            }
            else
            {
                Register(() => Assert.IsNotNull(value, message));
            }
        }

        /// <summary>
        /// Tests whether the specified condition is true and throws an exception if the condition is false.
        /// </summary>
        public static void IsTrue(bool condition, string message = null)
        {
            if (message == null)
            {
                Register(() => Assert.IsTrue(condition));
            }
            else
            {
                Register(() => Assert.IsTrue(condition, message));
            }
        }

        /// <summary>
        /// Tests whether the specified condition is false and throws an exception if the condition is true.
        /// </summary>
        public static void IsFalse(bool condition, string message = null)
        {
            if (message == null)
            {
                Register(() => Assert.IsFalse(condition));
            }
            else
            {
                Register(() => Assert.IsFalse(condition, message));
            }
        }

        /// <summary>
        /// Tests whether the code specified by delegate action throws exact given exception of type T (and not of derived type) and throws AssertFailedException if code does not throws exception or throws exception of type other than T.
        /// </summary>
        public static void ThrowsException<T>(Action action, string message = null) where T : Exception
        {
            if (message == null)
            {
                Register(() => Assert.ThrowsException<T>(action));
            }
            else
            {
                Register(() => Assert.ThrowsException<T>(action, message));
            }
        }

        /// <summary>
        /// Tests whether the code specified by delegate action throws exact given exception of type T (and not of derived type) and throws AssertFailedException if code does not throws exception or throws exception of type other than T.
        /// </summary>
        internal static void ThrowsExceptionAsync<T>(Func<Task> action, string message = null) where T : Exception
        {
            if (message == null)
            {
                Register(async () => await Assert.ThrowsExceptionAsync<T>(action));
            }
            else
            {
                Register(async () => await Assert.ThrowsExceptionAsync<T>(action, message));
            }
        }
        #endregion

        #region Singleton Instance For Extension Methods 
        private static AssertAll that;
        /// <summary>
        /// Gets the singleton instance of the AssertAll functionality.
        /// </summary>
        public static AssertAll That => that ?? (that = new AssertAll());
        #endregion

        #region Private Methods 
        internal static void Register(Action action)
        {
            if (ReadyForUsage == false)
            {
                throw new InvalidOperationException(
                    "AssertAll statements can only be used in a test with the AssertAllTestMethod attribute");
            }

            RegisteredAssertStatements.Add(new AssertStatement
            {
                TestName = CurrentTestInfo.TestName,
                File = CurrentTestInfo.File,
                LineNumber = CurrentTestInfo.LineNumber,
                Action = action
            });
        }
        #endregion
    }
}