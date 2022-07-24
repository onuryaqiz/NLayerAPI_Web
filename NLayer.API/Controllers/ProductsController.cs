using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{

    public class ProductsController : CustomBaseController // Business kod yani Service kodu buraya Best Practice için yazılmayacak. Olabildiğince temiz olmalı.
    {
        // Controller sadece servisleri bilir. Kesinlikle repository miras almayacak.
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;

        public ProductsController(IService<Product> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        // GET api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();

            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList()); // IEnumerable yani bir Entity döneceği için mapper'ı da ekledik.

            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }
        // Örnek : api/products/5 olan data gelir.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);

            var productsDtos = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDtos));


        }

    }
}
