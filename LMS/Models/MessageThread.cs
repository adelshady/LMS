using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class MessageThread
    {
        [Key]
        [Required]
        public int IDMessageInitial { get; set; }
        [ForeignKey("IDMessageInitial")]
        public virtual Message MessageInitial { get; set; }

        [Key]
        [Required]
        public int IDMessageReply { get; set; }
        [ForeignKey("IDMessageReply")]
        public virtual Message MessageReply { get; set; }
        [Required]
        public  DateTime DateInserted { get; set; } 

    }
}
