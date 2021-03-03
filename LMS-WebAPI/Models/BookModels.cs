using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_WebAPI.Models
{
    public class BookModels
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int? Qty { get; set; }


    }
}