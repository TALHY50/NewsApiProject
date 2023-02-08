using webapinews.Entities;
using webapinews.Models;

namespace webapinews.Interface
{
    public interface IUserService
    {
        UserDataResponse Authenticate(UserDataRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(User model);
        bool Update(User model);
        void Delete(int id);
    }
}
