using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CampMa8.Models
{
    public class CampEvent
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Camper")]
        public int CamperId { get; set; }
        public Camper Camper { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}