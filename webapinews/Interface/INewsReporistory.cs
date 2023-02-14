using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Models;

namespace webapinews.Interface
{
    public interface INewsReporistory
    {
        List<News> GetAll();
        PaginatedList<News> Get(OwnerStringParameter ownerStringParameter);
     
        News Add(News news); 
        News GetById(int id);
        News Update(int id, News news);
        News Delete(int id);

        
    }
}
