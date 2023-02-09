using webapinews.Interface;
using webapinews.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using static webapinews.Reporistory.BookMarkServices;
using webapinews.Entities;
using webapinews.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace webapinews.Reporistory
{
    public class BookMarkServices : IBookMark
    {

            private readonly NewsApiContext _context;
            private IJwtAuth _jwtUtils;
            int _nextId = 0;
        public BookMarkServices(NewsApiContext context, IJwtAuth jwtutils)
            {
                _context = context;
                _jwtUtils = jwtutils;
             
            }
            public bool Delete(int id, int userId)
            {
                var user = _context.BookMarks.Where(x => x.UserId == userId && x.NewsId == id).FirstOrDefault();

                if (user is null)
                {
                    return false;
                }

                _context.BookMarks.Remove(user);
                _context.SaveChanges();
                return true;
            }

        public IEnumerable<News> GetAll()
        {
           return _context.News;
        }
        public PaginatedList<News> Get(OwnerStringParameter ownerStringParameter)
        {
            var qurey = _context.News.AsQueryable();
            if (!string.IsNullOrEmpty(ownerStringParameter.search))
            {
                qurey = qurey.Where(hh => hh.Title.Contains(ownerStringParameter.search));
            }
            var result = PaginatedList<News>.Create(qurey, ownerStringParameter);
            return result;
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
                Email= e.User.Email,
                NewsId= e.NewsId,
                Aurthor = e.News.Aurthor,
                Title = e.News.Title,
                 Content= e.News.Content,
                CreationDate = e.News.CreationDate,
                 IsBookMark =e.IsBookMark
            });
            return mappedData.ToList();
        }
        public PaginatedList<BookMarksViewModel> GetBookMarkedById(int id, OwnerStringParameter ownerStringParameter)
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
            if (!string.IsNullOrEmpty(ownerStringParameter.search))
            {
                mappedData = mappedData.Where(hh => hh.userName.Contains(ownerStringParameter.search));
            }
            if (!string.IsNullOrEmpty(ownerStringParameter.SortBy))
            {
                switch (ownerStringParameter.SortBy)
                {
                    case "user_desc": mappedData = mappedData.OrderByDescending(hh => hh.userName); break;
                    case "Id_asc": mappedData = mappedData.OrderBy(hh => hh.NewsId); break;
                    case "Id_desc": mappedData = mappedData.OrderByDescending(hh => hh.NewsId); break;
                    case "email_asc": mappedData = mappedData.OrderBy(hh => hh.Email); break;
                    case "email_desc": mappedData = mappedData.OrderByDescending(hh => hh.Email); break;
                }
            }
            var result = PaginatedList<BookMarksViewModel>.Create(mappedData, ownerStringParameter);
            return result;
            
        }

        public bool Update(int id, BookMark bookMark)
        {
            throw new NotImplementedException();
        }
        
    }
    
}