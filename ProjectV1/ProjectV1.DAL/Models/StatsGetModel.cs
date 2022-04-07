using ProjectV1.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class StatsGetModel
    {

        public int sum { get; set; }
        public int nrofplayers { get; set; }
        public double avgsalary{ get; set; }

        public static Expression<Func<SalaryStats, StatsGetModel>> Projection => contract => new StatsGetModel()
        {
            avgsalary = contract.avgsalary,
            sum = contract.sum,
            nrofplayers = contract.nrofplayers
        };
    }
}
