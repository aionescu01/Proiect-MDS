using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class StaffMemberGetModel
    {
        public string LastName;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime Birth_Date { get; set; }
        public string Email { get; set; }
        public string Phone_Number { get; set; }
    
    public static Expression<Func<Entities.StaffMember, StaffMemberGetModel>> Projection => staffmember => new StaffMemberGetModel()
    {
        Id = staffmember.Id,
        LastName = staffmember.LastName,
        Name = $"{staffmember.FirstName} {staffmember.LastName}",
        Role = staffmember.Role,
        Birth_Date = staffmember.Birth_Date,
        Email = staffmember.Email,
        Phone_Number = staffmember.Phone_Number
    };
    }
}
