using eShop.Products.Domain.Interfaces;

namespace eShop.Products.Domain.Models;

public class Param : IDeletable
{

    public int ParamId { get; set; }

    public int ParamGroupId { get; set; }

    public bool IsDeleted { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    

}
