using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Models
{
    public class ContractGetModel
    {
        public string LastName;
        public int Id { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public int Salary { get; set; }
        //poate sa punem si tabela cu bonusuri
        public string Agent { get; set; }
        public string Name { get; set; }
        public int PlayerId { get; set; }
        public int StaffMemberId { get; set; }

        public static Expression<Func<Entities.Contract, ContractGetModel>> Projection => contract => new ContractGetModel()
        {
            Id = contract.Id,
            Start_date = contract.Start_date,
            End_date = contract.End_date,
            Name = Expression.Lambda<Func<string>>(Expression.Condition(Expression.Constant(contract.Player.Id != null),
                Expression.Constant($"{contract.Player.FirstName} {contract.Player.LastName}"),
                Expression.Constant($"{contract.StaffMember.FirstName} {contract.StaffMember.LastName}"))).Compile()(),
            Salary = contract.Salary,
            Agent = contract.Agent,
            PlayerId = contract.PlayerId.GetValueOrDefault(0),
            StaffMemberId = contract.StaffMemberId.GetValueOrDefault(0)
            
        };

    }
}
