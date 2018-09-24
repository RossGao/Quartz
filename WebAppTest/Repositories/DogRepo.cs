using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppTest.Models;

namespace WebAppTest.Repositories
{
    public class DogRepo : IDogRepo
    {
        private DogDBcontext context;

        public DogRepo(DogDBcontext theContext)
        {
            context = theContext;
        }

        public Dog GetDogInfo(int id)
        {
            if (id != 0)
            {
                return context.Dogs.Find(id);
            }

            return null;
        }
    }
}
