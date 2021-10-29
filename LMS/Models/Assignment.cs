using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Assignment
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string AssignmentName { get; set; }
        public DateTime PublishDate { get; set; }
        [Required]
        public int ChapterId { get; set; }
        [ForeignKey("ChapterId ")]
        public virtual Chapter Chapter { get; set; }
        public int MaxGrade { get; set; }
        public string Content { get; set; }
        public string File { get; set; }

    }
}
