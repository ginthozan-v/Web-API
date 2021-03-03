using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_WebAPI.Models
{
    public class BookAuthorModels
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }
}