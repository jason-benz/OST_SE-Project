using MediaHub.Data.UserSuggestionModule.Model;
using System;
using System.Threading.Tasks;

namespace MediaHub.Test.UserSuggestionTest
{
    public class UserSuggestionEngineMock : IUserSuggestionEngine
    {
        public Task StartUserSuggestionEngine(string userId)
        {
            return Task.Run(() => Console.WriteLine("Start UserSuggestionEngineMock"));
        }
    }
}
