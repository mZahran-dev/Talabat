using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext)
        {
            // seed data for productBrand
            var brandsData = File.ReadAllText("../Talabat.Repository/Data Seeding/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            if (brands.Count() > 0) 
            {
                if (!(dbContext.ProductBrands.Count() > 0))
                {
                    foreach (var brand in brands)
                    {
                        dbContext.Set<ProductBrand>().Add(brand);

                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            //seed data for ProductCategory
            var categoryData = File.ReadAllText("../Talabat.Repository/Data Seeding/categories.json");
            var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoryData);
            if (categories.Count() > 0)
            {
                if (!(dbContext.ProductCategories.Count() > 0))
                {
                    foreach (var category in categories)
                    {
                        dbContext.Set<ProductCategory>().Add(category);

                    }
                    await dbContext.SaveChangesAsync();
                }
            }


            //seed data for Products
            var productsData = File.ReadAllText("../Talabat.Repository/Data Seeding/Products.json");
            var Products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (Products.Count() > 0)
            {
                if (!(dbContext.Products.Count() > 0))
                {
                    foreach (var products in Products)
                    {
                        dbContext.Set<Product>().Add(products);

                    }
                    await dbContext.SaveChangesAsync();
                }
            }


            // seed data for DeliveryMethods
            var deliveryData = File.ReadAllText("../Talabat.Repository/Data Seeding/delivery.json");
            var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
            if(deliveryMethods?.Count() > 0)
            {
                if(!(dbContext.DeliveryMethods.Count() > 0))
                {
                    foreach(var deliveryMethod in deliveryMethods)
                    {
                        dbContext.Set<DeliveryMethod>().Add(deliveryMethod);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }






    }
}
