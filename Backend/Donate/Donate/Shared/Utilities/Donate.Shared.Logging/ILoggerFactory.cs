namespace Donate.Shared.Logging
{
    public interface ILoggerFactory
    {
        IApiLogger GetLogger<T>();
    }
}
