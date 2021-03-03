using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_WebAPI.Models
{
    public class BookLendModels
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ExtendedDate { get; set; }
        public int Returned { get; set; }
        public string ActionType { get; set; }
    }
}