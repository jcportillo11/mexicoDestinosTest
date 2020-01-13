using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mexicoDestinos.Models
{
    public class Zone
    {
        [Key]
        [StringLength(10)]
        public string ZoneCode { get; set; }
        [StringLength(150)]
        public string ZoneName { get; set; }
        [ForeignKey("Airports")]
        public int AirportId { get; set; }
        public Airport Airport { get; set; }
    }
}
