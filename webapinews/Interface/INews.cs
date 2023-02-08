using webapinews.Models;

namespace webapinews.Interface
{
    public interface INews
    {
        IEnumerable<News> GetAll();
        News Add(News news);
        News GetById(int id);
        bool Update(int id, News news);
        void Delete(int id);
    }
}
