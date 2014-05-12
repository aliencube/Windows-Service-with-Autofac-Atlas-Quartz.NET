using Atlas;
using Autofac;
using Common.Logging;
using System;
using System.Linq;

namespace SampleWindowsService
{
    /// <summary>
    /// This represents the Windows Service application entity.
    /// </summary>
    internal class Program
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// This represents the main entry point of the Windows Service application.
        /// </summary>
        /// <param name="args">List of arguments.</param>
        private static void Main(string[] args)
        {
            try
            {
                var configuration = Host.UseAppConfig<SampleService>()  // #1
                                        .AllowMultipleInstances()       // #2
                                        .WithRegistrations(p => p.RegisterModule(new SampleModule()));  // #3
                if (args != null && args.Any())
                    configuration = configuration.WithArguments(args);  // #4

                Host.Start(configuration);                              // #5
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception during startup.", ex);
                Console.ReadLine();
            }
        }
    }
}