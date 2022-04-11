using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class MatchGetModel
    {
        public string opponent;
        public int Id { get; set; }
        public int? StadiumId { get; set; }
        public string Opponent { get; set; }
        public string Competition { get; set; }
        public DateTime Event_date { get; set; }
        public string Score { get; set; }
        public string Referee { get; set; }

        public static Expression<Func<Entities.Match, MatchGetModel>> Projection => match => new MatchGetModel()
        {
            Id = match.Id,
            StadiumId = match.StadiumId.GetValueOrDefault(0),
            Opponent = match.Opponent,
            Competition = match.Competition,
            Event_date = match.Event_date,
            Score = match.Score,
            Referee = match.Referee,
        };
    }
}
