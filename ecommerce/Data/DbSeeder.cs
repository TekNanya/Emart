using ecommerce.Constants;
using ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            // 1. Using GetRequiredService to ensure the app stops with a clear error if services are missing
            var userMgr = service.GetRequiredService<UserManager<IdentityUser>>();
            var roleMgr = service.GetRequiredService<RoleManager<IdentityRole>>();
            var context = service.GetRequiredService<ApplicationDbContext>();

            // 2. Roles & Admin Account
    
            if (!await roleMgr.RoleExistsAsync(Roles.Admin.ToString()))
                await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

            if (!await roleMgr.RoleExistsAsync(Roles.User.ToString()))
                await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));

            var adminEmail = "admin@gmail.com";
            var adminUser = await userMgr.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var admin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userMgr.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                {
                    await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
                }
            }

            // 3. Categories 
            if (context.Categories != null && !await context.Categories.AnyAsync())
            {
                context.Categories.AddRange(
                    new Category { Name = "Специи", ImageUrl = "/images/cat-spices.jpg", Description = "Ароматные африканские травы" },
                    new Category { Name = "Соусы", ImageUrl = "/images/cat-sauces.jpg", Description = "Острые и пряные добавки" },
                    new Category { Name = "Зерновые", ImageUrl = "/images/cat-grains.jpg", Description = "Мука, рис и крупы" },
                    new Category { Name = "Напитки", ImageUrl = "/images/cat-drinks.jpg", Description = "Традиционный чай и кофе" }
                );
                await context.SaveChangesAsync();
            }

            // 4. Products
            if (context.Products != null && !await context.Products.AnyAsync())
            {
                var spiceCat = await context.Categories!.FirstOrDefaultAsync(c => c.Name == "Специи");

                if (spiceCat != null)
                {
                    context.Products.AddRange(
                        new Product { Name = "Бербере", Price = 450, CategoryId = spiceCat.Id, ImageUrl = "/images/p1.jpg", IsPopular = true, CreatedDate = DateTime.Now, Description = "Эфиопская смесь специй", Weight = "100г" },
                        new Product { Name = "Харисса", Price = 550, CategoryId = spiceCat.Id, ImageUrl = "/images/p2.jpg", IsPopular = true, CreatedDate = DateTime.Now.AddDays(-10), Description = "Острая паста из туниса", Weight = "200г" },
                        new Product { Name = "Рас-эль-ханут", Price = 600, CategoryId = spiceCat.Id, ImageUrl = "/images/p3.jpg", IsPopular = false, CreatedDate = DateTime.Now, Description = "Марокканская классика", Weight = "150г" },
                        new Product { Name = "Дукка", Price = 380, CategoryId = spiceCat.Id, ImageUrl = "/images/p4.jpg", IsPopular = false, CreatedDate = DateTime.Now, Description = "Египетская ореховая смесь", Weight = "100г" }
                    );
                    await context.SaveChangesAsync();
                }
            }

            // 5. Reviews
            if (context.Reviews != null && !await context.Reviews.AnyAsync())
            {
                context.Reviews.AddRange(
                    new Review
                    {
                        CustomerName = "Алексей",
                        Comment = "Лучшие специи в Москве! Очень аутентично.",
                        Rating = 5,
                        UserId = "seed-data" 
                    },
                    new Review
                    {
                        CustomerName = "Мария",
                        Comment = "Доставка быстрая, соусы просто огонь!",
                        Rating = 5,
                        UserId = "seed-data"
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}