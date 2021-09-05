using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Chapter
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Lesson")]
        public int LessonId { get; set; }
        [ForeignKey("LessonId")]
        public virtual Lesson Lesson{ get; set; }


    }
}
