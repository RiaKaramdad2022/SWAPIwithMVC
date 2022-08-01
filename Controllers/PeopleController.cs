using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SwapiMVC.Models;

namespace SwapiMVC.Controllers
{
    // The class controller brings in MVC functionality like view() which allows us to connect our controller code to our .cshtml files.
    public class PeopleController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}