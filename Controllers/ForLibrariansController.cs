using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagementWithAuthen.Data;
using LibraryManagementWithAuthen.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementWithAuthen.Controllers
{
    [Authorize]
    public class ForLibrariansController : Controller
    {
        private readonly LibDbContext _context;

        public ForLibrariansController(LibDbContext context)
        {
            _context = context;
        }

        // GET: ForLibrarians
        public async Task<IActionResult> Index(string search, string sortOrder, int? pageNumber)
        {
            
            ViewData["BookNameSort"] = String.IsNullOrEmpty(sortOrder) ? "BookName_desc" : "";
            ViewData["PublicDateSort"] = sortOrder == "PublicDate" ? "PublicDate_desc" : "PublicDate";
            ViewData["QuantitySort"] = sortOrder == "AvailableQuantity" ? "AvailableQuantity_desc" : "AvailableQuantity";
            ViewData["AuthorSort"] = sortOrder == "Author" ? "Author_desc" : "Author";
            ViewData["SubjectSort"] = sortOrder == "Subject" ? "Subject_desc" : "Subject";
            ViewData["FormatSort"] = sortOrder == "Format" ? "Format_desc" : "Format";
            ViewData["NumofPagesSort"] = sortOrder == "NumofPages" ? "NumofPages_desc" : "NumofPages";
            ViewData["CurrentFilter"] = search;
            ViewData["CurrentSort"] = sortOrder;

            var books = from s in _context.Books
                             select s;

            // search a book 
            if (!String.IsNullOrEmpty(search))
            {
                books = books.Where(s => s.BookName.Contains(search)
                                    || s.Author.Contains(search)
                                    || s.ISBN.ToString().Contains(search)
                                    || s.Subject.Contains(search));
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "BookName";
            }

            // order by descending or ascending
            bool descending = false;

            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            // if descending = true, values in the view will be sorted by descending by passing sortOrder into EF Property method
            if (descending)
            {
                books = books.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }

            // if it is false, then  values in the view will be sorted by ascending by passing sortOrder into EF Property method
            else
            {
                books = books.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            int pageSize = 6;
            return View(await PaginatedList<Book>.CreateAsync(books.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: ForLibrarians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get each entities for each book
            // then get each RentedBooks Entities 
            var book = await _context.Books
                .Include(r => r.RentedBooks)
                    .ThenInclude(b => b.Student)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: ForLibrarians/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ForLibrarians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookName,Author,Edition,ISBN,Subject,PublicDate,Format,NumofPages,AvailableQuantity")] Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Please check if there are any errors with the database");
            }

            return View(book);
        }

        // GET: ForLibrarians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: ForLibrarians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booktoUpdate = await _context.Books.FirstOrDefaultAsync(b => b.BookID == id);

            // update book through updatemodelasync
            if (await TryUpdateModelAsync<Book>(
                booktoUpdate,
                "",
                b => b.BookName, b => b.Author, b => b.Edition,
                b => b.ISBN, b => b.Subject,
                b => b.PublicDate, b => b.Format, b => b.NumofPages, b => b.AvailableQuantity))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, or check your database ");
                }
            }

            return View(booktoUpdate);
        }

        // GET: ForLibrarians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: ForLibrarians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            catch
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }

        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }

    }
}
