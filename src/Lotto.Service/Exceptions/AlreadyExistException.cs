using System.Runtime.Serialization;

namespace Planner.Service.Exceptions;

public class AlreadyExistException : Exception
{
    public AlreadyExistException() : base() { }
    public AlreadyExistException(string message) : base(message) { }
    public AlreadyExistException(string message, Exception innerException) : base(message) { }
    public AlreadyExistException(SerializationException exception, StreamingContext context) { }
    public int StatusCode => 409;

}
 