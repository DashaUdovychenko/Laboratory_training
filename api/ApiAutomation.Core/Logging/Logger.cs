using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;
using System.Reflection;

namespace ApiAutomation.Core.Logging
{
    public static class Logger
    {
        private static readonly ILog log;

        static Logger()
        {
            Assembly? assembly = Assembly.GetEntryAssembly();
            ILoggerRepository logRepository;

            if (assembly != null)
            {
                logRepository = LogManager.GetRepository(assembly);
            }
            else
            {
                logRepository = LogManager.GetRepository(typeof(Logger).Assembly);
            }

            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            log = LogManager.GetLogger(typeof(Logger));
        }

        public static void Info(string message) => log.Info(message);
        public static void Error(string message) => log.Error(message);
        public static void Warn(string message) => log.Warn(message);
    }
}
