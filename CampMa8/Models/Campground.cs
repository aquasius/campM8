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
        public string availabilityStatus { get; set; }
        public string facilityID { get; set; }
        public string facilityName { get; set; }
        public string faciltyPhoto { get; set; }
        public string siteType { get; set; }
        public string sitesWithPetsAllowed { get; set; }
        public string sitesWithWaterfront { get; set; }
        public string sitesWithAmps { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

       // public string apiKeyString = "https://maps.googleapis.com/maps/api/js?key=" + GoogleMapsKey.Key + "&callback=initializeMap";
    }
}