using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Announcement
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime ShowUntil { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public String body { get; set; }
        [Display(Name ="File")]
        public String Image { get; set; }
        [Required]
        [Display(Name ="Level")]
        public int LevelId { get; set; }
        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }
        [Required]
        public string Roles { get; set; }
    }
}
