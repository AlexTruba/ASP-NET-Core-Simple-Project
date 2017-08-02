using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimple.Model
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
    }
}
