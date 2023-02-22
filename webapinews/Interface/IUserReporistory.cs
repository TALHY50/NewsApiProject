using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Helpers.Paging;
using webapinews.Models;
using webapinews.Reporistory;

namespace webapinews.Interface
{
    public interface IUserReporistory 
    {
        User Authenticate(UserInputModel model);
        List<User> GetAll();
        PaginatedList<User> Get(PaginatedViewModel paginatedViewModel);
        User GetById(int id);
        User Register(User model);
        User Update(User model);
        User Delete(int id);
    }
}
