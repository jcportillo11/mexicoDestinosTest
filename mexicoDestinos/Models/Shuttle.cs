using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mexicoDestinos.Models
{
    public class Shuttle
    {
        [Key]
        public int Id { get; set; }
        [StringLength(1)]
        public string ShuttleCode { get; set; }
        [ForeignKey("Zones")]
        public string ZoneCode { get; set; }
        public Zone Zone { get; set; }
        [ForeignKey("ShuttleTexts")]
        public int ShuttleTextId { get; set; }
        public ShuttleText ShuttleText { get; set; }
        public decimal NetRate { get; set; }
        public decimal Tax { get; set; }
        public decimal MarkUp { get; set; }
    }

    public class ShuttleText
    {
        [Key]
        public int Id { get; set; }
        [StringLength(3)]
        public string Language { get; set; }
        [StringLength(150)]
        public string ShuttleName { get; set; }
        [StringLength(300)]
        public string ShuttleContent { get; set; }
    }

    public class ShuttleRequest {
        public string HotelName { get; set; }
        public string ZoneCode { get; set; }
        public string Language { get; set; }
    }

    public class ShuttleResults
    {
        public int Id { get; set; }
        public string AirportCode { get; set; }
        public string AirportName { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }
        public string ShuttleCode { get; set; }
        public string ShuttleName { get; set; }
        public string ShuttleContent { get; set; }
        public string GrossRate { get; set; }
        public string TotalRate { get; set; }

    }
}
