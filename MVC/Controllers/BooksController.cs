#nullable disable
using APP.Models;
using CORE.APP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// Generated from Custom MVC Template.

namespace MVC.Controllers
{
    [AllowAnonymous]
    public class BooksController : Controller
    {
        // Service injections:
        private readonly IService<BookRequest, BookResponse> _bookService;

        /* Can be uncommented and used for many to many relationships, "entity" may be replaced with the related entity name in the controller and views. */
        //private readonly IService<EntityRequest, EntityResponse> _EntityService;
        private readonly IService<AuthorRequest, AuthorResponse> _authorService;
        private readonly IService<GenreRequest, GenreResponse> _genreService;

        public BooksController(
        IService<BookRequest, BookResponse> bookService,
        IService<AuthorRequest, AuthorResponse> authorService,
        IService<GenreRequest, GenreResponse> genreService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
        }

        private void SetViewData()
        {
            ViewBag.Authors = new SelectList(_authorService.List(), "Id", "FullName");
            ViewBag.Genres = new SelectList(_genreService.List(), "Id", "Name");
        }

        private void SetTempData(string message, string key = "Message")
        {
            /*
            TempData is used to carry extra data to the redirected controller action's view.
            */

            TempData[key] = message;
        }

        // GET: Books
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _bookService.List();
            return View(list); // return response collection as model to the Index view
        }

        // GET: Books/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _bookService.Item(id);
            return View(item); // return response item as model to the Details view
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(); // return Create view with no model
        }

        // POST: Books/Create
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(BookRequest book)
        {
            if (ModelState.IsValid) // check data annotation validation errors in the request
            {
                // Insert item service logic:
                var response = _bookService.Create(book);
                if (response.IsSuccessful)
                {
                    SetTempData(response.Message); // set TempData dictionary to carry the message to the redirected action's view
                    return RedirectToAction(nameof(Details), new { id = response.Id }); // redirect to Details action with id parameter as response.Id route value
                }
                ModelState.AddModelError("", response.Message); // to display service error message in the validation summary of the view
            }
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(book); // return request as model to the Create view
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _bookService.Edit(id);
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(item); // return request as model to the Edit view
        }

        // POST: Books/Edit
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(BookRequest book)
        {
            if (ModelState.IsValid) // check data annotation validation errors in the request
            {
                // Update item service logic:
                var response = _bookService.Update(book);
                if (response.IsSuccessful)
                {
                    SetTempData(response.Message); // set TempData dictionary to carry the message to the redirected action's view
                    return RedirectToAction(nameof(Details), new { id = response.Id }); // redirect to Details action with id parameter as response.Id route value
                }
                ModelState.AddModelError("", response.Message); // to display service error message in the validation summary of the view
            }
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(book); // return request as model to the Edit view
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _bookService.Item(id);
            return View(item); // return response item as model to the Delete view
        }

        // POST: Books/Delete
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var response = _bookService.Delete(id);
            SetTempData(response.Message); // set TempData dictionary to carry the message to the redirected action's view
            return RedirectToAction(nameof(Index)); // redirect to the Index action
        }
    }
}
