using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Register
    {
        public int ID { get; set; }

        public string FullName{ get; set; }

        public string image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDay { get; set; }
        public double NationalId { get; set; }

    }
}
