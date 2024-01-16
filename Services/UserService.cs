namespace User.Services
{
    public class UserService
    {
        private List<Models.User> Users { get; }
        private int nextId = 3;

        public UserService()
        {
            Users =
            [
                new Models.User { Id = 1, Name = "Super Admin", IsActive = false },
                new Models.User { Id = 2, Name = "Customer", IsActive = true }
            ];
        }

        public List<Models.User> GetAll()
        {
            return Users;
        }

        public void Add(Models.User user)
        {
            user.Id = nextId++;
            Users.Add(user);
        }

        public Models.User? Get(int id)
        {
            return Users.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Models.User user)
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