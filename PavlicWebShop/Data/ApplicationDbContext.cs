using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PavlicWebShop.Models.Dbo;
using PavlicWebShop.Models.Dbo.Base;
using PavlicWebShop.Models.ViewModel;
using PavlicWebShop.Models.Binding;

namespace PavlicWebShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public override int SaveChanges()
        {

            var entries = ChangeTracker
                        .Entries()
                        .Where(e => e.Entity is IEntityBase && (
                          e.State == EntityState.Added
                          || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        ((IEntityBase)entityEntry.Entity).Created = DateTime.Now;
                        break;
                    default:
                        break;
                }

            }
            return base.SaveChanges();

        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is IEntityBase && (
              e.State == EntityState.Added
              || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        ((IEntityBase)entityEntry.Entity).Created = DateTime.Now;
                        break;
                    default:
                        break;
                }

            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductCategory>().HasData(
                new ProductCategory
                {
                    Id = 1,
                    Title = "Vino",
                    Description = "Vino od grožđa"
                },
                new ProductCategory
                {
                    Id = 2,
                    Title = "Sok",
                    Description = "Domaći voćni sok"
                },
                new ProductCategory
                {
                    Id = 3,
                    Title = "Med",
                    Description = "Domaći med",
                });

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Graševina",
                    Description = "Kvalitetno suho vino - Berba 2021.",
                    ProductCategoryId = 1,
                    Quantity = 20,
                    Price = 120,
                    ProductImgUrl = "~/Vino/vinarija_kaptol_grasevina.jpg",
                },
                new Product
                {
                    Id = 2,
                    Title = "Muškat zlatni",
                    Description = "Desertno suho vino - Berba 2020.",

                    ProductCategoryId = 1,
                    Quantity = 50,
                    Price = 180,
                    ProductImgUrl = "~/Vino/krauthaker-muskat_zuti_1.jpg",
                },
                new Product
                {
                    Id = 3,
                    Title = "Sok od Kupine",
                    Description = "Domaći sok od kupine - 0.7l",
                    ProductCategoryId = 2,
                    Quantity = 15,
                    Price = 60,
                    ProductImgUrl = "~/Sok/063-Sok-kupina-1-l-Sedic-copy.png",
                },
                 new Product
                 {
                     Id = 4,
                     Title = "Med-Bagrem",
                     Description = "Domaći med od bagrema - 2021. 450g",
                     ProductCategoryId = 3,
                     Quantity = 20,
                     Price = 120,
                     ProductImgUrl= "~/Med/BagremMed.png",
                 },
                new Product
                {
                    Id = 5,
                    Title = "Med-Lipa",
                    Description = "Domaći med od bagrema - 2021. 450g",
                    ProductCategoryId = 3,
                    Quantity = 65,
                    Price = 75,
                    ProductImgUrl = "~/Med/medlipa.png",
                },
               
                new Product
                {
                    Id = 6,
                    Title = "Sok aronija",
                    Description = "Domaći sok od aronije - 1l 2021.",
                    ProductCategoryId = 2,
                    Quantity = 50,
                    Price = 80,
                    ProductImgUrl = "~/Sok/Aronija.png",
                });

        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Adress> Adress { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<FileStorage> FileStorage { get; set; }
        public DbSet<PavlicWebShop.Models.ViewModel.ProductCategoryViewModel> ProductCategoryViewModel { get; set; }
        public DbSet<PavlicWebShop.Models.ViewModel.ApplicationUserViewModel> ApplicationUserViewModel { get; set; }
        public DbSet<PavlicWebShop.Models.ViewModel.ProductViewModel> ProductViewModel { get; set; }
    }
}