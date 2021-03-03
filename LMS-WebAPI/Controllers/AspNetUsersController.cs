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
    public class AspNetUsersController : ApiController
    {
        private LMSEntities db = new LMSEntities();

        // GET: api/AspNetUsers
        public Task<User> GetAspNetUsers()
        {
            string userId = User.Identity.GetUserId();

            var user = (from u in db.AspNetUsers
                        where u.Id == userId
                        select new User{
                            Id = u.Id,
                            Username = u.UserName
                        }).FirstOrDefault();
            return Task.Run(() => user);
        }

        // GET: api/AspNetUsers/5
        [ResponseType(typeof(AspNetUser))]
        public async Task<IHttpActionResult> GetAspNetUser(string id)
        {
            AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            return Ok(aspNetUser);
        }

        // PUT: api/AspNetUsers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAspNetUser(string id, AspNetUser aspNetUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aspNetUser.Id)
            {
                return BadRequest();
            }

            db.Entry(aspNetUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserExists(id))
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

        // POST: api/AspNetUsers
        [ResponseType(typeof(AspNetUser))]
        public async Task<IHttpActionResult> PostAspNetUser(AspNetUser aspNetUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AspNetUsers.Add(aspNetUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AspNetUserExists(aspNetUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = aspNetUser.Id }, aspNetUser);
        }

        // DELETE: api/AspNetUsers/5
        [ResponseType(typeof(AspNetUser))]
        public async Task<IHttpActionResult> DeleteAspNetUser(string id)
        {
            AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            db.AspNetUsers.Remove(aspNetUser);
            await db.SaveChangesAsync();

            return Ok(aspNetUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AspNetUserExists(string id)
        {
            return db.AspNetUsers.Count(e => e.Id == id) > 0;
        }
    }
}