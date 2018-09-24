using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using WebAppTest.Jobs;
using WebAppTest.Models;
using WebAppTest.Repositories;

namespace WebAppTest
{
    public class QuartzStartup
    {
        private IScheduler _scheduler; // after Start, and until shutdown completes, references the scheduler object
        private readonly IServiceProvider serProd;
        private IDog dog;
        private DogDBcontext context;

        //public QuartzStartup(IDog theDog, DogDBcontext theContext)
        //{
        //    dog = theDog;
        //    context = theContext;
        //}

        public QuartzStartup(IServiceProvider theProd)
        {
            serProd = theProd;
        }

        // starts the scheduler, defines the jobs and the triggers
        public void Start()
        {
            if (_scheduler != null)
            {
                throw new InvalidOperationException("Already started.");
            }

            //var properties = new NameValueCollection
            //{
            //    // json serialization is the one supported under .NET Core (binary isn't)
            //    ["quartz.serializer.type"] = "json",

            //    // the following setup of job store is just for example and it didn't change from v2
            //    ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",
            //    ["quartz.jobStore.useProperties"] = "false",
            //    ["quartz.jobStore.dataSource"] = "Home",
            //    ["quartz.jobStore.tablePrefix"] = "QRTZ_",
            //    ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.MySQLDelegate, Quartz",
            //    ["quartz.dataSource.default.provider"] = "MySql-61", // SqlServer-41 is the new provider for .NET Core
            //    ["quartz.dataSource.default.connectionString"] = @"Server=localhost;database=Dog;uid=root;pwd=Lx5826698;"
            //};

            //var schedulerFactory = new StdSchedulerFactory(properties);
            var schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler().Result;
            //_scheduler.JobFactory = new JobFactory(dog);
            //_scheduler.Start().Wait();
            _scheduler.JobFactory = new JobFactory(serProd);

            var laixiJog = JobBuilder.Create<DoStuffJob>()
                .WithIdentity("LaiXiIsLiving")
                .Build();
            var laixiRunningSche = TriggerBuilder.Create()
                .WithIdentity("DogBehavior")
                .StartNow()
                .WithSimpleSchedule(s => s.WithIntervalInSeconds(30).RepeatForever())
                //.WithCronSchedule("0 * 8 * * ?")
                .Build();

            Console.WriteLine("The dog behavior job is created and scheduled.");
            _scheduler.ScheduleJob(laixiJog, laixiRunningSche).Wait();
            _scheduler.Start().Wait();

            //var adminEmailsJob = JobBuilder.Create<SendAdminEmailsJob>()
            //    .WithIdentity("SendAdminEmails")
            //    .Build();
            //var adminEmailsTrigger = TriggerBuilder.Create()
            //    .WithIdentity("AdminEmailsCron")
            //    .StartNow()
            //    .WithCronSchedule("0 0 9 ? * THU,FRI")
            //    .Build();

            //_scheduler.ScheduleJob(adminEmailsJob, adminEmailsTrigger).Wait();
        }

        // initiates shutdown of the scheduler, and waits until jobs exit gracefully (within allotted timeout)
        public void Stop()
        {
            if (_scheduler == null)
            {
                return;
            }

            // give running jobs 30 sec (for example) to stop gracefully
            if (_scheduler.Shutdown(waitForJobsToComplete: true).Wait(30000))
            {
                _scheduler = null;
            }
            else
            {
                // jobs didn't exit in timely fashion - log a warning...
            }
        }
    }
}
