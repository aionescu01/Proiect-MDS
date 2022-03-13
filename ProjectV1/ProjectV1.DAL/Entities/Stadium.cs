using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Entities
{
    public class Stadium
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Surface { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}