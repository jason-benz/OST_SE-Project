using System.Collections.Generic;

namespace MediaHub.Test.ContactTest;

public class MockUser
{
    private static readonly List<string> MockUsers = new List<string>()
    {
        "c67f490b-e0a0-460d-95b0-6505910f6600",
        "c87b8391-0e96-45d8-a3cf-9300498f5601",
        "9d6855fb-7aff-4a55-b41e-aab429a54602",
        "9d6855fb-7aff-4a55-b41e-aab429a70603"
    };

    public static List<string> GetMockUsers()
    {
        return MockUsers;
    }
}