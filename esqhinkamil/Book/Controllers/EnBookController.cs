using esqhinkamil.Book.Database;
using esqhinkamil.Book.Database.Models;
using esqhinkamil.Book.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace esqhinkamil.Book.Controllers
{
    [Route("Book")]
    public class EnBookController : Controller
    {
        [HttpGet("add",Name ="EnBook-add")]
        public IActionResult Add()
        {
            return View("~/Book/Views/Enbook/Add.cshtml");
        }
        [HttpPost("add", Name = "EnBook-add-model")]
        public IActionResult Add(AddViewModel Book)
        {

            if (!ModelState.IsValid)
            {
                return View("~/Book/Views/Enbook/Add.cshtml", Book);
            }

            Database.DatabaseAccess.EnBooks.Add(new EnBook
            {
                Id = TablePkAutoincrement.BookCounter,
                Title = Book.Title,
                Author = Book.Author,
                Price = Book.Price.Value,
                CreatedAt = DateTime.UtcNow


            });


            return RedirectToRoute("EnBook-List");
        }


        [HttpGet("List", Name = "EnBook-List")]
        public IActionResult List()
        {
            var book = Database.DatabaseAccess.EnBooks
                .Select(e => new ListItemViewModel(e.Id, e.Title, e.Price, e.CreatedAt))
                .ToList();

            return View("~/Book/Views/Enbook/List.cshtml",book);
        }


        [HttpGet("update/{id}", Name = "book-update-id")]
        public IActionResult Update(int id)
        {
            var book = DatabaseAccess.EnBooks.FirstOrDefault(e=>e.Id==id);
            if (book == null)
            {
                return NotFound();
            }

            return View("~/Book/Views/Enbook/Update.cshtml",new UpdateViewModel
            {
                Id=book.Id,
                Title=book.Title,   
                Author=book.Author,
                Price=book.Price
            });
        }
        [HttpPost("update", Name = "book-update")]
        public IActionResult Update(UpdateViewModel book)
        {
            var enbook = Database.DatabaseAccess.EnBooks.FirstOrDefault(e => e.Id == book.Id);

            if (enbook is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View("~/Book/Views/Enbook/Update.cshtml", enbook);

            }

            enbook.Title = book.Title;
            enbook.Author = book.Author;
            enbook.Price = book.Price.Value;

            return RedirectToAction(nameof(List));
        }

        [HttpGet("Delete/{id}", Name = "EnBook-Delete-id")]
        public IActionResult Delete(int id)
        {
            var book = Database.DatabaseAccess.EnBooks.FirstOrDefault(e => e.Id == id);

            if (book is null)
            {
                return NotFound();
            }

            Database.DatabaseAccess.EnBooks.Remove(book);


            return RedirectToRoute("EnBook-List");
        }
        [HttpGet("Delete", Name = "EnBook-Delete-bulk")]
        public IActionResult Delete()
        {
           
            Database.DatabaseAccess.EnBooks.Clear();


            return RedirectToRoute("EnBook-List");
        }

        [HttpGet("Details/{id}", Name = "EnBook-Details")]
        public IActionResult Details(int id)
        {
            var book = Database.DatabaseAccess.EnBooks.Where(e => e.Id == id).FirstOrDefault();

            if (book is null)
            {
                return NotFound();
            }

            var model = new DetailsViewModel(book.Title, book.Author, book.Price, book.CreatedAt);


            return View("~/Book/Views/Enbook/Details.cshtml",model);
        }
    }
}
