using System.Runtime.Serialization;

namespace Lotto.Service.Exceptions;

public class ArgumentIsNotValidException : Exception
{
    public ArgumentIsNotValidException() { }
    public ArgumentIsNotValidException(string message) : base(message) { }
    public ArgumentIsNotValidException(string message, Exception innerException) : base(message) { }
    public ArgumentIsNotValidException(SerializationException exception, StreamingContext context) { }
    public int StatusCode => 400;
}