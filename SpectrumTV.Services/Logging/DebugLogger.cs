using System;
using System.Collections.Generic;
using SpectrumTV.Services.Logging;

namespace SpectrumTV.Interfaces.Logging
{
    public class DebugLogger : Logger
    {
        public override void Error(object sender, Exception ex, IDictionary<string, string> properties = null)
        {
            DebugError(sender, ex);
        }
    }
}
