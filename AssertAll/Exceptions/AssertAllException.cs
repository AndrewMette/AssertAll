using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Exceptions
{
    internal class AssertAllException : AssertFailedException
    {
        protected internal readonly string OldStackTrace;

        internal AssertAllException(string message, string stackTrace) : base(message)
        {
            OldStackTrace = stackTrace;
        }
        
        public override string StackTrace => OldStackTrace;
    }
}