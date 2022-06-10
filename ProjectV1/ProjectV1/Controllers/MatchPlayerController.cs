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

namespace ProjectV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchPlayerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MatchPlayerController(AppDbContext context)
        {
            _context = context;
        }



        [HttpPost("add-matchplayer")]
        public async Task<IActionResult> CreateMatchPlayerRelation(MatchPlayerPostModel model)
        {
            var mp = new MatchPlayer()
            {
                MatchId = model.MatchId,
                PlayerId = model.PlayerId,
            };

            await _context.MatchPlayers.AddRangeAsync(mp);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("get-all-matchplayers")]
        public async Task<IActionResult> GetMatchPlayerRelation()
        {
            var mp = await _context.MatchPlayers.Select(MatchPlayerGetModel.Projection).ToListAsync();

            return Ok(mp);
        }

        [HttpGet("get-by-MatchId")]
        public async Task<IActionResult> GetMatchPlayerByMatchId(int id)
        {
            var matchplayer = await _context.MatchPlayers.Select(MatchPlayerGetModel.Projection).FirstOrDefaultAsync(matchplayer => matchplayer.MatchId == id);
            return Ok(matchplayer);
        }

        [HttpGet("get-by-PlayerId")]
        public async Task<IActionResult> GetMatchPlayerByPlayerId(int id)
        {
            var matchplayer = await _context.MatchPlayers.Select(MatchPlayerGetModel.Projection).FirstOrDefaultAsync(matchplayer => matchplayer.PlayerId == id);
            return Ok(matchplayer);
        }

        [HttpDelete("delete-all-matchplayers")]
        public async Task<IActionResult> DeleteMatchPlayerRelation()
        {
            var mp = await _context.MatchPlayers.ToListAsync();
            foreach (var i in mp)
                _context.MatchPlayers.Remove(i);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete-by-PlayerId/{id}")]
        public async Task<IActionResult> DeleteMatchPlayerByPlayerId(int id)
        {
            var md = await _context.MatchPlayers.FirstOrDefaultAsync(md => md.PlayerId == id);

            _context.MatchPlayers.Remove(md);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete-by-MatchId/{id}")]
        public async Task<IActionResult> DeleteMatchPlayerByMatchId(int id)
        {
            var md = await _context.MatchPlayers.FirstOrDefaultAsync(md => md.MatchId == id);

            _context.MatchPlayers.Remove(md);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}

