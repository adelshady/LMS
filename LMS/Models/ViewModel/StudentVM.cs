using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models.ViewModel
{
    public class StudentVM
    {
        public Zone Zone { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }

        public List<Student> Students { get; set; }
        public string[] StudentList { get; set; }
    }
}
