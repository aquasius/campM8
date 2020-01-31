using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CampMa8.Models
{
    public class Campsite
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Campground")]
        public int CampgroundId { get; set; }
        public Campground Campground  { get; set; }
    }
}