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
        User Register(User model);
        User Update(User model);
        User Delete(int id);
    }
}
