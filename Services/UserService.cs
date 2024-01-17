using Iam.Models;

namespace Iam.Services
{
    public class UserService
    {
        private List<AppUser> Users { get; }
        private int nextId = 3;

        public UserService()
        {
            Users =
            [
                new AppUser { Id = 1, Name = "Super Admin", IsActive = false },
                new AppUser { Id = 2, Name = "Customer", IsActive = true }
            ];
        }

        public List<AppUser> GetAll()
        {
            return Users;
        }

        public void Add(AppUser user)
        {
            user.Id = nextId++;
            Users.Add(user);
        }

        public AppUser? Get(int id)
        {
            return Users.FirstOrDefault(p => p.Id == id);
        }

        public void Update(AppUser user)
        {
            var index = Users.FindIndex(p => p.Id == user.Id);
            if (index == -1)
                return;

            Users[index] = user;
        }

        public void Delete(int id)
        {
            var user = Get(id);
            if (user is null) return;
            Users.Remove(user);
        }

    }
}