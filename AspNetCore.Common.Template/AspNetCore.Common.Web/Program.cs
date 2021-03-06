﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace AspNetCore.Common.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            FluentScheduler.JobManager.StopAndBlock();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                        .AddCommandLine(args)
                        .AddJsonFile("hosting.json", optional: true)
                        .Build();
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            return WebHost.CreateDefaultBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseConfiguration(config)
                .UseEnvironment("Development")
                .UseStartup<Startup>()
                .UseNLog()
                .Build();
        }
    }
}
