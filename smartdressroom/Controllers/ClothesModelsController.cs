using System;
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
        private readonly ApplicationContext _context;

        public ClothesModelsController(ApplicationContext context) => _context = context;

        // GET: ClothesModels
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.ClothesModels.Include(c => c.Collection);
            return View(await applicationContext.ToListAsync());
        }

        // GET: ClothesModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothesModel = await _context.ClothesModels
                .Include(c => c.Collection)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (clothesModel == null)
            {
                return NotFound();
            }

            return View(clothesModel);
        }

        // GET: ClothesModels/Create
        public IActionResult Create()
        {
            ViewData["CollectionID"] = new SelectList(_context.CollectionModels, "ID", "ID");
            return View();
        }

        // POST: ClothesModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Code,Price,Size,Brand,ImgFormat,ImgPath,CollectionID")] ClothesModel clothesModel)
        {
            if (ModelState.IsValid)
            {
                clothesModel.ID = Guid.NewGuid();
                clothesModel.ImgPath =
                    $"images/clothes/{clothesModel.Brand}/{clothesModel.Code}.{clothesModel.ImgFormat}";
                _context.Add(clothesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CollectionID"] = new SelectList(_context.CollectionModels, "ID", "ID", clothesModel.CollectionID);
            return View(clothesModel);
        }

        // GET: ClothesModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothesModel = await _context.ClothesModels.FindAsync(id);
            if (clothesModel == null)
            {
                return NotFound();
            }
            ViewData["CollectionID"] = new SelectList(_context.CollectionModels, "ID", "ID", clothesModel.CollectionID);
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
                    _context.Update(clothesModel);
                    await _context.SaveChangesAsync();
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
            ViewData["CollectionID"] = new SelectList(_context.CollectionModels, "ID", "ID", clothesModel.CollectionID);
            return View(clothesModel);
        }

        // GET: ClothesModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothesModel = await _context.ClothesModels
                .Include(c => c.Collection)
                .FirstOrDefaultAsync(m => m.ID == id);
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
            var clothesModel = await _context.ClothesModels.FindAsync(id);
            _context.ClothesModels.Remove(clothesModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothesModelExists(Guid id) => _context.ClothesModels.Any(e => e.ID == id);
    }
}
