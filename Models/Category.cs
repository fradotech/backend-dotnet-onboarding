using Product.Interface;

namespace Product.Models;

public class Category: IProduct
{
    public new int Id { get; set; }
    public new required string Name { get; set; }
    public new string? Description { get; set; }
}