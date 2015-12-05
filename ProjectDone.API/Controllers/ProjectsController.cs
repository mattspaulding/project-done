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
      public class ProjectsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        [Route("api/Projects/All")]
        public List<ProjectResponse> GetAllProjects()
        {
            Mapper.CreateMap<Project, ProjectResponse>();
            return Mapper.Map<List<ProjectResponse>>(db.Projects);
        }

        // GET: api/Projects
        public List<ProjectResponse> GetProjects()
        {
            var userId = User.Identity.GetUserId();
            Mapper.CreateMap<Project, ProjectResponse>();
            return Mapper.Map<List<ProjectResponse>>(db.Projects.Where(x=>x.ApplicationUserId==userId));
        }

        // GET: api/Projects/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult GetProject(int id)
        {
            Project project = db.Projects.Find(id);
             if (project == null)
            {
                return NotFound();
            }
            if (User.Identity.GetUserId() != project.ApplicationUserId)
            {
                return BadRequest("This is not your project");
            }

            Mapper.CreateMap<Project, ProjectResponse>();
            return Ok(Mapper.Map<ProjectResponse>(project));
        }

        // PUT: api/Projects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProject(int id, ProjectRequest projectRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = db.Projects.Find(id);

            if (User.Identity.GetUserId() != project.ApplicationUserId)
            {
                return BadRequest("This is not your project");
            }
            Mapper.CreateMap<ProjectRequest, Project>();
            project = Mapper.Map<ProjectRequest,Project>(projectRequest,project);

            db.Entry(project).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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
        [Route("api/Projects/Image")]
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
                    var filePath = HttpContext.Current.Server.MapPath("~/ProjectImages/" + fullFilename);
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

    

    // POST: api/Projects
    [ResponseType(typeof(Project))]
        public IHttpActionResult PostProject(ProjectRequest projectRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.CreateMap<ProjectRequest, Project>();
            var project = Mapper.Map<Project>(projectRequest);
            // Set creation date
            project.CreationDate = DateTime.Now;
            project.ApplicationUserId = User.Identity.GetUserId();
            db.Projects.Add(project);
            db.SaveChanges();

            Mapper.CreateMap<Project, ProjectResponse>();
            return CreatedAtRoute("DefaultApi", new { id = project.ProjectId }, Mapper.Map<ProjectResponse>(project));
        }

        // DELETE: api/Projects/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult DeleteProject(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            if (User.Identity.GetUserId() != project.ApplicationUserId)
            {
                return BadRequest("This is not your project");
            }

            db.Projects.Remove(project);
            db.SaveChanges();

            Mapper.CreateMap<Project, ProjectResponse>();
            return Ok(Mapper.Map<ProjectResponse>(project));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.ProjectId == id) > 0;
        }
    }
}