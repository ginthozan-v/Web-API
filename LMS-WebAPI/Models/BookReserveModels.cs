using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_WebAPI.Models
{
    public class BookReserveModels
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public System.DateTime ReserveOn { get; set; }
    }
}