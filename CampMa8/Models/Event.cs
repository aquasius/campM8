using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CampMa8.Models
{
    public class Event
    {

        [Key]
        public int EventId { get; set; }
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Display(Name = "Campground Address")]
        public string CampgroundAddress { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }
        public string DateOfEvent { get; set; }
        [Display(Name = "Time")]
        public string TimeOfEvent { get; set; }
        [Display(Name = "Maximum Campers")]
        public int MaximumCampers { get; set; }
        [Display(Name = "Current Campers")]
        public int CurrentCampers { get; set; }
        [Display(Name = "Event Is Full")]
        public bool IsFull { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public string apiKeyString = "https://maps.googleapis.com/maps/api/js?key=" + GoogleMapsKey.Key + "&callback=initializeMap";
        [ForeignKey("Camper")]
        public int CamperId { get; set; }
        public Camper Camper { get; set; }
    }
}