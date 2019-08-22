using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementWithAuthen.Models;
namespace LibraryManagementWithAuthen.Data
{
    public class DbInitializer
    {
        public static void Initialize(LibDbContext context)
        {
            context.Database.EnsureCreated();

            // initial data for BOOK
            if (context.Books.Any())
            {
                return;
            }
            var books = new Book[]
            {
                new Book{BookName="Book 1", Author="Author 1", Edition="1st", ISBN = 111111111, Subject="Math", PublicDate = DateTime.Parse("2005-09-01"),
                        Format="Paperback", NumofPages = 111, AvailableQuantity = 1},

                new Book{BookName="Book 2", Author="Author 2", Edition="2nd", ISBN = 222222222, Subject="Physic", PublicDate = DateTime.Parse("2002-09-01"),
                        Format="Paperback", NumofPages = 222, AvailableQuantity = 2},

                new Book{BookName="Book 3", Author="Author 3", Edition="3rd", ISBN = 333333333, Subject="Chemistry", PublicDate = DateTime.Parse("2003-09-01"),
                        Format="Paperback", NumofPages = 333, AvailableQuantity = 3},

                new Book{BookName="Book 4", Author="Author 4", Edition="4th", ISBN = 444444444, Subject="Psychology", PublicDate = DateTime.Parse("2005-09-01"),
                        Format="Paperback", NumofPages = 444, AvailableQuantity = 4},

                new Book{BookName="Book 5", Author="Author 5", Edition="5th", ISBN = 555555555, Subject="Math", PublicDate = DateTime.Parse("2007-08-02"),
                        Format="Paperback", NumofPages = 555, AvailableQuantity = 5},

                new Book{BookName="Book 6", Author="Author 6", Edition="6th", ISBN = 666666666, Subject="Accounting", PublicDate = DateTime.Parse("2010-09-01"),
                        Format="Paperback", NumofPages = 666, AvailableQuantity = 6},

                new Book{BookName="Book 7", Author="Author 7", Edition="7th", ISBN = 777777777, Subject="Architecture", PublicDate = DateTime.Parse("2012-09-05"),
                        Format="Paperback", NumofPages = 777, AvailableQuantity = 7},

                new Book{BookName="Book 8", Author="Author 8", Edition="8th", ISBN = 888888888, Subject="Film Making", PublicDate = DateTime.Parse("2006-09-01"),
                        Format="Paperback", NumofPages = 888, AvailableQuantity = 8},
            };
            foreach (Book b in books)
            {
                context.Books.Add(b);
            }
            context.SaveChanges();



            // initial data for STUDENT
            var students = new Student[]
            {
                new Student{StudentFirstName="First Name 1", StudentLastName="Last Name 1", StudentEmail="studentemail1@uw.edu", StudentPhoneNumber= "1111111111"},

                new Student{StudentFirstName="First Name 2", StudentLastName="Last Name 2", StudentEmail="studentemail2@uw.edu", StudentPhoneNumber= "2222222222"},

                new Student{StudentFirstName="First Name 3", StudentLastName="Last Name 3", StudentEmail="studentemail3@uw.edu", StudentPhoneNumber= "3333333333"},

                new Student{StudentFirstName="First Name 4", StudentLastName="Last Name 4", StudentEmail="studentemail4@uw.edu", StudentPhoneNumber= "4444444444"},

                new Student{StudentFirstName="First Name 5", StudentLastName="Last Name 5", StudentEmail="studentemail5@uw.edu", StudentPhoneNumber= "5555555555"}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();



            // initial data for LIST OF RENTED BOOKS
            var rentedbooks = new RentedBook[]
            {
                new RentedBook{StudentID = 1, BookID = 2, RentDate = DateTime.Parse("2019-08-17"), ReturnDate = DateTime.Parse("2019-08-21")},

                new RentedBook{StudentID = 1, BookID = 1, RentDate = DateTime.Parse("2019-08-20"), ReturnDate = DateTime.Parse("2019-08-27")},

                new RentedBook{StudentID = 3, BookID = 4, RentDate = DateTime.Parse("2019-08-10"), ReturnDate = DateTime.Parse("2019-08-17")},

                new RentedBook{StudentID = 2, BookID = 5, RentDate = DateTime.Parse("2019-08-16"), ReturnDate = DateTime.Parse("2019-08-26")},
            };
            foreach (RentedBook rb in rentedbooks)
            {
                context.RentedBooks.Add(rb);
            }
            context.SaveChanges();

        }
    }
}
