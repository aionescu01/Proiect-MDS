using ProjectV1.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Entities
{
    public class SalaryStats
    {
        public int sum { get; set; }
        public int nrofplayers { get; set; }
        public double avgsalary { get; set; }

    }
}
