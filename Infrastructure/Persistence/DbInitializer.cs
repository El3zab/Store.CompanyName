using Domain.Contracts;
using Domain.Models;
using Domain.Models.Identity;
using Domain.Models.OrderModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer(
        StoreDbContext context, 
        StoreIdentityDbContext identityDbContext,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager
        ) 
        : IDbInitializer
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
                // Seeding Products From Json Files
                if (!context.DeliveryMethods.Any())
                {
                    // 1. Read All Date From products Json File as String
                    var deliveryData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\delivery.json");

                    // 2. Transform String To C# Objects [List<Products>]
                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                    // 3. Add List<Products> To Database
                    if (deliveryMethods is not null && deliveryMethods.Any())
                    {
                        await context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task InitializeIdentityAsync()
        {
            // Create Database If it Doesn't Exists && Apply To Any Pending Migrations
            if (identityDbContext.Database.GetPendingMigrations().Any())
            {
                await identityDbContext.Database.MigrateAsync();
            }

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin"
                });
                
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "SuperAdmin"
                });
            }

            // Seeding
            if(!userManager.Users.Any())
            {
                var superAdminUser = new AppUser()
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "0123456789"
                };
                
                var adminUser = new AppUser()
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "0123456789"
                };

                await userManager.CreateAsync(superAdminUser, "P@ssW0rd");
                await userManager.CreateAsync(adminUser, "P@ssW0rd");


                await userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
