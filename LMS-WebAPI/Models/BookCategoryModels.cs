using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_WebAPI.Models
{
    public class BookCategoryModels
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CategoryId { get; set; }
    }
}