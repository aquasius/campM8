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
        public string CampgroundName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public string apiKeyString = "https://maps.googleapis.com/maps/api/js?key=" + GoogleMapsKey.Key + "&callback=initializeMap";
    }
}