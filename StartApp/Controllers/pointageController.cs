﻿using Microsoft.AspNetCore.Mvc;
using StarApp.Core.Models.Compta;

namespace StartApp.Controllers
{
    public class pointageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(pointage model)
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View();
        }
        public IActionResult Edit(int id, pointage model)
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            return View();
        }


    }
}
