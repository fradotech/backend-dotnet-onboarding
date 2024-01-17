namespace Iam.Models;

public class AppUser
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsActive { get; set; }

    public int RoleId { get; set; }
    public required Role Role { get; set; }
}