using System;
using System.Collections.Generic;
using SpectrumTV.Interfaces;

namespace SpectrumTV.Services.Logging
{
    public class Logger : ILogger
    {
        public virtual void Error(object sender, Exception ex, IDictionary<string, string> properties = null)
        {
            try
            {
                if (properties == null)
                {
                    properties = new Dictionary<string, string>();
                }

                properties["sender"] = sender.GetType().ToString();

                //Crashes.TrackError(ex, properties);

                DebugError(sender, ex);
            }
            catch (Exception errorEx)
            {
                Debug(this, $"Exception logging error: {errorEx.Message}\n{errorEx.StackTrace}");
            }
        }

        public void DebugError(object sender, Exception ex)
        {
            Debug(sender, $"Exception thrown: {ex.Message}\n{ex.StackTrace}");
        }

        public void Debug(object sender, string message)
        {
            System.Diagnostics.Debug.WriteLine($"[{sender.GetType().Name}] {message}");
        }
    }
}
