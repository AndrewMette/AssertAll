using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllNuget.ExtensionMethods
{
    internal static class ThrowsExceptionExtensions
    {
        internal static void ThrowsExceptionWithInnerException<T>(this Assert source, Action action, string message = null)
        {
            Exception thrownException = null;
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                thrownException = e;
            }

            if (thrownException == null)
            {
                throw new AssertFailedException($"Assert.ThrowsExceptionWithInnerException failed. No exception thrown. {typeof(T).Name} inner exception was expected. {message}");
            }

            if (thrownException.InnerException == null)
            {
                throw new AssertFailedException($"Assert.ThrowsExceptionWithInnerException failed. Thrown exception has no inner exception. {typeof(T).Name} inner exception was expected. {message}");
            }

            if (thrownException.InnerException?.GetType() != typeof(T))
            {
                throw new AssertFailedException($"Assert.ThrowsExceptionWithInnerException failed. Threw exception {thrownException.InnerException.GetType()}, but inner exception {typeof(T)} was expected. {message}\r\nException Message: {thrownException.Message}\r\nStack Trace:    {thrownException.StackTrace}");
            }
        }

        internal static void ExceptionMessageContains(this Assert source, Action action, string contains, string message=null)
        {
            Exception thrownException = null;
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                thrownException = e;
            }

            if (thrownException == null)
            {
                throw new AssertFailedException($"Assert.ExceptionMessageContains failed. No exception thrown. {message}");
            }

            if (thrownException.Message.Contains(contains) == false)
            {
                throw new AssertFailedException($"Assert.ExceptionMessageContains failed. {thrownException.Message} does not contain {contains}. {message}");
            }
        }

        internal static void ExceptionMessageEquals(this Assert source, Action action, string equals, string message=null)
        {
            Exception thrownException = null;
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                thrownException = e;
            }

            if (thrownException == null)
            {
                throw new AssertFailedException($"Assert.ExceptionMessageEquals failed. No exception thrown. {message}");
            }

            if (thrownException.Message != equals)
            {
                throw new AssertFailedException($"Assert.ExceptionMessageEquals failed. Expected message: {equals} Actual message: {thrownException.Message}. {message}");
            }
        }

        internal static async Task ThrowsExceptionWithInnerExceptionAsync<T>(this Assert source, Func<Task> func, string message = null)
        {
            Exception thrownException = null;
            try
            {
                await func.Invoke();
            }
            catch (Exception e)
            {
                thrownException = e;
            }

            if (thrownException == null)
            {
                throw new AssertFailedException($"Assert.ThrowsExceptionWithInnerExceptionAsync failed. No exception thrown. {typeof(T).Name} inner exception was expected. {message}");
            }

            if (thrownException.InnerException == null)
            {
                throw new AssertFailedException($"Assert.ThrowsExceptionWithInnerExceptionAsync failed. Thrown exception has no inner exception. {typeof(T).Name} inner exception was expected. {message}");
            }

            if (thrownException.InnerException?.GetType() != typeof(T))
            {
                throw new AssertFailedException($"Assert.ThrowsExceptionWithInnerExceptionAsync failed. Threw exception {thrownException.InnerException.GetType()}, but inner exception {typeof(T)} was expected. {message}\r\nException Message: {thrownException.Message}\r\nStack Trace:    {thrownException.StackTrace}");
            }
        }

        internal static async Task ExceptionMessageContainsAsync(this Assert source, Func<Task> func, string contains, string message = null)
        {
            Exception thrownException = null;
            try
            {
                await func.Invoke();
            }
            catch (Exception e)
            {
                thrownException = e;
            }

            if (thrownException == null)
            {
                throw new AssertFailedException($"Assert.ExceptionMessageContainsAsync failed. No exception thrown. {message}");
            }

            if (thrownException.Message.Contains(contains) == false)
            {
                throw new AssertFailedException($"Assert.ExceptionMessageContainsAsync failed. {thrownException.Message} does not contain {contains}. {message}");
            }
        }

        internal static async Task ExceptionMessageEqualsAsync(this Assert source, Func<Task> func, string equals, string message = null)
        {
            Exception thrownException = null;
            try
            {
                await func.Invoke();
            }
            catch (Exception e)
            {
                thrownException = e;
            }

            if (thrownException == null)
            {
                throw new AssertFailedException($"Assert.ExceptionMessageEqualsAsync failed. No exception thrown. {message}");
            }

            if (thrownException.Message != equals)
            {
                throw new AssertFailedException($"Assert.ExceptionMessageEqualsAsync failed. Expected message: {equals} Actual message: {thrownException.Message}. {message}");
            }
        }
    }
}
