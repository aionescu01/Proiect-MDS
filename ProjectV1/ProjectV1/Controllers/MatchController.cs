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
    public class MatchController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MatchController(AppDbContext context)
        {
            _context = context;
        }



        [HttpPost("add-match")]
        [Authorize(Roles = "Owner, Manager")]

        public async Task<IActionResult> CreateMatch(MatchPostModel model)
        {
            var match = new Match()
            {
                StadiumId = model.StadiumId,
                Opponent = model.Opponent,
                Competition = model.Competition,
                Event_date = model.Event_date,
                Score = model.Score,
                Referee = model.Referee,
            };

            await _context.Matches.AddRangeAsync(match);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("get-all-matches")]
        public async Task<IActionResult> GetMatches()
        {
            var match = await _context.Matches.Select(MatchGetModel.Projection).ToListAsync();

            return Ok(match);
        }

        [HttpGet("get-by-opponent/{opponent}")]
        public async Task<IActionResult> GetMatchesByOpponent(string opponent)
        {
            var match = await _context.Matches.Select(MatchGetModel.Projection).FirstOrDefaultAsync(match => match.Opponent == opponent);
            return Ok(match);
        }


        [HttpPut("put-by-id/{id}")]
        [Authorize(Roles = "Owner, Manager")]

        public async Task<IActionResult> EditPlayer(int id, MatchPostModel model)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(match => match.Id == id);

            if (match == null)
            {
                return BadRequest($"The match with id {id} does not exist");
            }

            if (model.StadiumId != 0)
                match.StadiumId = model.StadiumId;
            if (model.Opponent != "")
                match.Opponent = model.Opponent;
            if (model.Event_date != DateTime.MinValue)
                match.Event_date = model.Event_date;
            if (model.Competition != "")
                match.Competition = model.Competition;
            if (model.Score != "")
                match.Score = model.Score;
            if (model.Referee != "")
                match.Referee = model.Referee;


            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete-all-matches")]
        [Authorize(Roles = "Owner, Manager")]

        public async Task<IActionResult> DeleteMatches()
        {
            var matches = await _context.Matches.ToListAsync();
            foreach (var i in matches)
                _context.Matches.Remove(i);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete-by-id/{id}")]
        [Authorize(Roles = "Owner, Manager")]

        public async Task<IActionResult> DeleteMatches(int id)
        {
            var match= await _context.Matches.FirstOrDefaultAsync(match => match.Id == id);

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
