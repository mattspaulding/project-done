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
using ProjectDone.API.Models;
using Microsoft.AspNet.Identity;
using AutoMapper;

namespace WebApplication7.Controllers
{
    public class BidsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Bids
        public IQueryable<Bid> GetBids()
        {
            return db.Bids;
        }

        // GET: api/Bids/Me
        public ICollection<Bid> GetMyBids()
        {
            var thisUser = db.Users.Find(User.Identity.GetUserId());
            return thisUser.Bids;
        }

        // GET: api/Bids/5
        [ResponseType(typeof(Bid))]
        public IHttpActionResult GetBid(int id)
        {
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return NotFound();
            }

            return Ok(bid);
        }

        // PUT: api/Bids/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBid(int id, BidRequest bidRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bid = db.Bids.Find(id);

            if (User.Identity.GetUserId() != bid.ApplicationUserId)
            {
                return BadRequest("This is not your bid");
            }
            Mapper.CreateMap<BidRequest, Bid>();
            bid = Mapper.Map<BidRequest, Bid>(bidRequest, bid);

            db.Entry(bid).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidExists(id))
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

        // POST: api/Bids
        [ResponseType(typeof(Bid))]
        public IHttpActionResult PostBid(BidRequest bidRequest)
        {
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.CreateMap<BidRequest, Project>();
            var bid = Mapper.Map<Bid>(bidRequest);
            // Set creation date
            bid.CreationDate = DateTime.Now;
            bid.ApplicationUserId = User.Identity.GetUserId();
            db.Bids.Add(bid);
            db.SaveChanges();

            Mapper.CreateMap<Bid, BidResponse>();
            return CreatedAtRoute("DefaultApi", new { id = bid.BidId }, Mapper.Map<BidResponse>(bid));
        }

        // DELETE: api/Bids/5
        [ResponseType(typeof(Bid))]
        public IHttpActionResult DeleteBid(int id)
        {
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return NotFound();
            }

            db.Bids.Remove(bid);
            db.SaveChanges();

            return Ok(bid);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BidExists(int id)
        {
            return db.Bids.Count(e => e.BidId == id) > 0;
        }
    }
}