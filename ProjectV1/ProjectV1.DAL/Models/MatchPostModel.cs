using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class MatchPostModel
    {
        public int StadiumId { get; set; }
        public string Opponent { get; set; }

        public string Competition { get; set; }
        public DateTime Event_date { get; set; }
        public string Score { get; set; }
        public string Referee { get; set; }
    }
}
