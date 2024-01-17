namespace Iam.Models;

public class AppUser
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; }

    public int? RoleId { get; set; }
    public Role? Role { get; set; }
}