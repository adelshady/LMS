using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Content
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string ContentName { get; set; }
        [Required]
        public int ChapterId { get; set; }
        [ForeignKey("ChapterId")]
        public virtual Chapter Chapter { get; set; }
        public DateTime PublishDate { get; set; }

        public string Body { get; set; }
        public byte[] VideoData  { get; set; }
    }
}
