//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LMS_WebAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book_Category
    {
        public int id { get; set; }
        public int book_id { get; set; }
        public int category_id { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Book Book { get; set; }
    }
}
