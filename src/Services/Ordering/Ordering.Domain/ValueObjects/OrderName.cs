using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
     
        public const int DefaultLength = 5; 
        public string Value { get; }
        public OrderName(string value) => Value = value;
        public static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length,DefaultLength);
            
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("OrderName cannot be empty.");
            }
            if (value.Length > DefaultLength)
            {
                throw new DomainException($"OrderName cannot be longer than {DefaultLength} characters.");
            }
            return new OrderName(value);
        }
    }
}