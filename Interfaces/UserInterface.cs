namespace Iam.Interface;

public class IUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public int? RoleId { get; set; }
    public IRole? Role { get; set; }

    public IUser()
    {
        Name = string.Empty;
    }
}