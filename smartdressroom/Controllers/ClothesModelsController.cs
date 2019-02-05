using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using smartdressroom.Models;
using smartdressroom.Services;

namespace smartdressroom.Controllers
{
    public class ClothesModelsController : Controller
    {
        private readonly IStorageService dbStorageService;

        public ClothesModelsController(IStorageService ss) => dbStorageService = ss;

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            var user = dbStorageService.AppContext.Admins.Where(a => a.Login == login && a.Password == password).FirstOrDefault();
            if (user != null)
            {
                HttpContext.Session.SetString(nameof(user), JsonConvert.SerializeObject(user));
                return RedirectToAction("Index");
            }
            else return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Login");
        }

        // GET: ClothesModels
        public async Task<IActionResult> Index() => View(await dbStorageService.AppContext.ClothesModels.ToListAsync());

        // GET: ClothesModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothesModel = await dbStorageService.AppContext.ClothesModels
                .FirstOrDefaultAsync(m => m.ID == id);
            if (clothesModel == null)
            {
                return NotFound();
            }

            return View(clothesModel);
        }

        // GET: ClothesModels/Create
        public IActionResult Create() => View();

        // POST: ClothesModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Code,Price,Size,Brand,ImgPath")] ClothesModel clothesModel)
        {
            if (ModelState.IsValid)
            {
                clothesModel.ID = Guid.NewGuid();
                dbStorageService.AppContext.Add(clothesModel);
                await dbStorageService.AppContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

            var clothesModel = await dbStorageService.AppContext.ClothesModels.FindAsync(id);
            if (clothesModel == null)
            {
                return NotFound();
            }
            return View(clothesModel);
        }

        // POST: ClothesModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Code,Price,Size,Brand,ImgPath")] ClothesModel clothesModel)
        {
            if (id != clothesModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbStorageService.AppContext.Update(clothesModel);
                    await dbStorageService.AppContext.SaveChangesAsync();
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
            return View(clothesModel);
        }

        // GET: ClothesModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothesModel = await dbStorageService.AppContext.ClothesModels
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
            var clothesModel = await dbStorageService.AppContext.ClothesModels.FindAsync(id);
            dbStorageService.AppContext.ClothesModels.Remove(clothesModel);
            await dbStorageService.AppContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothesModelExists(Guid id) => dbStorageService.AppContext.ClothesModels.Any(e => e.ID == id);
    }
}
