namespace Iam.Interface;

public class IRole
{
    public int Id { get; set; }
    public string Name { get; set; }

    public IRole()
    {
        Name = string.Empty;
    }
}