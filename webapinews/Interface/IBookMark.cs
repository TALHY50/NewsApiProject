using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Models;
using static webapinews.Reporistory.BookMarkServices;

namespace webapinews.Interface
{
    public interface IBookMark
    {
        IEnumerable<News> GetAll();
        PaginatedList<News> Get(OwnerStringParameter ownerStringParameter);
        List<BookMark> BookMarkNews(int newsId, int userId);
        List<BookMarksViewModel> GetById(int id);
        PaginatedList<BookMarksViewModel> GetBookMarkedById(int id,OwnerStringParameter ownerStringParameter);
        bool Update(int id, BookMark bookMark);
        bool Delete(int id, int userId);
       
    }
}
