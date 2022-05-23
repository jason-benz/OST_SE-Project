using MediaHub.Data.PersistencyLayer;
using Serilog.Core;

namespace MediaHub.Services;

public class SerilogService : LogService
{
    private Logger _logger;
    public SerilogService(Logger logger)
    {
        _logger = logger;
    }

    public override void LogInformation(string message, LogService.LogCategory category)
    {
        _logger.Information(FormatString(message, category));
    }

    public override void LogException(string message, LogService.LogCategory category, Exception exception)
    {
        _logger.Error(exception, FormatString(message, category));
    }

    private string FormatString(string message, LogService.LogCategory category)
    {
        return $"{category.ToString()} -> {message}";
    }
}