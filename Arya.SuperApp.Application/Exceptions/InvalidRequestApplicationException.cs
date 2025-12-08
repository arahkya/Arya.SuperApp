namespace Arya.SuperApp.Application.Exceptions;

public class InvalidRequestApplicationException(string message, Exception? innerException) : Exception($"Application Error Occurred : {message}", innerException)
{
    
}