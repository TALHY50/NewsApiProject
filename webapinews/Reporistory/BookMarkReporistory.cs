using Microsoft.EntityFrameworkCore;
using webapinews.FilterandSorting;
using webapinews.Helpers;
using webapinews.Helpers.Paging;
using webapinews.Interface;
using webapinews.Mappers.BookMarkMapper;
using webapinews.Models;

namespace webapinews.Reporistory
{
    public class BookMarkReporistory : IBookMarkReporistory
    {

        private readonly NewsApiCodeContext _context;
        private IJwtAuth _jwtUtils;
        int _nextId = 0;
        public BookMarkReporistory(NewsApiCodeContext context, IJwtAuth jwtutils)
        {
            _context = context;
            _jwtUtils = jwtutils;

        }
        public string Delete(int id, int userId)
        {
          
            var bookmarks = _context.BookMarks.Where(b => b.UserId == userId).FirstOrDefault();
            _context.BookMarks.Remove(bookmarks);
            _context.SaveChanges();
            return "BookMarked News Deleted";
        }
        public List<BookMark> BookMarkNews(int newsId, int userId)
        {
            var existingBookmark = _context.BookMarks.FirstOrDefault(b => b.NewsId == newsId && b.UserId == userId);

            if (existingBookmark != null)
            {
                existingBookmark.IsBookMark = !existingBookmark.IsBookMark;
                //existingBookmark.CreationDate = DateTime.Now;
            }
            else
            {
                BookMark newBookmark = new BookMark
                {
                   Id = _nextId++,
                    NewsId = newsId,
                    UserId = userId,
                    //CreationDate = DateTime.Now,
                    IsBookMark = true
                };

                _context.BookMarks.Add(newBookmark);
                _context.SaveChanges();
            }

            return _context.BookMarks.Where(b => b.UserId == userId).ToList();
        }

        public List<BookMarksViewModel> GetById(int id)
        {

            var user = _context.BookMarks.Include(e => e.User)
            .Include(e => e.News).
            Where(e => e.UserId == id)
            .ToList();
            var mappedData = user.Select(e => new BookMarksViewModel
            {
                UserId = e.UserId,
                Email = e.User.Email,
                NewsId = e.NewsId,
                Aurthor = e.News.Aurthor,
                Title = e.News.Title,
                Content = e.News.Content,
                //CreationDate = e.News.CreationDate,
                IsBookMark = e.IsBookMark
            });
            return mappedData.ToList();
        }
        public PaginatedList<BookMarksViewModel> GetBookMarkedById(int id, PaginatedViewModel paginatedViewModel)
        {

            var user = _context.BookMarks.Include(e => e.User)
            .Include(e => e.News).
            Where(e => e.UserId == id).AsQueryable();
            var mappedData = user.Select(e => new BookMarksViewModel
            {
                UserId = e.UserId,
                userName = e.User.UserName,
                Email = e.User.Email,
                NewsId = e.NewsId,
                Aurthor = e.News.Aurthor,
                Title = e.News.Title,
                Content = e.News.Content,
                
                IsBookMark = e.IsBookMark
            });
            var filter = Filtering.Filter<BookMarksViewModel>(paginatedViewModel.columnName, paginatedViewModel.search, mappedData);
            var sort = Sorting<BookMarksViewModel>.Sort(paginatedViewModel.SortBy, paginatedViewModel.columnName, filter.AsQueryable());
            var result = PaginationHelper.Create(sort.AsQueryable(), paginatedViewModel);
            _context.SaveChanges();
            return result;
            

        }


    }

}