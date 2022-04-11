using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class MatchPostModel
    {
        public string Opponent { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string Competition { get; set; }
        public DateTime Event_date { get; set; }
        public string Score { get; set; }
        public string Referee { get; set; }
    }
}
