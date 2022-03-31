using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using researchOnlineWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace researchOnlineWebsite.Controllers
{
    public class AdminController : Controller
    {
        private readonly researchWebsiteContext _context;

        public AdminController(researchWebsiteContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> geographyResearch()
        {
            var orItems = await _context.Researches.FromSqlRaw("SELECT * FROM research where specialize='Geography' and  redirectTo='admin'").ToListAsync();
            return View(orItems);
        }
        public async Task<IActionResult> redirectToGeographyAdmin(int ResearchId)
        {
              await _context.Database.ExecuteSqlRawAsync("UPDATE research SET statId = 2, redirectTo = 'adminGeography',   modifiedDate= GETDATE() where researchId =" + ResearchId);
              return Ok();
        }
        public async Task<IActionResult> GeographyApprove()
        {
             var approve = await _context.Researches.FromSqlRaw("SELECT * FROM research where  statId = 3 and specialize='Geography' ").ToListAsync();
            return View(approve);
       }
        public async Task<IActionResult> GeographyCancel()
        {
            var cancel = await _context.Researches.FromSqlRaw("SELECT * FROM research where  statId = 4 and specialize='Geography' ").ToListAsync();
            return View(cancel);
        }
        public async Task<IActionResult> Allpdf()
        {
            var pdfAll = await _context.ResearchContents.FromSqlRaw("SELECT * FROM researchContent ").ToListAsync();
            return View(pdfAll);

        }
        public async Task<IActionResult> redirectToStudent(int ResearchId)
        {
            await _context.Database.ExecuteSqlRawAsync("UPDATE research SET statId = 5, redirectTo = 'user' where researchId =" + ResearchId);
            return Ok();
        }
        //public async Task<IActionResult> Reassign(int ResearchId)
        //{
        //    await _context.Database.ExecuteSqlRawAsync("UPDATE research SET statId = 2, redirectTo = 'adminGeography',   modifiedDate= GETDATE() where researchId =" + ResearchId);
        //    return Ok();
        //}
        //and modifiedDate = GETDATE()


    }
}
