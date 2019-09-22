using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace smartdressroom.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Storage.ApplicationContext _context;

        public ProductsController(Storage.ApplicationContext _context) => this._context = _context;

        // GET api/products/R240580
        [Authorize]
        [HttpGet("api/[controller]/{vcode}")]
        public async Task<object> Get(string vcode)
        {
            var result = await _context.ClothesModels
                .Include(cm => cm.Collection.ClothesModels)
                .FirstOrDefaultAsync(cm => cm.VendorCode == vcode);

            if (result == null)
                return null;

            var collection = result.Collection.ClothesModels
                .Where(cm => cm.ID != result.ID)
                .Select(cm => 
                new
                {
                    cm.VendorCode,
                    cm.Price,
                    cm.Sizes,
                    cm.Brand,
                    cm.ImagesCount,
                    cm.ImgPath
                }).ToList();

            return new
            {
                result.VendorCode,
                result.Price,
                result.Sizes,
                result.Brand,
                collectionName = result.Collection.Name,
                result.ImagesCount,
                result.ImgPath,
                collection
            };
        }
    }
}