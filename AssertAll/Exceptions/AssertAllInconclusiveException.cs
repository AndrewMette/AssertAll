namespace AssertAllNuget.Exceptions
{
    internal class AssertAllInconclusiveException : AssertAllException
    {
        internal AssertAllInconclusiveException(string message, string stackTrace) : base(message, stackTrace)
        {
        }
    }
}