using Iam.Interface;

namespace Iam.Models;

public class Role: IRole
{
    public new int Id { get; set; }
    public new required string Name { get; set; }
}