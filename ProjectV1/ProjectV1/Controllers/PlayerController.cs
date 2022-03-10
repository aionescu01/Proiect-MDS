using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectV1.DAL;
using ProjectV1.DAL.Models;

namespace ProjectV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlayerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriver(PlayerPostModel model)
        {
            var player = new Player()
            {
                Name = model.Name,
                Surname = model.Surname,
                Nationality = model.Nationality,
                Birth_Date = model.Birth_Date,
                Height = model.Height,
                Foot = model.Foot,
                Position = model.Position,
            };

            await _context.Players.AddRangeAsync(player);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
