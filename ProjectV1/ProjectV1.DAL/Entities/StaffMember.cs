using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Entities
{
    public class StaffMember
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //[Required]
        //public string FirstName { get; set; }
        //[Required]
       // public string LastName { get; set; }
        [Required]
        public string Role { get; set; }
        //poate si nationalitate
        //am dat remove la required pt email phone number si birth date
        public DateTime Birth_Date { get; set; }
        public string Email { get; set; }
        public string Phone_Number { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual ICollection<MatchStaff> MatchStaffs { get; set; }
    }
}
