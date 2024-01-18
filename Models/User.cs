using Iam.Interface;

namespace Iam.Models;

public class AppUser: IUser
{
    public new int Id { get; set; }
    public new required string Name { get; set; }
    public new bool IsActive { get; set; }

    public new int? RoleId { get; set; }
    public new Role? Role { get; set; }
}