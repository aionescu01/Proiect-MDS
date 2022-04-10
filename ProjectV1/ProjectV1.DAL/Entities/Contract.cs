using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Entities
{
    public class Contract
    {
        public int Id { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public int Salary { get; set; }
        //poate sa punem si tabela cu bonusuri
        public string Agent { get; set; }
        public int? PlayerId { get; set; }
        public int? StaffMemberId { get; set; }
        public virtual Player Player{ get; set; }
        public virtual StaffMember StaffMember{ get; set; }

    }
}
