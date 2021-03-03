using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LMS_WebAPI;
using LMS_WebAPI.Models;

namespace LMS_WebAPI.Controllers
{
    public class Book_ReserveController : ApiController
    {
        private LMSEntities db = new LMSEntities();

        // GET: api/Book_Reserve
        public Task<List<BookReserveModels>> GetBook_Reserve()
        {
            var bookReserve = (from br in db.Book_Reserve

                            select new BookReserveModels
                            {
                                Id = br.id,
                                BookId = br.book_id,
                                UserId = br.user_id,
                                ReserveOn = br.reserve_on
                            }).ToList();
            return Task.Run(() => bookReserve);
        }

        // GET: api/Book_Reserve/5
        [ResponseType(typeof(Book_Reserve))]
        public async Task<IHttpActionResult> GetBook_Reserve(int id)
        {
            Book_Reserve book_Reserve = await db.Book_Reserve.FindAsync(id);
            if (book_Reserve == null)
            {
                return NotFound();
            }

            return Ok(book_Reserve);
        }

        // PUT: api/Book_Reserve/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBook_Reserve(Book_Reserve book_Reserve)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != book_Reserve.id)
            //{
            //    return BadRequest();
            //}

            db.Entry(book_Reserve).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!Book_ReserveExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Book_Reserve
        [ResponseType(typeof(Book_Reserve))]
        public async Task<IHttpActionResult> PostBook_Reserve(BookReserveModels bookReserve)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brm = new Book_Reserve
            {
                book_id = bookReserve.BookId,
                user_id = bookReserve.UserId,
                reserve_on = bookReserve.ReserveOn
            };
            db.Book_Reserve.Add(brm);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bookReserve.Id }, bookReserve);
        }

        // DELETE: api/Book_Reserve/5
        [ResponseType(typeof(Book_Reserve))]
        public async Task<IHttpActionResult> DeleteBook_Reserve(int id)
        {
            Book_Reserve book_Reserve = await db.Book_Reserve.FindAsync(id);
            if (book_Reserve == null)
            {
                return NotFound();
            }

            db.Book_Reserve.Remove(book_Reserve);
            await db.SaveChangesAsync();

            return Ok(book_Reserve);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Book_ReserveExists(int id)
        {
            return db.Book_Reserve.Count(e => e.id == id) > 0;
        }
    }
}