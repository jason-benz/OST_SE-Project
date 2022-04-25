using MediaHub.Data.Persistency;
using Serilog.Core;

namespace MediaHub.Services;

public class SerilogService : ILogService
{
    private Logger _logger;
    private static SerilogService? _singleton; 
    public SerilogService(Logger logger)
    {
        _logger = logger;
    }

    public static ILogService GetSingleton()
    {
        if (_singleton == null)
        {
            throw new Exception("Singleton was not created yet");
        }

        return _singleton;
    }
    public static ILogService CreateAndGetSingleton(Logger logger)
    {
        _singleton = new SerilogService(logger);
        return _singleton;
    }
    public void LogInformation(string message, ILogService.LogCategory category)
    {
        _logger.Information(FormatString(message, category));
    }

    public void LogException(string message, ILogService.LogCategory category, Exception exception)
    {
        _logger.Error(exception, FormatString(message, category));
    }

    private string FormatString(string message, ILogService.LogCategory category)
    {
        return $"{category.ToString()} -> {message}";
    }
}