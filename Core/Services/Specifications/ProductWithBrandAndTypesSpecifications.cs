using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypesSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypesSpecifications(int id) : base(P => P.Id == id)
        {
            ApplyIncludes();
        }

        public ProductWithBrandAndTypesSpecifications(ProductSpecificationParameters specParames) 
            : base(
                    P => 
                    (string.IsNullOrEmpty(specParames.Search) || P.Name.ToLower().Contains(specParames.Search.ToLower()))&&
                    (!specParames.BrandId.HasValue || P.ProductBrand.Id == specParames.BrandId) &&
                    (!specParames.TypeId.HasValue || P.ProductType.Id == specParames.TypeId)
                  )
        {
            ApplyIncludes();
            ApplySorting(specParames.Sort);
            ApplyPagination(specParames.PageIndex, specParames.PageSize);
        }

        private void ApplyIncludes()
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
        private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "namedesc":
                        AddOrderByDescending(P => P.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }
        }
    }
}
