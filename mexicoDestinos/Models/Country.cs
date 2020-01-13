using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mexicoDestinos.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string CountryName { get; set; }
    }
}
