/*************************************************************************************
 *
 * File name:   LogClearTask.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/17 18:16
 * ======================================
*************************************************************************************/
using Koknight.Basic.Common.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koknight.Basic.Common.Loggers
{
    public class LogClearTask : BackgroundService
    {
        private readonly int saveDays;
        private readonly IConfiguration configuration;


        public LogClearTask(IOptionsMonitor<LoggingSetting> setting, IConfiguration configuration)
        {
            saveDays = setting.CurrentValue.SaveDays;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    string basePath = configuration.GetSection("LoggingBasePath").Value.ToString();

                    if (Directory.Exists(basePath))
                    {
                        List<string> logPaths = IOHelper.GetAllFolders(basePath).ToList();

                        var deleteTime = DateTime.UtcNow.AddDays(-1 * saveDays);

                        if (logPaths.Count != 0)
                        {
                            foreach (var logPath in logPaths)
                            {
                                var directoryInfo = new DirectoryInfo(logPath);

                                if (directoryInfo.CreationTimeUtc < deleteTime)
                                {
                                    Directory.Delete(logPath);
                                }

                            }
                        }
                    }
                }
                catch
                {
                }

                await Task.Delay(1000 * 60 * 60 * 24, stoppingToken);
            }
        }
    }
}
