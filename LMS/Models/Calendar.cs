using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Calendar
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string day { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinalDate { get; set; }
        public string description { get; set; }
        //[Required]
        //[Display(Name ="Tasks")]
        //public int TasksId { get; set; }
        //[ForeignKey("TasksId")]
        //public virtual Tasks Tasks { get; set; }
        [Required]
        public string CategoryIds { get; set; }
        [Required]
        [Display(Name="Events")]
        public int EventID { get; set; }
        [ForeignKey("EventID")]
        public virtual Events Events { get; set; }
        [Required]
        [Display(Name ="Level ")]
        public int LevelId { get; set; }
        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }

    }
}
