using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models.ViewModel
{
    public class CalendarAndTasks
    {
        //public string PreprationRequired { get; set; }
        public Calendar calendar { get; set; }
        public List<CheckBoxItem> checkBoxItems{ get; set; }

        public string[] CategoryIds { get; set; }

        public DateTime Day{ get; set; }

    }
}
