using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}