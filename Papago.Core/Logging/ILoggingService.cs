using System;

namespace Papago.Core.Logging
{
    public interface ILoggingService
    {
        void Debug( string message );
        void Debug( Exception exception );
        void Error( string message );
        void Error( Exception exception );
        void Fatal( string message );
        void Fatal( Exception exception );
        void Info( string message );
        void Info( Exception exception );
        void Warn( string message );
        void Warn( Exception exception );
    }
}