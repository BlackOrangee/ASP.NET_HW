using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_HW.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookController : Controller
    {

        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "To Kill a Mockingbird", Author = "Harper Lee", Year = 1960 },
            new Book { Id = 2, Title = "1984", Author = "George Orwell", Year = 1949 },
            new Book { Id = 3, Title = "Pride and Prejudice", Author = "Jane Austen", Year = 1813 },
            new Book { Id = 4, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Year = 1925 },
            new Book { Id = 5, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Year = 1951 },
        };

        private static List<int> bookIds = new List<int> { 1, 2, 3, 4, 5 };


        [HttpGet (Name = "GetBook")]
        public ActionResult<IEnumerable<Book>> GetBooks(){
            return Ok(books);
        }

        [HttpGet("{id}", Name = "GetBookById")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = books.Find(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost(Name = "AddBook")]
        public ActionResult<Book> AddBook(Book book)
        {
            book.Id = bookIds.Max() + 1;
            bookIds.Add(book.Id);
            books.Add(book);
            return CreatedAtRoute("GetBookById", new { id = book.Id }, book);
        }

        [HttpDelete("{id}", Name = "DeleteBook")]
        public ActionResult DeleteBook(int id)
        {
            var book = books.Find(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            books.Remove(book);
            return Ok(new { message = "Book deleted successfully" });
        }

        [HttpPut("{id}", Name = "UpdateBook")]
        public ActionResult UpdateBook(int id, Book book)
        {
            var existingBook = books.Find(b => b.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Year = book.Year;
            return Ok(existingBook);
        }
    }
}
