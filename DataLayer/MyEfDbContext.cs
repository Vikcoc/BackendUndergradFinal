using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataLayer
{
    public class MyEfDbContext : IdentityDbContext<WaterUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<WaterSourceContribution> WaterSourceContributions { get; set; }
        public DbSet<WaterSourcePicture> WaterSourcePictures { get; set; }
        public DbSet<WaterSourcePlace> WaterSourcePlaces { get; set; }
        public DbSet<WaterSourceVariant> WaterSourceVariants { get; set; }

        public MyEfDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<WaterSourcePlace>()
                .HasIndex(ws => new { ws.Latitude, ws.Longitude });

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.AddQueryFilter<WaterUser>(x => x.DeletedAt == null);
            builder.AddQueryFilter<BaseEntity>(x => x.DeletedAt == null);

            builder.Entity<WaterSourceVariant>().HasData(new WaterSourceVariant { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), CreatedAt = new DateTime(2022, 2, 28, 18, 22, 47, 43, DateTimeKind.Utc), Name = "Classic", Description = "Simple style but effective at quenching thirst" });
            builder.Entity<WaterSourcePicture>().HasData(new WaterSourcePicture { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), CreatedAt = new DateTime(2022, 2, 28, 18, 22, 47, 43, DateTimeKind.Utc), Uri = "Pictures/Default/Classic1.jpg", WaterSourceVariantId = Guid.Parse("00000000-0000-0000-0000-000000000001") });

            builder.Entity<WaterSourceVariant>().HasData(new WaterSourceVariant { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), CreatedAt = new DateTime(2022, 2, 28, 18, 22, 47, 43, DateTimeKind.Utc), Name = "Doggie", Description = "Now with design for dogs" });
            builder.Entity<WaterSourcePicture>().HasData(new WaterSourcePicture { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), CreatedAt = new DateTime(2022, 2, 28, 18, 22, 47, 43, DateTimeKind.Utc), Uri = "Pictures/Default/Dog1.jpg", WaterSourceVariantId = Guid.Parse("00000000-0000-0000-0000-000000000002") });

            builder.Entity<WaterSourceVariant>().HasData(new WaterSourceVariant { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), CreatedAt = new DateTime(2022, 2, 28, 18, 22, 47, 43, DateTimeKind.Utc), Name = "Old time", Description = "Imagined in another time" });
            builder.Entity<WaterSourcePicture>().HasData(new WaterSourcePicture { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), CreatedAt = new DateTime(2022, 2, 28, 18, 22, 47, 43, DateTimeKind.Utc), Uri = "Pictures/Default/OldTime1.jpg", WaterSourceVariantId = Guid.Parse("00000000-0000-0000-0000-000000000003") });

        }


    }

    public static class EfDbHelper
    {
        public static void AddQueryFilter<T>(this ModelBuilder builder, Expression<Func<T, bool>> expression)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var type = entityType.ClrType;
                if (!type.IsSubclassOf(typeof(T)))
                    continue;

                var param = Expression.Parameter(type);
                var filter = Expression.Lambda(
                    Expression.MakeBinary(((BinaryExpression)expression.Body).NodeType, Expression.Property(param, ((MemberExpression)((BinaryExpression)expression.Body).Left).Member.Name), ((BinaryExpression)expression.Body).Right),
                        param);

                builder.Entity(type).HasQueryFilter(filter);
            }
        }
    }
}
