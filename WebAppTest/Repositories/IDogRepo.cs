using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppTest.Models;

namespace WebAppTest.Repositories
{
    public interface IDogRepo
    {
        Dog GetDogInfo(int id);
    }
}
