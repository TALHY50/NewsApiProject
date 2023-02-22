using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Helpers.Paging;
using webapinews.Models;

namespace webapinews.Interface
{
    public interface INewsReporistory
    {
        List<News> GetAll();
        PaginatedList<News> Get(PaginatedViewModel paginatedViewModel);
     
        News Add(News news); 
        News GetById(int id);
        News Update(News news);
        string Delete(int id);

        
    }
}
