using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class QuizDetails
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime receivedTime { get; set; }

        public int Attempts { get; set; }
        [Required]
        [Display(Name ="Chapter")]
        public int ChapterId { get; set; }
        [ForeignKey("ChapterId")]
        public virtual Chapter Chapter { get; set; }
        [Required]
        [Display(Name = "Question")]
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question{ get; set; }
        

    }
}
