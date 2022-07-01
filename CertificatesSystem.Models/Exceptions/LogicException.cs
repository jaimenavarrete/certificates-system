namespace CertificatesSystem.Models.Exceptions
{
    public class LogicException : Exception
    {
        public LogicException() {}
    
        public LogicException(string message) : base(message) {}
    }
}