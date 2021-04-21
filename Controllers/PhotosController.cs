using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LetsTryMVC.Data;
using LetsTryMVC.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace LetsTryMVC.Controllers
{
    [Authorize]
    public class PhotosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PhotosController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Photos
        public async Task<IActionResult> Index()
        {
            var findUserPhotos = _context.Photos
                .Where(p => p.UserName == User.Identity.Name)
                .Include(p => p.Category);
   
            return View(await findUserPhotos.ToListAsync());
        }

        // GET: Photos/Create
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ImageFile,CategoryId,UserName")] Photo photo)
        {
            ProductCategory category = _context.Categories.Find(photo.CategoryId);
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(photo.ImageFile.FileName);
                string extension = Path.GetExtension(photo.ImageFile.FileName);
                photo.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await photo.ImageFile.CopyToAsync(fileStream);
                }
                photo.Category = category;
                //Insert record
                _context.Add(photo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(photo);

        }


        // GET: Photos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageModel = await _context.Photos.FindAsync(id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", imageModel.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            //delete the record
            _context.Photos.Remove(imageModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult TheirPhotos(string userName)
        {
            var findUserPhotos = _context.Photos
             .Where(p => p.UserName == userName)
             .Include(p => p.Category)
             .ToList();
            ViewBag.User = userName;
            return View(findUserPhotos);
        }

        [Route("/AllPhotos/category")]
        public IActionResult AllPhotos(string category)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ProductCategory All = new ProductCategory
            {
                Name = "All"
            };

            ViewBag.Categories.Add(All);
            List<Photo> picsWithCategory = new List<Photo>();

            if(category == "All")
            {
                picsWithCategory = _context.Photos.ToList();
            } else
            {
                picsWithCategory = _context.Photos.Where(p => p.Category.Name == category)
                                .ToList();
            }
            
            ViewBag.category = category;
            
            return View(picsWithCategory);
        }

        private bool PhotoExists(int id)
        {
            return _context.Photos.Any(e => e.Id == id);
        }
    }
}
