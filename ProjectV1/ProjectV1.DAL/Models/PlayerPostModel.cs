using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class PlayerPostModel
    {
        //public string Link { get; set; }
        public string Name { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string Nationality { get; set; }
        public DateTime Birth_Date { get; set; }
        public float Height { get; set; }
        public string Foot { get; set; }
        public string Position { get; set; }
        public decimal Value { get; set; }
    }
}
