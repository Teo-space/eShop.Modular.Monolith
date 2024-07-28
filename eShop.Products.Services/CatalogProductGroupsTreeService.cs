using eShop.Products.Domain.Models;
using eShop.Products.Interfaces.DbContexts;
using eShop.Products.Interfaces.Services;
using eShop.Products.Models.Catalog.ProductGroups;
using Microsoft.EntityFrameworkCore;

namespace eShop.Products.Services;

internal class CatalogProductGroupsTreeService(ICatalogDbContext catalogDbContext) : ICatalogProductGroupsTreeService
{
    public async Task<IReadOnlyCollection<ProductGroup>> GetProductGroups()
    => (await catalogDbContext.ProductGroups.AsNoTracking().ToArrayAsync());

    public async Task<Result<ProductGroupTreeModel>> GetProductGroupsTree()
    {
        var model = new ProductGroupTreeModel();

        var productGroups = await GetProductGroups();
        model.TotalCount = productGroups.Count;

        model.ProductGroups = productGroups
            .Where(x => x.ParentProductGroupId == 0)
            .Select(productGroup => new ProductGroupModel()
            {
                ProductGroupId = productGroup.ProductGroupId,
                Name = productGroup.Name,
                Description = productGroup.Description,
            })
            .OrderBy(x => x.Name)
            .ToArray();

        List<ProductGroupModel> levelResults = new List<ProductGroupModel>(model.ProductGroups);
        List<ProductGroupModel> nextLevelResults = new List<ProductGroupModel>();

        while (levelResults.Any())
        {
            foreach (var productGroup in levelResults)
            {
                var childs = productGroups
                    .Where(x => x.ParentProductGroupId == productGroup.ProductGroupId)
                    .Select(productGroup => new ProductGroupModel()
                    {
                        ProductGroupId = productGroup.ProductGroupId,
                        Name = productGroup.Name,
                        Description = productGroup.Description,
                    })
                    .OrderBy(x => x.Name)
                    .ToArray();

                if (childs.Any())
                {
                    productGroup.Childs = childs;
                    nextLevelResults.AddRange(childs);
                }
            }

            levelResults.Clear();
            if (nextLevelResults.Any())
            {
                levelResults.AddRange(nextLevelResults);
                nextLevelResults.Clear();
            }
        }

        return model;
    }

}
