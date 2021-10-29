using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Zone
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string  Name { get; set; }

        public string Description { get; set; }

        public string File { get; set; }
       
        public string Role { get; set; }
    }
}
