using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _services;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService services, ICategoryService categoryService, IMapper mapper)
        {
            _services = services;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _services.GetProductWithCategory());
        }

        public async Task<IActionResult> Save()
        {
            var categories = _categoryService.GetAllAsync();

            var categoriesDTO = _mapper.Map<List<CategoryDto>>(categories);

            ViewBag.cagetories = new SelectList(categoriesDTO, "Id", "Name"); // DropdownList olarak SelectList alacak . categoriesDTO liste olarak verdik. Bu categoryDTO'dan ID'yi göstereceğiz . Kullanıcılar da Name'ini görecek.

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {

            {
                await _services.AddAsync(_mapper.Map<Product>(productDto)); // ProductDto'yu Product'a dönüştür. 

                return RedirectToAction(nameof(Index)); // "Index" olarak da yazılabilir fakat tip güvenli şekilde yazdık.

                return View(); // Başarısız ise View'e geri dönecek . 
            }
        }


    }
}
