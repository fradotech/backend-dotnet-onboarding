using User.Models;

namespace User.Services;

public static class UserService
{
    static List<UserModel> Users { get; }
    static int nextId = 3;
    static UserService()
    {
        Users = new List<UserModel>
        {
            new UserModel { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
            new UserModel { Id = 2, Name = "Veggie", IsGlutenFree = true }
        };
    }

    public static List<UserModel> GetAll() => Users;

    public static UserModel? Get(int id) => Users.FirstOrDefault(p => p.Id == id);

    public static void Add(UserModel user)
    {
        user.Id = nextId++;
        Users.Add(user);
    }

    public static void Delete(int id)
    {
        var user = Get(id);
        if(user is null)
            return;

        Users.Remove(user);
    }

    public static void Update(UserModel user)
    {
        var index = Users.FindIndex(p => p.Id == user.Id);
        if(index == -1)
            return;

        Users[index] = user;
    }
}