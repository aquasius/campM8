using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CampMa8.Models
{
    public class Campground
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "State")]
        public string contractID { get; set; }
        public string contractType { get; set; }
        [Display(Name = "Amenities")]
        public string amenities { get; set; }
        public string facilityID { get; set; }
        [Display(Name = "Campground Name")]
        public string facilityName { get; set; }
        public string faciltyPhoto { get; set; }
        [Display(Name = "Site Type")]
        public string siteType { get; set; }
        [Display(Name = "Pets Allowed?")]
        public string sitesWithPetsAllowed { get; set; }
        [Display(Name = "Campgrounds with Water Access")]
        public string sitesWithWaterfront { get; set; }
        [Display(Name = "Electricity")]
        public string hookups { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

       // public string apiKeyString = "https://maps.googleapis.com/maps/api/js?key=" + GoogleMapsKey.Key + "&callback=initializeMap";
    }
}