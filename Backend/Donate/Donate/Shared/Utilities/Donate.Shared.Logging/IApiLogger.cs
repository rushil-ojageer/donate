using System;

namespace Donate.Shared.Logging
{
    public interface IApiLogger
    {
        void Info(string log);
        void Error(string log, Exception ex);
        void Error(string log);
        void Debug(string log);
        void Warning(string log);
        void Trace(string log);
        void Critical(string log);
    }
}
