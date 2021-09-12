using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Quiz
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }
    }
}
