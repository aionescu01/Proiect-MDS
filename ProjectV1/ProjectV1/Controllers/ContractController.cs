using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectV1.DAL;
using ProjectV1.DAL.Models;
using ProjectV1.DAL.Entities;

namespace ProjectV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController: ControllerBase
    {
            private readonly AppDbContext _context;

            public ContractController(AppDbContext context)
            {
                _context = context;
            }

            [HttpPost]
            public async Task<IActionResult> CreateContract(ContractPostModel model)
            {

            var verif = await _context.Contracts.Select(ContractGetModel.Projection).FirstOrDefaultAsync(verif => verif.Id == model.PlayerId);
            var verif2 = await _context.Contracts.Select(ContractGetModel.Projection).FirstOrDefaultAsync(verif => verif.Id == model.StaffMemberId);
            var player = await _context.Players.Select(PlayerGetModel.Projection).FirstOrDefaultAsync(player => player.Id == model.PlayerId);
            var staffmember = await _context.Staff.Select(StaffMemberGetModel.Projection).FirstOrDefaultAsync(staffmember => staffmember.Id == model.StaffMemberId);


            if (verif != null)
            {
                if (verif.PlayerId != 0)
                    return BadRequest("The player already has a contract");
            }

            if (verif2 != null)
            {
                if (verif2.StaffMemberId != 0)
                    return BadRequest("The staff member already has a contract");
            }
            if (player == null && staffmember == null)
            {

                if (model.PlayerId != 0)
                    return BadRequest("The player does not exist");
                if (model.StaffMemberId != 0)
                    return BadRequest("The staff member does not exist");
            }

            Contract contract;
            if (model.PlayerId != 0)
                contract = new Contract()
                {
                    Start_date = model.Start_date,
                    End_date = model.End_date,
                    Salary = model.Salary,
                    Agent = model.Agent,
                    PlayerId = model.PlayerId,
                };
            else
            {
                contract = new Contract()
                {
                    Start_date = model.Start_date,
                    End_date = model.End_date,
                    Salary = model.Salary,
                    Agent = model.Agent,
                    StaffMemberId = model.StaffMemberId
                };
            }
                await _context.Contracts.AddRangeAsync(contract);
                await _context.SaveChangesAsync();

                return Ok();
            }

            [HttpGet]
            public async Task<IActionResult> GetContracts()
            {
                var contracts = await _context.Contracts.Select(ContractGetModel.Projection).ToListAsync();

                return Ok(contracts);
            }
            [HttpGet("get-by-name/{name}")]
            public async Task<IActionResult> GetContractById(string name)
            {
                var contract = await _context.Contracts.Select(ContractGetModel.Projection).FirstOrDefaultAsync(contract => contract.LastName == name);
                return Ok(contract);
            }

            [HttpDelete("delete-by-id/{id}")]
            public async Task<IActionResult> DeleteContract(int id)
            {
                var contract = await _context.Contracts.FirstOrDefaultAsync(contract => contract.Id == id);

                _context.Contracts.Remove(contract);
                await _context.SaveChangesAsync();

                return Ok();
            }
        }
    
}
