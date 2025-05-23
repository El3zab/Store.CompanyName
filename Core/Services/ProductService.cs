﻿using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstraction;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters specParames)
        {
            var spec = new ProductWithBrandAndTypesSpecifications(specParames);

            // Get All Products Throught ProductRepository
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);

            var specCount = new ProductWithCountSpecifications(specParames);

            var count = await unitOfWork.GetRepository<Product, int>().CountAync(specCount);
            
            // Mapping IEnumerable<Product> To IEnumerable<ProductResultDto> : Automapper
            var result = mapper.Map<IEnumerable<ProductResultDto>>(products);
            return new PaginationResponse<ProductResultDto>(specParames.PageIndex,specParames.PageSize,count,result);
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndTypesSpecifications(id);

            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(spec);
            if (product is null) throw new ProductNotFoundException(id);

            var result = mapper.Map<ProductResultDto>(product);
            return result;
        }

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            var result = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return result;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            var result = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return result;
        }
    }
}
