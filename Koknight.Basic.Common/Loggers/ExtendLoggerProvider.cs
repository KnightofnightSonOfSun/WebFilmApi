/*************************************************************************************
 *
 * File name:   LocalFileLoggerProvider.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/17 17:04
 * ======================================
*************************************************************************************/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koknight.Basic.Common.Loggers
{
    public class ExtendLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, ExtendLogger> loggers = new();
        private readonly IConfiguration configuration;
        private readonly string basePath;


        public ExtendLoggerProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.basePath = configuration.GetSection("LoggingBasePath").Value.ToString();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, new ExtendLogger(categoryName, this.basePath));
        }

        public void Dispose()
        {
            loggers.Clear();
            GC.SuppressFinalize(this);
        }
    }
}
