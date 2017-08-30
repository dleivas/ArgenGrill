using Argengrill.Core.Infrastructure;
using Argengrill.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argengrill.Core.Logging
{
    public interface IChronometer : IDisposable
    {
        void StepStart(string key, string message);

        void StepStop(string key);
    }

    public static class IChronometerExtensions
    {
        public static IDisposable Step(this IChronometer chronometer, string message)
        {
            Guard.NotEmpty(message, nameof(message));

            var key = "step" + CommonHelper.GenerateRandomDigitCode(10);

            chronometer.StepStart(key, message);
            return new ActionDisposable(() => chronometer.StepStop(key));
        }
    }

    public class NullChronometer : IChronometer
    {
        private static readonly IChronometer _instance = new NullChronometer();

        public static IChronometer Instance
        {
            get { return _instance; }
        }

        public void StepStart(string key, string message)
        {
        }

        public void StepStop(string key)
        {
        }

        public void Dispose()
        {
        }
    }
}