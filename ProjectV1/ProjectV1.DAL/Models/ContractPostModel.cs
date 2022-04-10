using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class ContractPostModel
    {
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public int Salary { get; set; }
        //poate sa punem si tabela cu bonusuri
        public string Agent { get; set; }

        public int PlayerId { get; set; }
        public int StaffMemberId { get; set; }

    }
}
