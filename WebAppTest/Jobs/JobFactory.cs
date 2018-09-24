using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppTest.Models;
using WebAppTest.Repositories;

namespace WebAppTest.Jobs
{
    public class JobFactory : IJobFactory
    {
        private IDog dog;

        //private DogDBcontext context;

        private IServiceProvider servProd;

        //public JobFactory(IDog theDog)
        //{
        //    dog = theDog;
        //    context = theContext;
        //}

        public JobFactory(IServiceProvider theProd)
        {
            servProd = theProd;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return servProd.GetService(bundle.JobDetail.JobType) as IJob;
            //return new DoStuffJob(dog) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            (job as IDisposable)?.Dispose();
        }
    }
}
