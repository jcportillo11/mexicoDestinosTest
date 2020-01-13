using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mexicoDestinos.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        [StringLength(150)]
        public string HotelName { get; set; }
        [ForeignKey("Destinations")]
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }
        [ForeignKey("Zones")]
        public string ZoneCode { get; set; }
        public Zone Zone { get; set; }
    }

    public class HotelBase {
        public string HotelName { get; set; }
        public string ZoneCode { get; set; }
    }
}
