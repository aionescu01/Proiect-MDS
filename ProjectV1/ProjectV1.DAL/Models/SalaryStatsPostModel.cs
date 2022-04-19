using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class SalaryStatsPostModel
    {   
        public int id { get; set; }
        public int sum { get; set; }
        public int nrofplayers { get; set; }
        public double avgsalary { get; set; }
    }
}
