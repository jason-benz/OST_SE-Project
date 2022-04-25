using MediaHub.Data.Persistency;
using Serilog.Core;

namespace MediaHub.Services;

public class SerilogService : ILogService
{
    private Logger _logger;
    private static SerilogService Singleton; 
    public SerilogService(Logger logger)
    {
        _logger = logger;
    }
    
    public static ILogService GetOrCreateSingleton(Logger logger)
    {
        if (Singleton == null)
        {
            Singleton = new SerilogService(logger);
        }

        return Singleton;
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