namespace AssertAllNuget.Exceptions
{
    internal class AssertAllFailedException : AssertAllException
    {
        internal AssertAllFailedException(string message, string stackTrace) : base(message, stackTrace)
        {   
        }
        
    }
}