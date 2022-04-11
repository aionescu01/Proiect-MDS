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
        public string LastName;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public DateTime Birth_Date { get; set; }
        public string Height { get; set; }
        public string Foot { get; set; }
        public string Position { get; set; }
        public decimal Value { get; set; }

        public static Expression<Func<Entities.Player, PlayerGetModel>> Projection => player => new PlayerGetModel()
        {
            Id = player.Id,
            Name = player.Name,
            Nationality = player.Nationality,
            Birth_Date = player.Birth_Date,
            Height = $"{player.Height} m",
            Foot = player.Foot,
            Position = player.Position,
            Value = player.Value
        };
    }
}
