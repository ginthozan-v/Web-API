using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LMS_WebAPI;
using LMS_WebAPI.Models;

namespace LMS_WebAPI.Controllers
{
    [Authorize]
    public class Book_CategoryController : ApiController
    {
        private LMSEntities db = new LMSEntities();


        // GET: api/Book_Category
        public List<BookCategoryModels> GetBook_Category()
        {
            var bookCategory = (from bc in db.Book_Category

                                select new BookCategoryModels
                                {
                                    Id = bc.id,
                                    BookId = bc.book_id,
                                    CategoryId = bc.category_id
                                }).ToList();
            return bookCategory;
        }

        // GET: api/Book_Category/5
        [ResponseType(typeof(Book_Category))]
        public IHttpActionResult GetBook_Category(int id)
        {
            Book_Category book_Category = db.Book_Category.Find(id);
            if (book_Category == null)
            {
                return NotFound();
            }

            return Ok(book_Category);
        }

        // PUT: api/Book_Category/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook_Category(int id, Book_Category book_Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book_Category.id)
            {
                return BadRequest();
            }

            db.Entry(book_Category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Book_CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Book_Category
        [ResponseType(typeof(Book_Category))]
        public IHttpActionResult PostBook_Category(Book_Category book_Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Book_Category.Add(book_Category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book_Category.id }, book_Category);
        }

        // DELETE: api/Book_Category/5
        [ResponseType(typeof(Book_Category))]
        public IHttpActionResult DeleteBook_Category(int id)
        {
            Book_Category book_Category = db.Book_Category.Find(id);
            if (book_Category == null)
            {
                return NotFound();
            }

            db.Book_Category.Remove(book_Category);
            db.SaveChanges();

            return Ok(book_Category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Book_CategoryExists(int id)
        {
            return db.Book_Category.Count(e => e.id == id) > 0;
        }
    }
}