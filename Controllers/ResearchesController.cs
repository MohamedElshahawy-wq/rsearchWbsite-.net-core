using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using researchOnlineWebsite.Models;

namespace researchOnlineWebsite.Controllers
{
    public class ResearchesController : Controller
    {
        private readonly researchWebsiteContext _context;

        public ResearchesController(researchWebsiteContext context)
        {
            _context = context;
        }

        // GET: Researches
        public async Task<IActionResult> Index()
        {
            var researchWebsiteContext = _context.Researches.Include(r => r.Stat).Include(r => r.User);
            return View(await researchWebsiteContext.ToListAsync());
        }

        // GET: Researches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var research = await _context.Researches
                //.Include(r => r.Stat)
                //.Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ResearchId == id);
            if (research == null)
            {
                return NotFound();
            }

            return View(research);
        }

        // GET: Researches/Create
        public IActionResult Create()
        {
            ViewData["StatId"] = new SelectList(_context.Statuses, "StatusId", "StatusId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");


            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumberOfPage,Language,ResearchId,Specialize,Title,UserId")] Research research)
        {
            SqlConnection conn1 = new SqlConnection("Server=.;Initial Catalog=researchWebsite;Integrated Security=True");

            string sql;
            sql = "SELECT * FROM users";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                string telephone = Convert.ToString((int)reader["telephone"]);
                Console.WriteLine("tele", telephone);

                int id = (int)reader["userId"];
                reader.Close();
                conn1.Close();
                if (id!=null)
                {

                    research.StatId = 1;
                    research.ModifiedDate = DateTime.Now;
                    research.RedirectTo = "admin";
                    research.Date = DateTime.Today;
                    research.Price = research.NumberOfPage * 2;

                    if (ModelState.IsValid)
                    {
                        _context.Add(research);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    //ViewData["StatId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", research.StatId);
                    //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", research.UserId);
                    return View(research);
                }
                else
                {
                    ViewData["Message1"] = "WRONG email or Password :) Try Again";
                    //@Html.ActionLink("Register", "Create", "ClientsRegister");

                    return RedirectToAction("Create", "ClientsRegister");
                }
            }
            else
            {
                ViewData["Message"] = "Register First Please";
                return View();
            }
        }

        // GET: Researches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var research = await _context.Researches.FindAsync(id);
            if (research == null)
            {
                return NotFound();
            }
            ViewData["StatId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", research.StatId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", research.UserId);
            return View(research);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumberOfPage,Language,ResearchId,Date,Specialize,Title,StatId,UserId,ModifiedDate,RedirectTo,Price")] Research research)
        {
            research.Price = research.NumberOfPage * 2;
            if (id != research.ResearchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(research);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearchExists(research.ResearchId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["StatId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", research.StatId);
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", research.UserId);
            return View(research);
        }

        // GET: Researches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var research = await _context.Researches
                .Include(r => r.Stat)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ResearchId == id);
            if (research == null)
            {
                return NotFound();
            }

            return View(research);
        }

        // POST: Researches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var research = await _context.Researches.FindAsync(id);
            _context.Researches.Remove(research);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Allpdf()
        {
            var pdfAllClients = await _context.ResearchContents.FromSqlRaw("SELECT * FROM researchContent where redirectTo='user'").ToListAsync();
            return View(pdfAllClients);

        }

        private bool ResearchExists(int id)
        {
            return _context.Researches.Any(e => e.ResearchId == id);
        }
    }
}
