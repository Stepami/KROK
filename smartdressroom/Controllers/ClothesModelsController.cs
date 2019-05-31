using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smartdressroom.Models;
using smartdressroom.Storage;

namespace smartdressroom.Controllers
{
    public class ClothesModelsController : Controller
    {
        public ClothesModelsController() { }

        // GET: ClothesModels
        public async Task<IActionResult> Index()
        {
            List<ClothesModel> clothesModels = null;
            using (var context = new ApplicationContext())
            {
                clothesModels = await context.ClothesModels.ToListAsync();
            }
            if (clothesModels == null)
            {
                return NotFound();
            }
            return View(clothesModels);
        }

        // GET: ClothesModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClothesModel clothesModel = null;
            using (var context = new ApplicationContext())
            {
                clothesModel = await context.ClothesModels
                    .Include(c => c.Collection)
                    .FirstOrDefaultAsync(m => m.ID == id);
            }
            if (clothesModel == null)
            {
                return NotFound();
            }

            return View(clothesModel);
        }

        // GET: ClothesModels/Create
        public IActionResult Create()
        {
            using (var context = new ApplicationContext())
            {
                ViewData["CollectionID"] = new SelectList(context.CollectionModels, "ID", "ID");
            }
            return View();
        }

        // POST: ClothesModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Code,Price,Size,Brand,ImgFormat,ImgPath,CollectionID")] ClothesModel clothesModel)
        {
            using (var context = new ApplicationContext())
            {
                if (ModelState.IsValid)
                {
                    clothesModel.ID = Guid.NewGuid();
                    clothesModel.ImgPath =
                        $"~/images/clothes/{clothesModel.Brand}/{clothesModel.Code}.{clothesModel.ImgFormat}";
                    context.Add(clothesModel);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CollectionID"] = new SelectList(context.CollectionModels, "ID", "ID", clothesModel.CollectionID);
            }
            return View(clothesModel);
        }

        // GET: ClothesModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClothesModel clothesModel = null;
            using (var context = new ApplicationContext())
            {
                clothesModel = await context.ClothesModels.FindAsync(id);
                if (clothesModel == null)
                {
                    return NotFound();
                }
                var collections = context.CollectionModels.ToList();

                ViewData["CollectionID"] = new SelectList(collections, "ID", "ID", clothesModel.CollectionID);
            }
            return View(clothesModel);
        }

        // POST: ClothesModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Code,Price,Size,Brand,ImgFormat,ImgPath,CollectionID")] ClothesModel clothesModel)
        {
            if (id != clothesModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var context = new ApplicationContext())
                    {
                        context.Update(clothesModel);
                        await context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothesModelExists(clothesModel.ID))
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
            using (var context = new ApplicationContext())
            {
                ViewData["CollectionID"] = new SelectList(context.CollectionModels, "ID", "ID", clothesModel.CollectionID);
            }
            return View(clothesModel);
        }

        // GET: ClothesModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClothesModel clothesModel = null;
            using (var context = new ApplicationContext())
            {
                clothesModel = await context.ClothesModels
                    .Include(c => c.Collection)
                    .FirstOrDefaultAsync(m => m.ID == id);
            }
            if (clothesModel == null)
            {
                return NotFound();
            }

            return View(clothesModel);
        }

        // POST: ClothesModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            using (var context = new ApplicationContext())
            {
                var clothesModel = await context.ClothesModels.FindAsync(id);
                context.ClothesModels.Remove(clothesModel);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ClothesModelExists(Guid id)
        {
            bool exists = false;
            using (var context = new ApplicationContext())
            {
                exists = context.ClothesModels.Any(e => e.ID == id);
            }
            return exists;
        }
    }
}
