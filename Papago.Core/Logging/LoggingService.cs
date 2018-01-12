using System;
using NLog;

namespace Papago.Core.Logging
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger _logger;

        public LoggingService()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Debug( string message ) => _logger.Debug( message );

        public void Debug( Exception exception ) => _logger.Debug( exception );

        public void Error( string message ) => _logger.Error( message );

        public void Error( Exception exception ) => _logger.Error( exception );

        public void Fatal( string message ) => _logger.Fatal( message );

        public void Fatal( Exception exception ) => _logger.Fatal( exception );

        public void Info( string message ) => _logger.Info( message );

        public void Info( Exception exception ) => _logger.Info( exception );

        public void Warn( string message ) => _logger.Warn( message );

        public void Warn( Exception exception ) => _logger.Warn( exception );
    }
}