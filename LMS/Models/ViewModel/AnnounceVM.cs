using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models.ViewModel
{
    public class AnnounceVM
    {
        public Announcement Announcement{ get; set; }
        public List<CheckBoxItem> checkBoxItems { get; set; }

        public string[] CategoryIds { get; set; }

    }
}
