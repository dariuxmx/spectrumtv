using System;
using System.Collections.Generic;

namespace SpectrumTV.Interfaces
{
    public interface ILogger
    {
        void Error(object sender, Exception ex, IDictionary<string, string> properties = null);
        void DebugError(object sender, Exception ex);
        void Debug(object sender, string message);
    }
}
