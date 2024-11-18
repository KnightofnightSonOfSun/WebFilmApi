/*************************************************************************************
 *
 * File name:   LoggingBuilderExtension.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/17 16:59
 * ======================================
*************************************************************************************/
using Koknight.Basic.Common.Loggers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace WebFilmApi.Extensions
{
    public static class LoggingBuilderExtension
    {
        public static void AddLocalFileLogger(this IServiceCollection services, Action<LoggingSetting> action)
        {
            services.Configure(action);
            services.AddSingleton<ILoggerProvider, ExtendLoggerProvider>();
            services.AddSingleton<IHostedService, LogClearTask>();
        }
    }
}
