using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Models;

namespace webapinews.Interface
{
    public interface INewsReporistory
    {
        IEnumerable<News> GetAll();
        PaginatedList<News> Get(OwnerStringParameter ownerStringParameter);
     
        News Add(News news); 
        News GetById(int id);
        bool Update(int id, News news);
        void Delete(int id);

        
    }
}
