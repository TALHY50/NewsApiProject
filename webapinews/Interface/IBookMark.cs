using webapinews.Entities;
using webapinews.Models;
using static webapinews.Reporistory.BookMarkServices;

namespace webapinews.Interface
{
    public interface IBookMark
    {
        IEnumerable<News> GetAll();
       List<BookMark> BookMarkNews(int newsId, int userId);
         List<BookMarksViewModel> GetById(int id);
        bool Update(int id, BookMark bookMark);
        bool Delete(int id, int userId);
       
    }
}
