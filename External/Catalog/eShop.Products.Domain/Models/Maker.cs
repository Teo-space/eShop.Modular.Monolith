namespace eShop.Products.Domain.Models;

/// <summary>
/// Производитель товара
/// </summary>
public sealed class Maker
{
    public int MakerId { get; private set; }

    public string Name { get; private set; }

    public List<Product> Product { get; private set; } = new List<Product>();

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private Maker() { }

    public static Maker Create(string name)
    {
        return new Maker
        {
            Name = name,
        };
    }

    public static Maker CreateExists(int makerId, string name)
    {
        return new Maker
        {
            MakerId = makerId,
            Name = name,
        };
    }


}
