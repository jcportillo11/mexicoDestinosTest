using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mexicoDestinos.Models
{
    public class Airport
    {
        [Key]
        public int Id { get; set; }
        [StringLength(4)]
        public string IATA { get; set; }
        [StringLength(150)]
        public string AirportName { get; set; }

    }
}
