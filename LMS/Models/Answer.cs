using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Answer
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Answer1 { get; set; }

        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }

    }
}
