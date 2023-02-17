using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Models;
using webapinews.Reporistory;

namespace webapinews.Interface
{
    public interface IUserReporistory 
    {
        UserInputResponse Authenticate(UserInputModel model);
        List<User> GetAll();
        PaginatedList<User> Get(PaginatedViewModel paginatedViewModel);
        User GetById(int id);
        User Register(User model);
        User Update(User model);
        User Delete(int id);
    }
}
