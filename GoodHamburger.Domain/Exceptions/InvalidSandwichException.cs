namespace GoodHamburger.Domain.Exceptions;

public class InvalidSandwichException : Exception
{
    public InvalidSandwichException() : base("Order must include one valid sandwich.") { }
}
