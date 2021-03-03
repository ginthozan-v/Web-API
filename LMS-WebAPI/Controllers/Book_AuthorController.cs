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
    public class Book_AuthorController : ApiController
    {
        private LMSEntities db = new LMSEntities();

        // GET: api/Book_Author
        public List<BookAuthorModels> GetBook_Author()
        {
            var bookauthor = (from ba in db.Book_Author

                          select new BookAuthorModels
                          {
                              Id = ba.id,
                              BookId = ba.book_id,
                              AuthorId = ba.author_id
                          }).ToList();
            return bookauthor;
        }

        // GET: api/Book_Author/5
        [ResponseType(typeof(Book_Author))]
        public IHttpActionResult GetBook_Author(int id)
        {
            Book_Author book_Author = db.Book_Author.Find(id);
            if (book_Author == null)
            {
                return NotFound();
            }

            return Ok(book_Author);
        }

        // PUT: api/Book_Author/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook_Author(int id, Book_Author book_Author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book_Author.id)
            {
                return BadRequest();
            }

            db.Entry(book_Author).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Book_AuthorExists(id))
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

        // POST: api/Book_Author
        [ResponseType(typeof(Book_Author))]
        public IHttpActionResult PostBook_Author(Book_Author book_Author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Book_Author.Add(book_Author);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book_Author.id }, book_Author);
        }

        // DELETE: api/Book_Author/5
        [ResponseType(typeof(Book_Author))]
        public IHttpActionResult DeleteBook_Author(int id)
        {
            Book_Author book_Author = db.Book_Author.Find(id);
            if (book_Author == null)
            {
                return NotFound();
            }

            db.Book_Author.Remove(book_Author);
            db.SaveChanges();

            return Ok(book_Author);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Book_AuthorExists(int id)
        {
            return db.Book_Author.Count(e => e.id == id) > 0;
        }
    }
}