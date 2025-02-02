namespace eShop.Products.Domain.Models;

public sealed class Region
{
    public int RegionId { get; set; }
    public string Name { get; set; }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private Region() { }

    public static Region Create(string name)
    {
        return new Region
        {
            RegionId = 0,
            Name = name
        };
    }

    public static Region CreateExists(int regionId, string name)
    {
        return new Region
        {
            RegionId = regionId,
            Name = name
        };
    }

}
