using webapinews.Helpers;
using webapinews.Helpers.Paging;
using webapinews.Mappers.BookMarkMapper;
using webapinews.Models;
using static webapinews.Reporistory.BookMarkReporistory;

namespace webapinews.Interface
{
    public interface IBookMarkReporistory
    {
        List<BookMark> BookMarkNews(int newsId, int userId);
        List<BookMarksViewModel> GetById(int id);
        PaginatedList<BookMarksViewModel> GetBookMarkedById(int id,PaginatedViewModel paginatedViewModel);
    
        string Delete(int id, int userId);
       
    }
}
