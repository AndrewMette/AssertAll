using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly:InternalsVisibleTo("AssertAll.Tests")]
namespace AssertAll
{
    public class AssertAll
    {
        #region Properties 
        private static readonly Dictionary<string, List<Action>> MethodActions;
        private static string Key
        {
            get
            {
                var stackTrace = new StackTrace();
                var frames = stackTrace.GetFrames()?.Where(x => x.GetMethod()?.DeclaringType?.Namespace?.StartsWith("System") == false
                                                                && x.GetMethod()?.DeclaringType?.Namespace?.StartsWith("Microsoft") == false);
                var method = frames?.Last().GetMethod();
                var key = $"{method?.DeclaringType?.Namespace}.{method?.DeclaringType?.Name}.{method?.Name}";
                return key;
            }
        }
        #endregion

        #region Constructor 
        static AssertAll()
        {
            MethodActions = new Dictionary<string, List<Action>>();
        }
        #endregion

        #region Execute 
        public static void Execute()
        {
            var assertExceptions = new List<UnitTestAssertException>();
            var failure = false;
            var actions = MethodActions.Where(x => x.Key == Key).SelectMany(x => x.Value);
            foreach (var action in actions)
            {
                try
                {
                    action.Invoke();
                }
                catch (AssertFailedException e)
                {
                    assertExceptions.Add(e);
                    failure = true;
                }
                catch (AssertInconclusiveException e)
                {
                    assertExceptions.Add(e);
                }
            }

            if (assertExceptions.Count > 0)
            {
                var allMessages = assertExceptions.Select(x => x.Message);

                var bigMessage = string.Join("; ", allMessages);

                if (failure)
                {
                    throw new AssertFailedException(bigMessage);
                }

                throw new AssertInconclusiveException(bigMessage);
            }
        }
        #endregion

        #region Assert Methods 
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
        public static AssertAll That => that ?? (that = new AssertAll());
        #endregion

        #region Private Methods 
        private static void Register(Action action)
        {
            if (MethodActions.ContainsKey(Key))
            {
                MethodActions.Single(x => x.Key == Key).Value.Add(action);
            }
            else
            {
                MethodActions.Add(Key, new List<Action> { action });
            }
        }
        #endregion
    }
}