using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class MatchPlayerGetModel
    {
        public int? MatchId { get; set; }
        public int? PlayerId { get; set; }


        public static Expression<Func<Entities.MatchPlayer, MatchPlayerGetModel>> Projection => matchplayer => new MatchPlayerGetModel()
        {
            MatchId = matchplayer.MatchId,
            PlayerId = matchplayer.PlayerId,
        };
    }
}


