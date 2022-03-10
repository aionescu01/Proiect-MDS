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
    public class StaffMemberController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StaffMemberController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaffMember(StaffMemberPostModel model)
        {
            var staffmember = new StaffMember()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = model.Role,
                Birth_Date = model.Birth_Date,
                Email = model.Email,
                Phone_Number = model.Phone_Number,
            };

            await _context.Staff.AddRangeAsync(staffmember);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetStaffMembers()
        {
            var staffmember = await _context.Staff.Select(StaffMemberGetModel.Projection).ToListAsync();

            return Ok(staffmember);
        }
        [HttpGet("get-by-name/{name}")]
        public async Task<IActionResult> GetStaffMemberById(string name)
        {
            var staffmember = await _context.Staff.Select(StaffMemberGetModel.Projection).FirstOrDefaultAsync(staffmember => staffmember.LastName == name);
            return Ok(staffmember);
        }

        [HttpDelete("delete-by-id/{id}")]
        public async Task<IActionResult> DeleteStaffMember(int id)
        {
            var staffmember = await _context.Staff.FirstOrDefaultAsync(staffmember => staffmember.Id == id);

            _context.Staff.Remove(staffmember);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
