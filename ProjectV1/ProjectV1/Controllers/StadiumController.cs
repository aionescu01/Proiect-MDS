using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectV1.DAL;
using ProjectV1.DAL.Models;
using ProjectV1.DAL.Entities;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using Microsoft.AspNetCore.Authorization;

namespace ProjectV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StadiumController(AppDbContext context)
        {
            _context = context;
        }



        [HttpPost("add-stadium")]
        [Authorize(Roles = "Owner, Manager")]

        public async Task<IActionResult> CreateStadium(StadiumPostModel model)
        {
            var stadium = new Stadium()
            {

                Name = model.Name,
                Capacity = model.Capacity,
                Surface = model.Surface,
                Address = model.Address,
            };

            await _context.Stadiums.AddRangeAsync(stadium);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("get-stadiums")]
        public async Task<IActionResult> GetStadiums()
        {
            var stadium = await _context.Stadiums.Select(StadiumGetModel.Projection).ToListAsync();

            return Ok(stadium);
        }

        [HttpGet("get-by-name")]
        public async Task<IActionResult> GetStadiumsByName(string nume)
        {
            var stadium = await _context.Stadiums.Select(StadiumGetModel.Projection).FirstOrDefaultAsync(stadium => stadium.Name == nume);
            return Ok(stadium);
        }

        [HttpPut("put-by-id/{id}")]
        [Authorize(Roles = "Owner, Manager")]

        public async Task<IActionResult> EditPlayer(int id, StadiumPostModel model)
        {
            var stadium = await _context.Stadiums.FirstOrDefaultAsync(stadium => stadium.Id == id);

            if (stadium == null)
            {
                return BadRequest($"The player with id {id} does not exist");
            }

            if (model.Name != "")
                stadium.Name = model.Name;
            if (model.Capacity != 0)
                stadium.Capacity = model.Capacity;
            if (model.Surface != "")
                stadium.Surface = model.Surface;
            if (model.Address != "")
                stadium.Address = model.Address;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete-stadiums")]
        [Authorize(Roles = "Owner, Manager")]

        public async Task<IActionResult> DeleteStadiums()
        {
            var stadiums = await _context.Stadiums.ToListAsync();
            foreach (var i in stadiums)
                _context.Stadiums.Remove(i);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete-by-id/{id}")]
        [Authorize(Roles = "Owner, Manager")]

        public async Task<IActionResult> DeleteStadiums(int id)
        {
            var stadium = await _context.Stadiums.FirstOrDefaultAsync(stadium => stadium.Id == id);

            _context.Stadiums.Remove(stadium);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
