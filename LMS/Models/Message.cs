using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Message
    {
        [Key]
        public int IDMessage { get; set; }
        [Required]
        public string Subject { get; set; }
        public string Body { get; set; }
        [Required]
        [Display(Name = "From User")]
        public int FromUserId { get; set; }
        [ForeignKey("FromUserId")]
        public virtual User FromUser { get; set; }
        [Required]
        [Display(Name = "To User")]
        public int ToUserId { get; set; }
        [ForeignKey("ToUserId")]
        public virtual User ToUser { get; set; }
        [Required]
        public DateTime DateInserted { get; set; } 
        public byte MessageRead { get; set; }

    }
}
