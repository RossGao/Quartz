using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppTest.Repositories;

namespace WebAppTest.Models
{
    public class Collie : Dog, IDog
    {
        //private IDogRepo repo;

        //public Collie(IDogRepo theRepo)
        //{
        //    repo = theRepo;
        //}

        public void Bark()
        {
            Console.WriteLine("Wang, Wangwang!");
        }

        public void GulpFood()
        {
            Console.WriteLine("Wait until there is no food given from master then lick the plate very quickly.");
        }

        public void PlayWithMaster()
        {
            Console.WriteLine("Play football wiht the master and like a very good goalkeeper");
        }

        public void ShowTheBasic(int id)
        {
            //var laixi = repo.GetDogInfo(id);

            //if (laixi != null)
            //{
            //    Console.WriteLine($"Laixi's basic info is:" +
            //        $"Name:{laixi.Name}," +
            //        $"Coloe:{laixi.FurColor}," +
            //        $"Age:{laixi.Age}," +
            //        $"Sex:{laixi.Sex}");
            //}
            Console.WriteLine("This is LaiXi");
        }
    }
}
