using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTest.Controllers
{
    public class TestController:Controller
    {
        public IActionResult GetTestResutl()
        {
            return Ok();
        }
    }
}
