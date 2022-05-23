using System;
using MediaHub.Data.PersistencyLayer;

namespace MediaHub.Test.LogTests;

public class LogServiceMock : LogService
{
    public override void LogInformation(string message, LogService.LogCategory category)
    {
    }

    public override void LogException(string message, LogService.LogCategory category, Exception exception)
    {
    }
}