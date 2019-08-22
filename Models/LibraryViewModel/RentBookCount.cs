using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementWithAuthen.Models.LibraryViewModel
{
    // model for querying and displaying the view for the dashboard
    public class RentBookCount
    {
        public int BookTotal { get; set; }
        public int RentedBookCount { get; set; }
        public int OverdueBook { get; set; }
    }
}