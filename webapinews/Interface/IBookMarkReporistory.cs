using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Models;
using static webapinews.Reporistory.BookMarkReporistory;

namespace webapinews.Interface
{
    public interface IBookMarkReporistory
    {
        List<BookMark> BookMarkNews(int newsId, int userId);
        List<BookMarksViewModel> GetById(int id);
        PaginatedList<BookMarksViewModel> GetBookMarkedById(int id,OwnerStringParameter ownerStringParameter);
    
        BookMark Delete(int id, int userId);
       
    }
}
