using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTest.Models
{
    public interface IDog
    {
        void Bark();

        void GulpFood();

        void PlayWithMaster();

        void ShowTheBasic(int id);
    }
}
