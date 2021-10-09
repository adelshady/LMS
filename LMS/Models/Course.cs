using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Course
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string CourseName{ get; set; }
        [Required]
        public string CourseCode { get; set; }
        [Required]
        [Display(Name ="Level")]
        public int LevelId { get; set; }
        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }

        [Required]
        [Display(Name = "Teacher")]
        public int TeacherId { get; set; }
        [ForeignKey("UserId")]
        public virtual Teacher Teacher { get; set; }
        [Required]
        [Display(Name = "HoDs")]
        public int HoDsId { get; set; }
        [ForeignKey("UserId")]
        public virtual HoDs HoDs { get; set; }

    }
}
