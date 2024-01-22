namespace Product.Interface;

public class IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public IProduct()
    {
        Name = string.Empty;
    }
}