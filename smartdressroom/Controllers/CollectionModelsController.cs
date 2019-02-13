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
    public class CollectionModelsController : Controller
    {
        private readonly ApplicationContext _context;

        public CollectionModelsController(ApplicationContext context) => _context = context;

        // GET: CollectionModels
        public async Task<IActionResult> Index() => View(await _context.CollectionModels.ToListAsync());

        // GET: CollectionModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectionModel = await _context.CollectionModels
                .FirstOrDefaultAsync(m => m.ID == id);
            if (collectionModel == null)
            {
                return NotFound();
            }

            return View(collectionModel);
        }

        // GET: CollectionModels/Create
        public IActionResult Create() => View();

        // POST: CollectionModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] CollectionModel collectionModel)
        {
            if (ModelState.IsValid)
            {
                collectionModel.ID = Guid.NewGuid();
                _context.Add(collectionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collectionModel);
        }

        // GET: CollectionModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectionModel = await _context.CollectionModels.FindAsync(id);
            if (collectionModel == null)
            {
                return NotFound();
            }
            return View(collectionModel);
        }

        // POST: CollectionModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name")] CollectionModel collectionModel)
        {
            if (id != collectionModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collectionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionModelExists(collectionModel.ID))
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
            return View(collectionModel);
        }

        // GET: CollectionModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectionModel = await _context.CollectionModels
                .FirstOrDefaultAsync(m => m.ID == id);
            if (collectionModel == null)
            {
                return NotFound();
            }

            return View(collectionModel);
        }

        // POST: CollectionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var collectionModel = await _context.CollectionModels.FindAsync(id);
            _context.CollectionModels.Remove(collectionModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionModelExists(Guid id) => _context.CollectionModels.Any(e => e.ID == id);
    }
}
