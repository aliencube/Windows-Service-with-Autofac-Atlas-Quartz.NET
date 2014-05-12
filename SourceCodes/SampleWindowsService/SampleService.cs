using Atlas;
using Common.Logging;
using Quartz;
using Quartz.Spi;
using System.Configuration;

namespace SampleWindowsService
{
    /// <summary>
    /// This represents an entity for Windows Service hosted by Atlas.
    /// </summary>
    internal class SampleService : IAmAHostedProcess
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets or sets the scheduler instance.
        /// </summary>
        public IScheduler Scheduler { get; set; }           // #1

        /// <summary>
        /// Gets or sets the job factory instance.
        /// </summary>
        public IJobFactory JobFactory { get; set; }         // #2

        /// <summary>
        /// Gets or sets the job listener instance.
        /// </summary>
        public IJobListener JobListener { get; set; }       // #3

        /// <summary>
        /// Starts the Windows Service.
        /// </summary>
        public void Start()
        {
            Log.Info("Sample Windows Service starting");

            var job = JobBuilder.Create<SampleJob>()
                                .WithIdentity("SampleJob", "SampleWindowsService")
                                .Build();                   // #4

            var trigger = TriggerBuilder.Create()
                                        .WithIdentity("SampleTrigger", "SampleWindowsService")
                                        .WithCronSchedule(ConfigurationManager.AppSettings["CronExpression"])   // #5
                                        .ForJob("SampleJob", "SampleWindowsService")
                                        .Build();           // #6

            this.Scheduler.JobFactory = this.JobFactory;    // #7
            this.Scheduler.ScheduleJob(job, trigger);       // #8
            this.Scheduler.ListenerManager.AddJobListener(this.JobListener);    // #9
            this.Scheduler.Start();                         // #10

            Log.Info("Sample Windows Service started");
        }

        /// <summary>
        /// Stops the Windows Service.
        /// </summary>
        public void Stop()
        {
            Log.Info("Sample Windows Service stopping");

            this.Scheduler.Shutdown();

            Log.Info("Sample Windows Service stopped");
        }

        /// <summary>
        /// Resumes the Windows Service.
        /// </summary>
        public void Resume()
        {
            Log.Info("Sample Windows Service resuming");

            this.Scheduler.ResumeAll();

            Log.Info("Sample Windows Service resumed");
        }

        /// <summary>
        /// Pauses the Windows Service.
        /// </summary>
        public void Pause()
        {
            Log.Info("Sample Windows Service pausing");

            this.Scheduler.PauseAll();

            Log.Info("Sample Windows Service paused");
        }
    }
}