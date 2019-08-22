using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementWithAuthen.Models
{
    public class Student
    {
        public int StudentID { get; set; } // primary key

        [Required]
        [StringLength(30)]
        [Display(Name = "First Name")]
        public string StudentFirstName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Last Name")]
        public string StudentLastName { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Addres")]
        public string StudentEmail { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string StudentPhoneNumber { get; set; }

        [Display(Name = "Full Name")]
        public string StudentFullName
        {
            get { return StudentLastName + ", " + StudentFirstName; }
        }


        // navigation property
        public ICollection<RentedBook> RentedBooks { get; set; }
    }
}
