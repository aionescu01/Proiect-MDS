using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Enums
{
    public enum RolesEnum
    {
        General = 1, //nu poate modifica nimic
        Manager = 2, //poate modifica doar jucatori
        Owner = 3 //poate modifica si jucatori si staff
    }
}
