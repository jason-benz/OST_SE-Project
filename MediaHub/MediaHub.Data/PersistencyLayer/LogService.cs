namespace MediaHub.Data.PersistencyLayer;

public class ILogService
{
    public enum LogCategory
    {
        Chat, Media, Identity, UserSuggestion
    }

    private static ILogService? _singleton = null;
    public static ILogService? Singleton
    {
        get
        {
            if (_singleton == null)
            {
                throw new Exception("Singleton must be set before use");
            }
            return _singleton;
        }
        set
        {
            _singleton = value;
        }
    }

    public void LogInformation(string message, LogCategory category);
    public void LogException(string message, LogCategory category, Exception exception);
}