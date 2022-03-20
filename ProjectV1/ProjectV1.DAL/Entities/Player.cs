using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Entities
{
    public class Player
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //[Required]
        //public string FirstName { get; set; }
        //[Required]
        //public string LastName { get; set; }
        [Required]
        public string Nationality { get; set; }
        public DateTime Birth_Date { get; set; }
        public float Height { get; set; }
        public decimal Value { get; set; }
        [Required]
        public string Foot { get; set; }
        [Required]
        public string Position { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; }

    }
}
