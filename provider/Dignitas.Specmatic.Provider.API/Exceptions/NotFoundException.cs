namespace Dignitas.Specmatic.Provider.API.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(NotFoundReason reason) : base()
    {
        Reason = reason;
    }

    public NotFoundReason Reason { get; }

    public enum NotFoundReason
    {
        CartNotFound,
        ProductNotFound,
        UserNotFound
    }
}
