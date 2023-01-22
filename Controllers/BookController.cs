using BookStore.Models.Domain;
using BookStore.Repository.Abstract;
using BookStore.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService bookservice;
        private readonly IGenreService genreService;
        private readonly IAuthorService authorService;
        private readonly IPublisherService publisherService;

        public BookController(IBookService bookservice,IGenreService genreService,IAuthorService authorService,IPublisherService publisherService)
        {
            this.bookservice = bookservice;
            this.genreService = genreService;
            this.authorService = authorService;
            this.publisherService = publisherService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            var model=new Book();
            model.AuthorList=authorService.GetAll().Select(author=> new SelectListItem { Text=author.authorname,Value=author.id.ToString()}).ToList();
            model.PublisherList = publisherService.GetAll().Select(publisher => new SelectListItem { Text = publisher.publishername, Value = publisher.id.ToString() }).ToList();
            model.GenreList = genreService.GetAll().Select(genre => new SelectListItem { Text = genre.name, Value = genre.id.ToString() }).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(Book model)
        {
          
            model.AuthorList = authorService.GetAll().Select(author => new SelectListItem { Text = author.authorname, Value = author.id.ToString(),Selected=author.id==model.AuthorId }).ToList();
            model.PublisherList = publisherService.GetAll().Select(publisher => new SelectListItem { Text = publisher.publishername, Value = publisher.id.ToString(), Selected = publisher.id == model.PublisherId }).ToList();
            model.GenreList = genreService.GetAll().Select(genre => new SelectListItem { Text = genre.name, Value = genre.id.ToString(), Selected = genre.id == model.GenreId }).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = bookservice.Add(model);
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
            var model = bookservice.FindById(id);
            model.AuthorList = authorService.GetAll().Select(author => new SelectListItem { Text = author.authorname, Value = author.id.ToString(), Selected = author.id == model.AuthorId }).ToList();
            model.PublisherList = publisherService.GetAll().Select(publisher => new SelectListItem { Text = publisher.publishername, Value = publisher.id.ToString(), Selected = publisher.id == model.PublisherId }).ToList();
            model.GenreList = genreService.GetAll().Select(genre => new SelectListItem { Text = genre.name, Value = genre.id.ToString(), Selected = genre.id == model.GenreId }).ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult Update(Book model)
        {
            model.AuthorList = authorService.GetAll().Select(author => new SelectListItem { Text = author.authorname, Value = author.id.ToString(), Selected = author.id == model.AuthorId }).ToList();
            model.PublisherList = publisherService.GetAll().Select(publisher => new SelectListItem { Text = publisher.publishername, Value = publisher.id.ToString(), Selected = publisher.id == model.PublisherId }).ToList();
            model.GenreList = genreService.GetAll().Select(genre => new SelectListItem { Text = genre.name, Value = genre.id.ToString(), Selected = genre.id == model.GenreId }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = bookservice.Update(model);
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
            var result = bookservice.Delete(id);

            return RedirectToAction(nameof(GetAll));
        }
        public IActionResult GetAll()
        {
            var data = bookservice.GetAll();

            return View(data);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
    }
}
