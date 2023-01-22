﻿using BookStore.Models.Domain;
using BookStore.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService service;

        public PublisherController(IPublisherService service)
        {
            this.service = service;
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Publisher model)
        {
            if (!ModelState.IsValid) { 
             return View(model);
            }
            var result= service.Add(model);
            if (result)
            {
                TempData["msg"] = "Data Save Success!";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error Occured!!";
            return View(model);
        }
        public IActionResult Update(int id)
        {
            var record= service.FindById(id);
            return View(record);
        }
        [HttpPost]
        public IActionResult Update(Publisher model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Update(model);
            if (result)
            {
                //TempData["msg"] = "Data Update Success!";
                return RedirectToAction(nameof(GetAll));
            }
            TempData["msg"] = "Error Occured!!";
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var result = service.Delete(id);

            return RedirectToAction(nameof(GetAll));
        }
        public IActionResult GetAll()
        {
            var data = service.GetAll();

            return View(data);
        }

    }
}
