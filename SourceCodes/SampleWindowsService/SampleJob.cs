using Common.Logging;
using Quartz;

namespace SampleWindowsService
{
    /// <summary>
    /// This represents an entity for the job that actually performs for the schedule.
    /// </summary>
    internal class SampleJob : IJob
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public ISampleLogicLayer SampleLogicLayer { get; set; }

        /// <summary>
        /// Called by the <c>Quartz.IScheduler</c> when a <c>Quartz.ITrigger</c> fires that is associated with the <c>Quartz.IJob</c>.
        /// </summary>
        /// <param name="context">JobExecutionContext instance</param>
        public void Execute(IJobExecutionContext context)
        {
            Log.Info("Application executing");

            this.SampleLogicLayer.Run();

            Log.Info("Application executed");
        }
    }
}