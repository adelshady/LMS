using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ParentName { get; set; }
        public string image { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public double phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDay { get; set; }
        [Required]
        public double NationalId { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        [Display(Name = "Level")]
        public int LevelId { get; set; }
        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }
    }
}
