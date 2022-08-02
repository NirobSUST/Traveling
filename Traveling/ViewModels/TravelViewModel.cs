using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Traveling.Models
{
    public class TravelViewModel
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string Bus { get; set; }

        public int ApproxCost { get; set; }
        public string Experience { get; set; }
        public string Title { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
