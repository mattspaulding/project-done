using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectDone.API.Models
{
    public class Bid
    {
        public int BidId { get; set; }
        [Required]
        public int Amount { get; set; }
        public string Message { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public string BidState { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
    public class BidRequest
    {
        [Required]
        public int Amount { get; set; }
        public string Message { get; set; }
    }
    public class BidResponse
    {
        public int BidId { get; set; }
        public string Amount { get; set; }
        public string Message { get; set; }
    }
}