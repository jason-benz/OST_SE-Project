namespace MediaHub.Data.PersistencyLayer;

public abstract class LogService
{
    public enum LogCategory
    {
        Chat, Media, Identity, UserSuggestion
    }

    private static LogService? _singleton = null;
    public static LogService? Singleton
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

    public abstract void LogInformation(string message, LogCategory category);
    public abstract void LogException(string message, LogCategory category, Exception exception);
}