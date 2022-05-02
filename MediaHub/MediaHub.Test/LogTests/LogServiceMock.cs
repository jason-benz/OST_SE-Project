using System;
using MediaHub.Data.Persistency;

namespace MediaHub.Test.UserProfileTest;

public class LogServiceMock : ILogService
{
    public void LogInformation(string message, ILogService.LogCategory category)
    {
    }

    public void LogException(string message, ILogService.LogCategory category, Exception exception)
    {
    }
}