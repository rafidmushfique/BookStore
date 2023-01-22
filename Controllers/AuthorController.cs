﻿using BookStore.Models.Domain;
using BookStore.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService service;

        public AuthorController(IAuthorService service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(author model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Add(model);
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
            var record = service.FindById(id);
            return View(record);
        }
        [HttpPost]
        public IActionResult Update(author model)
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
