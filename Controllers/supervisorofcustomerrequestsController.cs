using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using researchOnlineWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace researchOnlineWebsite.Controllers
{
    public class supervisorofcustomerrequestsController : Controller
    {
        private readonly researchWebsiteContext _context;

        public supervisorofcustomerrequestsController(researchWebsiteContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> allUsers()
        {
            var allUsers = await _context.Users.FromSqlRaw("SELECT * FROM users").ToListAsync();
            return View(allUsers);

        }
    }
}
