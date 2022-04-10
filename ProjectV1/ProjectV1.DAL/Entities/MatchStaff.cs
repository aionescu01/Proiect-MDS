using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Entities
{
    public class MatchStaff
    {
        public int? MatchId { get; set; }
        public virtual Match Match { get; set; }
        public int? StaffMemberId { get; set; }
        public virtual StaffMember StaffMember { get; set; }
    }
}
