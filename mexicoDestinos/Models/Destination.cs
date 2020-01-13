using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mexicoDestinos.Models
{
    public class Destination
    {
        [Key]
        public int Id { get; set; }
        [StringLength(80)]
        public string DestinationName { get; set; }
        [ForeignKey("Countries")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
