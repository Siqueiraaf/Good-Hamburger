namespace GoodHamburger.Domain.Exceptions;

public class UndefinedSandwichException : Exception
{
    public UndefinedSandwichException() : base("Sandwich is not defined in the menu.") { }
}
