using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using researchOnlineWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace researchOnlineWebsite.Controllers
{
    public class GeographyAdminController : Controller
    {
        private readonly researchWebsiteContext _context;

        public GeographyAdminController(researchWebsiteContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> geographyAdminResearch()
        {
            var geoResearch = await _context.Researches.FromSqlRaw("SELECT * FROM research where specialize='Geography' and  redirectTo='adminGeography' ").ToListAsync();
            return View(geoResearch);
        }
        public async Task<IActionResult> Approve(int ResearchId)
        {
            await _context.Database.ExecuteSqlRawAsync("UPDATE research SET statId = 3,   modifiedDate= GETDATE() where researchId =" + ResearchId);
            return Ok();
        }
        public async Task<IActionResult> Cancel(int ResearchId)
        {
            await _context.Database.ExecuteSqlRawAsync("UPDATE research SET statId = 4, redirectTo = 'admin',   modifiedDate= GETDATE() where researchId =" + ResearchId);
            return Ok();

        }
        public async Task<IActionResult> Done(int ResearchId)
        {
            await _context.Database.ExecuteSqlRawAsync("UPDATE research SET statId = 5, redirectTo = 'admin',   modifiedDate= GETDATE() where researchId =" + ResearchId);
            return Ok();

        }
        public async Task<IActionResult> reassignResearches()
        {
            var geoResearch = await _context.Researches.FromSqlRaw("SELECT * FROM research where specialize='Geography' and  redirectTo='adminGeography' ").ToListAsync();
            return View(geoResearch);

        }
        public async Task<IActionResult> Allpdf()
        {
            var pdfAll = await _context.ResearchContents.FromSqlRaw("SELECT * FROM researchContent ").ToListAsync();
            return View(pdfAll);

        }



    }
}
