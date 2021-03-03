using LMS_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMS_WebAPI.Controllers
{
    [Authorize]
    public class BookByCategoryController : ApiController
    {
        private LMSEntities db = new LMSEntities();

        // GET: api/BookByCategory/5
        //public List<CategorizedBook> GetBookBy_Category(int id)
        //{
        //    var bookCategory = (from bc in db.Book_Category
        //                        where bc.category_id == id
        //                        select new CategorizedBook
        //                        {
        //                            Id = bc.id,
        //                            BookId = bc.book_id,
        //                            CategoryId = bc.category_id,
        //                            CategoryName = bc.Category.category_name,
        //                            BookTitle = bc.Book.title,
        //                            BookImage = bc.Book.image,
        //                        }).ToList();
        //    return bookCategory;
        //}


        //public BookCategoryModels GetBookBy_Category(int id)
        //{
        //    var bookCategory = (from c in db.Categories
        //                        where c.id == id
        //                        select new BookCategoryModels
        //                        {
        //                            Id = c.id,
        //                            CategoryName = c.category_name,

        //                            BookList = (from bc in db.Book_Category
        //                                        where bc.category_id == c.id

        //                                        select new BookModels
        //                                        {
        //                                            Id = bc.Book.id,
        //                                            Title = bc.Book.title,
        //                                            Image = bc.Book.image,
        //                                            //Author = ba.Author.name

        //                                        }).ToList()
        //                        }).FirstOrDefault();
        //    return bookCategory;
        //}

    }
}
