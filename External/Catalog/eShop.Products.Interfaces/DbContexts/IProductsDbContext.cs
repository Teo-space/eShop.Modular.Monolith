using eShop.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace eShop.Products.Interfaces.DbContexts;

public interface IProductsDbContext : IBaseDbContext
{
    DbSet<Maker> Makers { get; }

    DbSet<ProductGroup> ProductGroups { get; }
    DbSet<ProductType> ProductTypes { get; }
    DbSet<ProductTypeParam> ProductTypeParams { get; }

    DbSet<Product> Products { get; }
    DbSet<ProductParamValue> ProductParamValues { get; }

    DbSet<Param> Params { get; }
    DbSet<ParamValue> ParamValues { get; }
}
