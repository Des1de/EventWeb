namespace EventWeb.Application.Exceptions
{
    public class InternalException : Exception
    {
        private const string MESSAGE = "Internal error occured during request processing";
        public InternalException(string MESSAGE) 
            : base(MESSAGE)
        {
            
        }
    }
}