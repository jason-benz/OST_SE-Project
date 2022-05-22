using System;
using System.Collections.Generic;

namespace MediaHub.Test.ContactTest;

public class MockUser
{
    private static readonly List<string> MockUsers = new()
    {
        Guid.NewGuid().ToString(),
        Guid.NewGuid().ToString(),
        Guid.NewGuid().ToString(),
        Guid.NewGuid().ToString(),
    };

    public static List<string> GetMockUserIds()
    {
        return MockUsers;
    }
}