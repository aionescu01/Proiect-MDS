using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class MatchStaffGetModel
    {
        public int? MatchId { get; set; }
        public int? StaffMemberId { get; set; }


        public static Expression<Func<Entities.MatchStaff, MatchStaffGetModel>> Projection => matchstaff => new MatchStaffGetModel()
        {
            MatchId = matchstaff.MatchId,
            StaffMemberId = matchstaff.StaffMemberId,
        };
    }
}



