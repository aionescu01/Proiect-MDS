using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Entities
{
    public class Match
    {   
        public int Id { get; set; }
        public int? StadiumId { get; set; }
        public string Opponent { get; set; }
        public string Competition { get; set; }
        public DateTime Event_date { get; set; } 
        public string Score { get; set; }
        public string Referee { get; set; }

        public virtual Stadium Stadium { get; set; }

        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; }
        public virtual ICollection<MatchStaff> MatchStaffs { get; set; }

    }
}
