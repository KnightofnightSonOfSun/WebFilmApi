/*************************************************************************************
 *
 * File name:   ExtendLogger.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/17 17:05
 * ======================================
*************************************************************************************/
using Koknight.Basic.Common.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Koknight.Basic.Common.Loggers
{
    public class ExtendLogger : ILogger
    {
        private readonly string categoryName;
        private readonly string basePath;


        public ExtendLogger(string categoryName, string basePath)
        {
            this.categoryName = categoryName;
            this.basePath = basePath;

            if (!Directory.Exists(basePath)) 
            {
                Directory.CreateDirectory(basePath);
            }
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return default;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel != LogLevel.None)
            {
                return true;
            }

            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (IsEnabled(logLevel))
            {
                if (state != null && state.ToString() != null)
                {
                    var logContent = state.ToString();

                    if (logContent != null)
                    {
                        if (exception != null)
                        {
                            var logMsg = new
                            {
                                message = logContent,
                                error = new
                                {
                                    exception?.Source,
                                    exception?.Message,
                                    exception?.StackTrace
                                }
                            };

                            logContent = JsonHelper.ObjectToJson(logMsg);
                        }

                        var log = new
                        {
                            CreateTime = DateTime.UtcNow,
                            Category = categoryName,
                            Level = logLevel.ToString(),
                            Content = logContent
                        };

                        string todayTime = DateTime.UtcNow.ToString("yyyyMMdd");
                        var logDirectoryPath = Path.Combine(basePath, todayTime);
                        if (!Directory.Exists(logDirectoryPath))
                        {
                            Directory.CreateDirectory(logDirectoryPath);
                        }

                        string logStr = JsonHelper.ObjectToJson(log);

                        var logPath = Path.Combine(logDirectoryPath, logLevel.ToString() + ".log");

                        File.AppendAllText(logPath, logStr + Environment.NewLine, Encoding.UTF8);
                    }
                }
            }
        }
    }
}
