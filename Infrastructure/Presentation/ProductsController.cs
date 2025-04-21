using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    // Api Controller
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServicesManager servicesManager) : ControllerBase
    {
        // endpoint: public non-static method

        // sort : nameAsc [default] - nameDesc - priceDesc - priceAsc
        [HttpGet] // Get: /api/products
        public async Task<IActionResult> GetAllProducts(int? brandId, int? typeId, string? sort, int pageIndex = 1, int pageSize = 1)
        {
            var result = await servicesManager.ProductService.GetAllProductsAsync(brandId, typeId, sort, pageIndex, pageSize);
            if (result is null) return BadRequest(); // 400
            return Ok(result); // 200
        }

        [HttpGet("{id}")] // Get: /api/products/1
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await servicesManager.ProductService.GetProductByIdAsync(id);
            if (result is null) return NotFound(); // 404
            return Ok(result); // 200
        }

        // Get All Brands
        [HttpGet("brands")] // Get: /api/products/brands
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await servicesManager.ProductService.GetAllBrandsAsync();
            if (result is null) return BadRequest(); // 400
            return Ok(result); // 200
        }

        // Get All Types
        [HttpGet("types")] // Get: /api/products/types
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await servicesManager.ProductService.GetAllTypesAsync();
            if (result is null) return BadRequest(); // 400
            return Ok(result); // 200
        }
    }
}
