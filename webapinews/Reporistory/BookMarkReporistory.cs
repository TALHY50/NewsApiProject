using Microsoft.EntityFrameworkCore;
using webapinews.Helpers;
using webapinews.Interface;
using webapinews.Mappers.BookMarkMapper;
using webapinews.Models;

namespace webapinews.Reporistory
{
    public class BookMarkReporistory : IBookMarkReporistory
    {

        private readonly NewsApiContext _context;
        private IJwtAuth _jwtUtils;
        int _nextId = 0;
        public BookMarkReporistory(NewsApiContext context, IJwtAuth jwtutils)
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
                existingBookmark.CreationDate = DateTime.Now;
            }
            else
            {
                BookMark newBookmark = new BookMark
                {
                    Id = _nextId++,
                    NewsId = newsId,
                    UserId = userId,
                    CreationDate = DateTime.Now,
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
                CreationDate = e.News.CreationDate,
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
                CreationDate = e.News.CreationDate,
                IsBookMark = e.IsBookMark
            });
            if (!string.IsNullOrEmpty(paginatedViewModel.search))
            {
                mappedData = mappedData.Where(hh => hh.userName.Contains(paginatedViewModel.search));
            }
            if (!string.IsNullOrEmpty(paginatedViewModel.SortBy))
            {
                switch (paginatedViewModel.SortBy)
                {
                    case "user_desc": mappedData = mappedData.OrderByDescending(hh => hh.userName); break;
                    case "Id_asc": mappedData = mappedData.OrderBy(hh => hh.NewsId); break;
                    case "Id_desc": mappedData = mappedData.OrderByDescending(hh => hh.NewsId); break;
                    case "email_asc": mappedData = mappedData.OrderBy(hh => hh.Email); break;
                    case "email_desc": mappedData = mappedData.OrderByDescending(hh => hh.Email); break;
                }
            }
            var result = PaginationHelper.Create(mappedData, paginatedViewModel);
            return result;

        }


    }

}