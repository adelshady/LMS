using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models.ViewModel
{
    public class TeacherAndHoDsVm
    {
        public int TeacherId { get; set; }
        public int HoDsId { get; set; }
        public string TeacherName { get; set; }
        public string HoDsName { get; set; }
        public List<Teacher> GetTeacherList { get; set; }
        public List<HoDs> GetHoDsList { get; set; }
    }
}
