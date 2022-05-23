using System;
using System.Collections.Generic;
using System.IO;
using MediaHub.Services;
using Serilog;
using Serilog.Core;
using Xunit;
using MediaHub.Data.PersistencyLayer;

namespace MediaHub.Test.LogTests
{
    [Collection("Sequential")]
    public class LogServicesUnitTest : IDisposable
    {
        private LogService _logService;

        private string _logFileName = "./log.txt";
        private Logger _logConfig;
        public LogServicesUnitTest()
        {
            _logConfig = new LoggerConfiguration()
                .WriteTo.File(_logFileName)
                .CreateLogger();
            _logService = new SerilogService(_logConfig);
        }



        [Fact, Trait("Category", "Unit")]
        public void GetSingletonbeforeCreateThrows()
        {
            LogService.Singleton = null; //other tests might interfere, so this resets Singleton to its default value
            var exception = Record.Exception(() => LogService.Singleton);
            Assert.NotNull(exception);
        }

        [Fact, Trait("Category", "Unit")]
        public void InformationLogTypeTest()
        {
            _logService.LogInformation("testinfo", LogService.LogCategory.Chat);
            AssertLogType("[INF]");
        }

        [Fact, Trait("Category", "Unit")]
        public void InformationLoggedModuleTest()
        {
            _logService.LogInformation("testinfo", LogService.LogCategory.Chat);
            AssertLoggedModule("Chat");
        }

        [Fact, Trait("Category", "Unit")]
        public void InformationLogMessageTest()
        {
            _logService.LogInformation("testinfo", LogService.LogCategory.Chat);
            AssertLogMessage("testinfo");
        }

        [Fact, Trait("Category", "Unit")]
        public void ErrorLogTypeTest()
        {
            _logService.LogException("testinfo", LogService.LogCategory.Chat, new Exception("testexception"));
            AssertLogType("[ERR]");
        }

        [Fact, Trait("Category", "Unit")]
        public void ErrorExceptionMessageTest()
        {
            _logService.LogException("testinfo", LogService.LogCategory.Chat, new Exception("testexception"));
            Assert.Equal("testexception", ReadExceptionMessage());
        }

        private void AssertLogType(string expected)
        {
            var logType = ReadFirstLineOfLog().Split(" ")[3];
            Assert.Equal(expected, logType);
        }

        private void AssertLoggedModule(string expected)
        {
            var loggedModule = ReadFirstLineOfLog().Split(" ")[4];
            Assert.Equal(expected, loggedModule);
        }

        private void AssertLogMessage(string excepected)
        {
            var logMessage = ReadFirstLineOfLog().Split(" ")[6];
            Assert.Equal(excepected, logMessage);
        }
        private string ReadFirstLineOfLog()
        {
            return WriteSafeReadAllLines(_logFileName)[0];
        }

        private string ReadExceptionMessage()
        {
            int exceptionLineInLogFile = 1;
            return WriteSafeReadAllLines(_logFileName)[1].Split(" ")[1];
        }

        public string[] WriteSafeReadAllLines(string path)
        {
            using (var csv = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(csv))
            {
                List<string> file = new List<string>();
                while (!sr.EndOfStream)
                {
                    file.Add(sr.ReadLine());
                }

                return file.ToArray();
            }
        }

        public void Dispose()
        {
            _logConfig.Dispose();
            File.Delete("./log.txt");
        }
    }
}
