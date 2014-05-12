using Atlas;
using Autofac;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Reflection;
using Module = Autofac.Module;

namespace SampleWindowsService
{
    /// <summary>
    /// This represents an entity for Autofac module.
    /// </summary>
    internal class SampleModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            this.LoadQuartz(builder);
            this.LoadServices(builder);
            this.LoadLogicLayers(builder);
        }

        /// <summary>
        /// Loads the quartz scheduler instance.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        private void LoadQuartz(ContainerBuilder builder)
        {
            builder.Register(c => new StdSchedulerFactory().GetScheduler())
                   .As<IScheduler>()
                   .InstancePerLifetimeScope(); // #1
            builder.Register(c => new SampleJobFactory(ContainerProvider.Instance.ApplicationContainer))
                   .As<IJobFactory>();          // #2
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(p => typeof(IJob).IsAssignableFrom(p))
                   .PropertiesAutowired();      // #3
            builder.Register(c => new SampleJobListener(ContainerProvider.Instance))
                   .As<IJobListener>();         // #4
        }

        /// <summary>
        /// Loads the service instance.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        private void LoadServices(ContainerBuilder builder)
        {
            builder.RegisterType<SampleService>()
                   .As<IAmAHostedProcess>()
                   .PropertiesAutowired();      // #5
        }

        /// <summary>
        /// Loads the logic layers.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        private void LoadLogicLayers(ContainerBuilder builder)
        {
            builder.RegisterType<SampleLogicLayer>()
                   .As<ISampleLogicLayer>();    // #6
        }
    }
}