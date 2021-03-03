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
using Microsoft.AspNet.Identity;

namespace LMS_WebAPI.Controllers
{
    [Authorize]
    public class Book_LendController : ApiController
    {
        private LMSEntities db = new LMSEntities();

        // GET: api/Book_Lend
        public Task<List<BookLendModels>> GetBooks()
        {
            //string userId = User.Identity.GetUserId();
            var booklend = (from bl in db.Book_Lend
                            where /*bl.user_id == userId &&*/
                            bl.returned == 0

                            select new BookLendModels
                            {
                                Id = bl.id,
                                BookId = bl.book_id,
                                UserId = bl.user_id,
                                StartDate = bl.start_date,
                                EndDate = bl.end_date,
                                ExtendedDate = bl.extended_date,
                                Returned = bl.returned
                            }).ToList();
            return Task.Run(() => booklend);
        }


        //public List<BookModels> GetBooks()
        //{
        //    string userId = User.Identity.GetUserId();

        //    //return db.Books.ToList();
        //    var booklend = (from b in db.Book_Lend
        //                    where b.user_id == userId

        //                    select new BookModels
        //                    {
        //                        Id = b.id,
        //                        Title = b.Book.title,
        //                        Image = b.Book.image,
        //                        Description = b.Book.description,
        //                        Qty = b.Book.qty

        //                    }).ToList();
        //    return booklend;
        //}

        // GET: api/Book_Lend/5
        [ResponseType(typeof(Book_Lend))]
        public async Task<IHttpActionResult> GetBook_Lend(int id)
        {
            Book_Lend book_Lend = await db.Book_Lend.FindAsync(id);
            if (book_Lend == null)
            {
                return NotFound();
            }

            return Ok(book_Lend);
        }

        // PUT: api/Book_Lend/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBook_Lend(BookLendModels booklend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Book_Lend book_lend =
                        (from bl in db.Book_Lend where bl.id == booklend.Id select bl).FirstOrDefault();

                if (booklend.ActionType == "Return")
                {

                    book_lend.returned = 1;

                    Book bookQty = 
                        (from b in db.Books where b.id == booklend.BookId select b).FirstOrDefault();
                    bookQty.qty = bookQty.qty + 1;
                }
                else if(booklend.ActionType == "Extend")
                {
                    book_lend.extended_date = book_lend.end_date.AddDays(7).Date;
                }
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Book_Lend
        [ResponseType(typeof(Book_Lend))]
        public async Task<IHttpActionResult> PostBook_Lend(BookLendModels booklend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.Identity.GetUserId();

            var blm = new Book_Lend
            {
                book_id = booklend.BookId,
                user_id = userId,
                start_date = DateTime.Today.Date,
                end_date = DateTime.Now.AddDays(7).Date
            };
            db.Book_Lend.Add(blm);
            

            Book bookQty = (from b in db.Books where b.id == booklend.BookId select b).FirstOrDefault();
            if (bookQty.qty > 0)
            {
                bookQty.qty = bookQty.qty - 1;
            }
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = booklend.Id }, booklend);
        }

        // DELETE: api/Book_Lend/5
        [ResponseType(typeof(Book_Lend))]
        public async Task<IHttpActionResult> DeleteBook_Lend(int id)
        {
            Book_Lend book_Lend = await db.Book_Lend.FindAsync(id);
            if (book_Lend == null)
            {
                return NotFound();
            }

            db.Book_Lend.Remove(book_Lend);
            await db.SaveChangesAsync();

            return Ok(book_Lend);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Book_LendExists(int id)
        {
            return db.Book_Lend.Count(e => e.id == id) > 0;
        }
    }
}