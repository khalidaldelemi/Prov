using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DaignosProv.Models
{
    public class Product
    {
        
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range (typeof(decimal),"5","1000")]
        public decimal Priice { get; set; }

    }
}
