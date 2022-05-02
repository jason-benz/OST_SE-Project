using System;
using MediaHub.Data.PersistencyLayer;

namespace MediaHub.Test.LogTests;

public class LogServiceMock : ILogService
{
    public void LogInformation(string message, ILogService.LogCategory category)
    {
    }

    public void LogException(string message, ILogService.LogCategory category, Exception exception)
    {
    }
}