

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miraii.Models;

namespace Miraii.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }

        [Route("/admin")]
        public IActionResult Index(int pg=1)
        {
            //var contents = _context.Contents.ToList();
            List<Content> contents = _context.Contents.ToList();

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int recsCount = contents.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = contents.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;


            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Content content)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _context.Contents.AddAsync(content);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                }
            }


            ModelState.AddModelError(string.Empty, "Something went wrong");
            return View(content);
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var content = await _context.Contents.FirstOrDefaultAsync(x => x.Id == id);
            return View(content);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Content content)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var exist = await _context.Contents.FirstOrDefaultAsync(x => x.Id == content.Id);

                    if(exist != null)
                    {
                        exist.Title = content.Title;
                        exist.Description = content.Description;
                        exist.Author = content.Author;
                        exist.Image = content.Image;
                        exist.Category = content.Category;

                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid content to update");
                    return View(content);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Invalid content to update {ex.Message}");
                }
            }


            ModelState.AddModelError(string.Empty, "Something went wrong");
            return View();

        }

        /*[HttpGet]
        [Route("/edit/{id}")]
        public ViewResult Edit(int id)
        {
            return View();
        }

        [HttpPut]
        public IActionResult Edit(int id, Content content)
        {

            _context.Entry(content).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        */

        public IActionResult Delete(int id)
        {
            Content user = _context.Contents.Find(id);
            _context.Contents.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
