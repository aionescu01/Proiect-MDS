using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class StaffMemberPostModel
    {
        public string Name { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string Role { get; set; }
        public DateTime Birth_Date { get; set; }
        public string Email { get; set; }
        public string Phone_Number { get; set; }
    }
}
