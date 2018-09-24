using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppTest.Models;
using WebAppTest.Repositories;

namespace WebAppTest.Jobs
{
    public class DoStuffJob : IJob
    {
        private IDog dog;
        //private DogDBcontext context;

        public DoStuffJob(IDog theDog)
        {
            dog = theDog;
            //context = theContext;
        }

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("LaiXi will do all this stuffs everday");

            dog.ShowTheBasic(1);

            //dog.Bark();

            //dog.GulpFood();

            //dog.PlayWithMaster();

            //dog.GulpFood();

            return null;
        }
    }
}
