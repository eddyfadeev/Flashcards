namespace Flashcards.Exceptions;

public class ExitApplicationException : Exception
{
    public ExitApplicationException() : base("Exiting the application...")
    {
    }
    
    public ExitApplicationException(string message) : base(message)
    {
    }
    
    public ExitApplicationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}