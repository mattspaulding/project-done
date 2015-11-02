using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace ProjectDone.API.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string Image { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
    public class JobRequest
    {
        public HttpPostedFileBase ImageFile { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }
    public class JobResponse
    {
        public int JobId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
