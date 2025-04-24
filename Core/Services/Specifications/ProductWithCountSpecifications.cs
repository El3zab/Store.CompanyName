using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithCountSpecifications : BaseSpecifications<Product,int>
    {
        public ProductWithCountSpecifications(ProductSpecificationParameters specParames) 
            : base(
                  P =>
                    (string.IsNullOrEmpty(specParames.Search) || P.Name.ToLower().Contains(specParames.Search.ToLower()))&&
                    (!specParames.BrandId.HasValue || P.ProductBrand.Id == specParames.BrandId) &&
                    (!specParames.TypeId.HasValue || P.ProductType.Id == specParames.TypeId)
                  )
        {
            
        }
    }
}
