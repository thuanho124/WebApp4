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
    public class RentedBooksController : Controller
    {
        private readonly LibDbContext _context;

        public RentedBooksController(LibDbContext context)
        {
            _context = context;
        }

        // GET: RentedBooks
        public async Task<IActionResult> Index()
        {
            var libDbContext = _context.RentedBooks.Include(r => r.Book).Include(r => r.Student);
            return View(await libDbContext.ToListAsync());
        }

        // GET: RentedBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentedBook = await _context.RentedBooks
                .Include(r => r.Book)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.RentedBookID == id);

            if (rentedBook == null)
            {
                return NotFound();
            }

            return View(rentedBook);
        }

        // GET: RentedBooks/Create
        public IActionResult Create()
        {  
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "BookID");
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID");
            return View();
        }

        // POST: RentedBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentedBookID,StudentID,BookID,RentDate,ReturnDate")] RentedBook rentedBook)
        {
            Book book = _context.Books.Find(rentedBook.BookID);

            var checkBook = _context.RentedBooks
                .Include(r => r.Book)
                .Include(r => r.Student).ToList();
            
            // if there are more than 1, book will be issued
            if (book.AvailableQuantity >= 1)
            {
                    book.AvailableQuantity -= 1;
                    _context.Entry(book).State = EntityState.Modified;

                    if (ModelState.IsValid)
                    {
                        _context.Add(rentedBook);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                
            }
            // if none, give a message telling user that the book is current unavailable
            else
            {
                ModelState.AddModelError(string.Empty,
                    "The book is unavailable! It is rented by other students.");
            }
            
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "BookID", rentedBook.BookID);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID", rentedBook.StudentID);
            return View(rentedBook);
        }

        // GET: RentedBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentedBook = await _context.RentedBooks.FindAsync(id);

            if (rentedBook == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "BookID", rentedBook.BookID);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID", rentedBook.StudentID);
            return View(rentedBook);
        }

        // POST: RentedBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentedBookID,StudentID,BookID,RentDate,ReturnDate")] RentedBook rentedBook)
        {
            if (id != rentedBook.RentedBookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentedBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentedBookExists(rentedBook.RentedBookID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "BookID", rentedBook.BookID);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID", rentedBook.StudentID);
            return View(rentedBook);
        }

        // GET: RentedBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentedBook = await _context.RentedBooks
                .Include(r => r.Book)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.RentedBookID == id);

            if (rentedBook == null)
            {
                return NotFound();
            }

            return View(rentedBook);
        }

        // POST: RentedBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {  
            RentedBook rentedBook = await _context.RentedBooks.FindAsync(id);

            var book = _context.Books.Find(rentedBook.BookID);
            
            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                // when a book is returned, book is back to stock => quantity + 1
                book.AvailableQuantity += 1;
                _context.RentedBooks.Remove(rentedBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
            
        }
        public async Task<IActionResult> ListOfOverdue()
        {
            var libDbContext = _context.RentedBooks.Include(r => r.Book).Include(r => r.Student);
            return View(await libDbContext.ToListAsync());
        }

            private bool RentedBookExists(int id)
        {
            return _context.RentedBooks.Any(e => e.RentedBookID == id);
        }
    }
}
