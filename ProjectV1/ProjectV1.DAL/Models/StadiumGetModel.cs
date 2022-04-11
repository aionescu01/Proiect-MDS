using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class StadiumGetModel
    {
        public string nume;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Surface { get; set; }
        public string Address { get; set; }


        public static Expression<Func<Entities.Stadium, StadiumGetModel>> Projection => stadium => new StadiumGetModel()
        {
            Id = stadium.Id,
            Name =stadium.Name,
            Capacity = stadium.Capacity,
            Surface = stadium.Surface,
            Address = stadium.Address,
        };
    }
}


