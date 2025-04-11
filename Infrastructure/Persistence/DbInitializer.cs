using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer(StoreDbContext context) : IDbInitializer
    {

        public async Task InitializeAsync()
        {
            try
            {
                // Create Database If it Doesn't Exists && Apply To Any Pending Migrations
                if (context.Database.GetPendingMigrations().Any())
                {
                    await context.Database.MigrateAsync();
                }


                // Data Seeding

                // Seeding ProductTypes From Json Files
                if (!context.ProductTypes.Any())
                {
                    // 1. Read All Date From types Json File as String
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

                    // 2. Transform String To C# Objects [List<ProductTypes>]
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    // 3. Add List<ProductTypes> To Database
                    if (types is not null && types.Any())
                    {
                        await context.ProductTypes.AddRangeAsync(types);
                        await context.SaveChangesAsync();
                    }
                }


                // Seeding ProductBrands From Json Files
                if (!context.ProductBrands.Any())
                {
                    // 1. Read All Date From brands Json File as String
                    var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");

                    // 2. Transform String To C# Objects [List<ProductBrand>]
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    // 3. Add List<ProductBrand> To Database
                    if (brands is not null && brands.Any())
                    {
                        await context.ProductBrands.AddRangeAsync(brands);
                        await context.SaveChangesAsync();
                    }
                }

                // Seeding Products From Json Files
                if (!context.Products.Any())
                {
                    // 1. Read All Date From products Json File as String
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");

                    // 2. Transform String To C# Objects [List<Products>]
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    // 3. Add List<Products> To Database
                    if (products is not null && products.Any())
                    {
                        await context.Products.AddRangeAsync(products);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
