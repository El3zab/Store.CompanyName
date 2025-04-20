using Domain.Models;
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

        public ProductWithBrandAndTypesSpecifications() : base(null)
        {
            ApplyIncludes();
        }

        private void ApplyIncludes()
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
