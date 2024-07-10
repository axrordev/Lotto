using System.Runtime.Serialization;

namespace Planner.Service.Exceptions;

public class NotFoundException : Exception
{
	public NotFoundException() : base() { }
	public NotFoundException(string message) : base(message) { }
	public NotFoundException(string message, Exception innerException) : base(message) { }
	public NotFoundException(SerializationException exception, StreamingContext context)   { }
    public int StatusCode => 404;
}