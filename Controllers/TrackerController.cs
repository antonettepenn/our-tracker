using Microsoft.AspNetCore.Mvc;
using OurTracker.Models;
using System;
using System.Collections.Generic;

namespace OurTracker.Controllers
{
    public class TrackerController : Controller
    {
        // temp database storing inn mem
        public static List<Entry> entries = new();

        // display list of all entries
        public IActionResult Index()
        {
            return View("Index", entries);
        }


        // display form
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // form submission handling
        [HttpPost]
        public IActionResult Add(Entry entry)
        {
            entry.CreatedAt = DateTime.Now;
            entries.Add(entry);
            return RedirectToAction("Index");
        }
    }
}