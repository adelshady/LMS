﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Question
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string BestAnswer { get; set; }
        [Required]
        public int Degree { get; set; }
        public string Image { get; set; }
        [Required]
        [Display(Name ="Question Type")]
        public int QuestionTypeId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual QuestionType QuestionType { get; set; }
       
    }
}
