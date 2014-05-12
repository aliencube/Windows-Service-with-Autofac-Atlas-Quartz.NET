using Common.Logging;

namespace SampleWindowsService
{
    /// <summary>
    /// This represents an entity that performs the actual business logic.
    /// </summary>
    internal class SampleLogicLayer : ISampleLogicLayer
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Runs the business logic here.
        /// </summary>
        public void Run()
        {
            Log.Info("This has been run");
        }

        /// <summary>
        /// Disposes resources not being used any more.
        /// </summary>
        public void Dispose()
        {
        }
    }
}