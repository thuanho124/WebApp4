using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementWithAuthen.Models;
using LibraryManagementWithAuthen.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using LibraryManagementWithAuthen.Models.LibraryViewModel;
namespace LibraryManagementWithAuthen.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly LibDbContext _context;

        public HomeController(LibDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string search)
        {
            ViewData["CurrentSearch"] = search;

            var books = from s in _context.Books
                          select s;

            if (!String.IsNullOrEmpty(search))
            {
                books = books.Where(s => s.BookName.Contains(search)
                                    || s.Author.Contains(search)
                                    || s.ISBN.ToString().Contains(search)
                                    || s.Subject.Contains(search));
            }

            return View(await books.AsNoTracking().ToListAsync());
        }


        [AllowAnonymous] // allow anyone to access to home page to find a book
        public IActionResult Privacy()
        {
            return View();

        }
        [Authorize] // need an account to login 
        public async Task<ActionResult> Dashboard()
        {
            // query and display rent book count
            List<RentBookCount> groups = new List<RentBookCount>();
            var conn = _context.Database.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command_rentcount = conn.CreateCommand())
                {
                    string query_rentcount = "SELECT COUNT(*) AS RentedBookCount FROM RentedBook;";

                    command_rentcount.CommandText = query_rentcount;
                    DbDataReader reader = await command_rentcount.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new RentBookCount { RentedBookCount = reader.GetInt32(0) };
                            groups.Add(row);
                        }
                    }
                    reader.Dispose();
                }

                // query and display total quantity 
                using (var command_booktotal = conn.CreateCommand())
                {
                    string query_booktotal = "SELECT SUM(AvailableQuantity) FROM Book;";

                    command_booktotal.CommandText = query_booktotal;
                    DbDataReader reader = await command_booktotal.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new RentBookCount { BookTotal = reader.GetInt32(0) };
                            groups.Add(row);
                        }
                    }
                    reader.Dispose();
                }

                // query to count overdue books
                using (var command_overdue = conn.CreateCommand())
                {
                    string query_overdue = "SELECT COUNT(*) AS OverdueBook FROM RentedBook WHERE ReturnDate < GETDATE();";

                    command_overdue.CommandText = query_overdue;
                    DbDataReader reader = await command_overdue.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new RentBookCount { OverdueBook = reader.GetInt32(0) };
                            groups.Add(row);
                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            // configure the view
            return View(groups);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
