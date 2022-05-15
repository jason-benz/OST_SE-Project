using MediaHub.Data.PersistencyLayer;
using Serilog.Core;

namespace MediaHub.Services;

public class SerilogService : ILogService
{
    private Logger _logger;
    public SerilogService(Logger logger)
    {
        _logger = logger;
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