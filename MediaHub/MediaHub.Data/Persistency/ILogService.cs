namespace MediaHub.Data.Persistency;

public interface ILogService
{
    public enum LogCategory
    {
        Chat, Media, Identity
    }

    public void LogInformation(string message, LogCategory category);
    public void LogException(string message, LogCategory category, Exception exception);
}