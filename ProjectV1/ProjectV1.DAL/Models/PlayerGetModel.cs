using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class PlayerGetModel
    {
        public string Name { get; set; }
        public string Nationality { get; set; }
        public DateTime Birth_Date { get; set; }
        public float Height { get; set; }
        public string Foot { get; set; }
        public string Position { get; set; }


        public static Expression<Func<Entities.Player, PlayerGetModel>> Projection => player => new PlayerGetModel()
        {
            Name = $"{player.FirstName} {player.LastName}",
            Nationality = player.Nationality,
            Birth_Date = player.Birth_Date,
            Height = player.Height,
            Foot = player.Foot,
            Position = player.Position
        };
    }
}
