using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using researchOnlineWebsite.Models;

namespace researchOnlineWebsite.Controllers
{
    public class ResearchContentsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly researchWebsiteContext _context;

        public ResearchContentsController(researchWebsiteContext context)
        {
            _context = context;
        }

        // GET: ResearchContents
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResearchContents.ToListAsync());
        }

        // GET: ResearchContents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchContent = await _context.ResearchContents
                .FirstOrDefaultAsync(m => m.ResearchId == id);
            if (researchContent == null)
            {
                return NotFound();
            }

            return View(researchContent);
        }

        // GET: ResearchContents/Create
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile fileX ,[Bind("ResearchId")] ResearchContent researchContent)
        {
            //ResearchPdf,ResearchPdfUrl
            researchContent.Specialize = "Geography";
            researchContent.StatId = 5;


            if (fileX == null || fileX.Length == 0)
                return Content("file not selected");
            else
            {

                // Get the file name from the browser
                string filename = fileX.FileName;
                // Get file path to be uploaded
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\book"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create, (FileAccess)FileMode.Open, (FileShare)FileAccess.Read))
                { await fileX.CopyToAsync(filestream); }

                researchContent.ResearchPdf = filename;
            }

           

           
            _context.Add(researchContent);
               await _context.SaveChangesAsync();
               return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchContent = await _context.ResearchContents.FindAsync(id);
            if (researchContent == null)
            {
                return NotFound();
            }
            return View(researchContent);
        }

        // POST: ResearchContents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResearchPdf,ResearchPdfUrl,ResearchId,Specialize,StatId")] ResearchContent researchContent)
        {
            if (id != researchContent.ResearchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(researchContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearchContentExists(researchContent.ResearchId))
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
            return View(researchContent);
        }

        // GET: ResearchContents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchContent = await _context.ResearchContents
                .FirstOrDefaultAsync(m => m.ResearchId == id);
            if (researchContent == null)
            {
                return NotFound();
            }

            return View(researchContent);
        }

        // POST: ResearchContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var researchContent = await _context.ResearchContents.FindAsync(id);
            _context.ResearchContents.Remove(researchContent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResearchContentExists(int id)
        {
            return _context.ResearchContents.Any(e => e.ResearchId == id);
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
    }

}
