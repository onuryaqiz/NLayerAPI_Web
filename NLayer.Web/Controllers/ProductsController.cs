using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers // Alınacak
{
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;

        private readonly CategoryApiService _categoryApiService;

        public ProductsController(CategoryApiService categoryApiService, ProductApiService productApiService)
        {
            _categoryApiService = categoryApiService;
            _productApiService = productApiService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _productApiService.GetProductsWithCategoryAsync());
        }

        public async Task<IActionResult> Save()
        {

            var categoriesDto = await _categoryApiService.GetAllAsync();


            ViewBag.cagetories = new SelectList(categoriesDto, "Id", "Name"); // DropdownList olarak SelectList alacak . categoriesDTO liste olarak verdik. Bu categoryDTO'dan ID'yi göstereceğiz . Kullanıcılar da Name'ini görecek.

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if (ModelState.IsValid)

            {
                await _productApiService.SaveAsync(productDto); // ProductDto'yu Product'a dönüştür. 

                return RedirectToAction(nameof(Index)); // "Index" olarak da yazılabilir fakat tip güvenli şekilde yazdık.
            }
            var categoriesDto = await _categoryApiService.GetAllAsync();



            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View(); // Başarısız ise View'e geri dönecek . 
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)

        {
            var product = await _productApiService.GetByIdAsync(id);

            var categoriesDto = await _categoryApiService.GetAllAsync();



            ViewBag.cagetories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);

            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)

            {
                await _productApiService.UpdateAsync(productDto);
                return RedirectToAction(nameof(Index));

            }
            var categoriesDto = await _categoryApiService.GetAllAsync();



            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);

            return View(productDto);

        }

        public async Task<IActionResult> Remove(int id)

        {
            await _productApiService.RemoveAsync(id);



            return RedirectToAction(nameof(Index));
        }
    }


}

