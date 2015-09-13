using System;
using Common.Logging;

namespace PetLab.Common {
    /// <summary>
    /// Method of logging events
    /// </summary>
    public static class Logger {
        private static readonly ILog CommonLogger = LogManager.GetCurrentClassLogger();

        public static void Log(LogLevel level, string message, params object[] args) {
            switch (level) {
                case LogLevel.Warn:
                    CommonLogger.WarnFormat(message, args);
                    break;
                case LogLevel.Info:
                    CommonLogger.InfoFormat(message, args);
                    break;
                case LogLevel.Error:
                    CommonLogger.ErrorFormat(message, args);
                    break;
                case LogLevel.Debug:
					CommonLogger.DebugFormat(message, args);
                    break;
            };
        }
        
        public static void Log(LogLevel level, string message, Exception ex, params object[] args) {
            switch (level) {
                case LogLevel.Warn:
                    CommonLogger.WarnFormat(message, ex, args);
                    break;
                case LogLevel.Info:
                    CommonLogger.InfoFormat(message, ex, args);
                    break;
                case LogLevel.Error:
                    CommonLogger.ErrorFormat(message, ex, args);
                    break;
                case LogLevel.Fatal:
                    CommonLogger.FatalFormat(message, ex, args);
                    break;
                case LogLevel.Debug:
					CommonLogger.DebugFormat(message, ex, args);
                    break;
            };
        }

        public static void Info(string message, params object[] args) {
            Log(LogLevel.Info, message, args);
        }

        public static void Warn(string message, params object[] args) {
            Log(LogLevel.Warn, message, args);
        }

        public static void Error(string message, params object[] args) {
            Log(LogLevel.Error, message, args);
        }

        public static void Error(string message, Exception ex, params object[] args) {
            Log(LogLevel.Error, message, ex, args);
        }

        public static void Fatal(string message, params object[] args) {
            Log(LogLevel.Fatal, message, args);
        }

        public static void Fatal(string message, Exception ex, params object[] args) {
            Log(LogLevel.Fatal, message, ex, args);
        }

        public static void Debug(string message, params object[] args) {
            Log(LogLevel.Debug, message, args);
        }

        public static void Debug(string message, Exception ex, params object[] args) {
            Log(LogLevel.Debug, message, ex, args);
        }
    }
}