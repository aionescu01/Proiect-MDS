using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class SalaryStatsGetModel
    {   
        public int id { get; set; }
        public int sum { get; set; }
        public int nrofplayers { get; set; }
        public double avgsalary { get; set; }


        public static Expression<Func<Entities.SalaryStats, SalaryStatsGetModel>> Projection => salaryStats => new SalaryStatsGetModel()
        {
            id = salaryStats.id,
            sum = salaryStats.sum,
            nrofplayers = salaryStats.nrofplayers,
            avgsalary = salaryStats.avgsalary,
        };
    }
}


