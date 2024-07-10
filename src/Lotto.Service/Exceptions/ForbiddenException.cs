using System.Runtime.Serialization;

namespace Planner.Service.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException() : base() { }
    public ForbiddenException(string message) : base(message) { }
    public ForbiddenException(string message, Exception innerException) : base(message, innerException) { }
    public ForbiddenException(SerializationInfo info, StreamingContext context) { }
    public int StatusCode => 403;
}