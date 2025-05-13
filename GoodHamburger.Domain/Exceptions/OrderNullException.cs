namespace GoodHamburger.Domain.Exceptions;

public class OrderNullException : Exception
{
    public OrderNullException() : base("Order must not be null.") { }
}
