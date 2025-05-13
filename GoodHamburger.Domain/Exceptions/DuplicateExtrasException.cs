namespace GoodHamburger.Domain.Exceptions;

public class DuplicateExtrasException : Exception
{
    public DuplicateExtrasException(string extras)
        : base($"Each order can only contain one of each extra item. Duplicates: {extras}") { }
}