using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CampMa8.Models
{
    public class CampExperience
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Camp Experience")]
        public double Experience { get; set; }
        public string facilityID { get; set; }
    }
}