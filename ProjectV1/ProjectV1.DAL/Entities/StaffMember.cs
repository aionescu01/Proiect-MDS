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
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public DateTime Birth_Date { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone_Number { get; set; }
    }
}
