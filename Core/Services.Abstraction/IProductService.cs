﻿using Shared;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IProductService
    {
        // Get All Products
        //Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(int? brandId, int? typeId, string? sort, int pageIndex = 1, int pageSize = 1);
        Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters specParames);

        // Get Product By Id
        Task<ProductResultDto?> GetProductByIdAsync(int id);

        // Get All Brands
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();


        // Get All Types
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();


    }
}
