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
using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Web;
using System.IO;
using System.Drawing;
using System.Web.Hosting;

namespace ProjectDone.API.Controllers
{
      public class JobsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        [Route("api/Jobs/All")]
        public List<JobResponse> GetAllJobs()
        {
            Mapper.CreateMap<Job, JobResponse>();
            return Mapper.Map<List<JobResponse>>(db.Jobs);
        }

        // GET: api/Jobs
        public List<JobResponse> GetJobs()
        {
            var userId = User.Identity.GetUserId();
            Mapper.CreateMap<Job, JobResponse>();
            return Mapper.Map<List<JobResponse>>(db.Jobs.Where(x=>x.ApplicationUserId==userId));
        }

        // GET: api/Jobs/5
        [ResponseType(typeof(Job))]
        public IHttpActionResult GetJob(int id)
        {
            Job job = db.Jobs.Find(id);
             if (job == null)
            {
                return NotFound();
            }
            if (User.Identity.GetUserId() != job.ApplicationUserId)
            {
                return BadRequest("This is not your job");
            }

            Mapper.CreateMap<Job, JobResponse>();
            return Ok(Mapper.Map<JobResponse>(job));
        }

        // PUT: api/Jobs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJob(int id, JobRequest jobRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var job = db.Jobs.Find(id);

            if (User.Identity.GetUserId() != job.ApplicationUserId)
            {
                return BadRequest("This is not your job");
            }
            Mapper.CreateMap<JobRequest, Job>();
            job = Mapper.Map<JobRequest,Job>(jobRequest,job);

            db.Entry(job).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

        [AllowAnonymous]
        [Route("api/Jobs/Image")]
        public HttpResponseMessage UploadImage()
        {

            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var fullFilename = "";
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    fullFilename = DateTime.Now.ToFileTimeUtc().ToString() + "_" + postedFile.FileName;
                    var filePath = HttpContext.Current.Server.MapPath("~/JobImages/" + fullFilename);
                    postedFile.SaveAs(filePath);

                  
                }
                result = Request.CreateResponse(HttpStatusCode.Created, fullFilename);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

    

    // POST: api/Jobs
    [ResponseType(typeof(Job))]
        public IHttpActionResult PostJob(JobRequest jobRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.CreateMap<JobRequest, Job>();
            var job = Mapper.Map<Job>(jobRequest);
            // Set creation date
            job.CreationDate = DateTime.Now;
            job.ApplicationUserId = User.Identity.GetUserId();
            db.Jobs.Add(job);
            db.SaveChanges();

            Mapper.CreateMap<Job, JobResponse>();
            return CreatedAtRoute("DefaultApi", new { id = job.JobId }, Mapper.Map<JobResponse>(job));
        }

        // DELETE: api/Jobs/5
        [ResponseType(typeof(Job))]
        public IHttpActionResult DeleteJob(int id)
        {
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }
            if (User.Identity.GetUserId() != job.ApplicationUserId)
            {
                return BadRequest("This is not your job");
            }

            db.Jobs.Remove(job);
            db.SaveChanges();

            Mapper.CreateMap<Job, JobResponse>();
            return Ok(Mapper.Map<JobResponse>(job));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobExists(int id)
        {
            return db.Jobs.Count(e => e.JobId == id) > 0;
        }
    }
}