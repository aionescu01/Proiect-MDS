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
                FirstName = model.FirstName,
                LastName = model.LastName,
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

        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await _context.Players.Select(PlayerGetModel.Projection).ToListAsync();

            return Ok(players);
        }
        [HttpGet("get-by-name/{name}")]
        public async Task<IActionResult> GetDriverById(string name)
        {
            //System.Console.WriteLine()
            var player = await _context.Players.Select(PlayerGetModel.Projection).FirstOrDefaultAsync(player => player.LastName == name);
            //var player = await _context.Players.FirstOrDefaultAsync(player => player.LastName == name);
            return Ok(player);
        }

        [HttpDelete("delete-by-id/{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(player => player.Id == id);

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
