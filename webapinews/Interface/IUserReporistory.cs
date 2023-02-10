using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Models;
using webapinews.Reporistory;

namespace webapinews.Interface
{
    public interface IUserReporistory 
    {
        UserDataResponse Authenticate(UserDataRequest model);
        List<User> GetAll();
        PaginatedList<User> Get(OwnerStringParameter ownerStringParameter);
        User GetById(int id);
        void Register(User model);
        bool Update(User model);
        void Delete(int id);
    }
}
