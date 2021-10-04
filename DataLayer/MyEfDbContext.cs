using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
                .HasIndex(ws => new {ws.Latitude, ws.Longitude});

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            
            builder.AddQueryFilter<WaterUser>(x => x.DeletedAt == null);
            builder.AddQueryFilter<BaseEntity>(x => x.DeletedAt == null);

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
                    Expression.Equal(Expression.Property(param, ((MemberExpression)((BinaryExpression)expression.Body).Left).Member.Name), ((BinaryExpression)expression.Body).Right),
                        param);

                builder.Entity(type).HasQueryFilter(filter);
            }
        }
    }
}
