using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Globalization;
using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Interface;
using webapinews.Models;

namespace webapinews.Reporistory
{
    public class NewsReporistory : INewsReporistory
    {

        private NewsApiContext _context;

        public NewsReporistory(NewsApiContext context)
        {
            _context = context;
            
        }
        public List<News> GetAll()

        {
            var news = _context.News;
            return news.ToList();
        }
        public PaginatedList<News> Get(PaginatedViewModel paginatedViewModel)
        {
            var qurey = _context.News.AsQueryable();
            if (!string.IsNullOrEmpty(paginatedViewModel.search))
            {
                qurey = qurey.Where(hh => hh.Title.Contains(paginatedViewModel.search));
            }
            qurey = qurey.OrderBy(hh => hh.Aurthor);
            if (!string.IsNullOrEmpty(paginatedViewModel.SortBy))
            {
                switch (paginatedViewModel.SortBy)
                {
                    case "title_desc": qurey = qurey.OrderByDescending(hh => hh.Title); break;
                    case "aurthor_asc": qurey = qurey.OrderBy(hh => hh.Aurthor); break;
                    case "aurthor_desc": qurey = qurey.OrderByDescending(hh => hh.Aurthor); break;
                    case "date_asc": qurey = qurey.OrderBy(hh => hh.CreationDate); break;
                    case "date_desc": qurey = qurey.OrderByDescending(hh => hh.CreationDate); break;
                }
            }

            var result = PaginationHelper.Create(qurey, paginatedViewModel);
            return result;
        }
        public News Add(News news)
        {
          if (news == null) 
         throw new KeyNotFoundException("News not found");
            _context.News.Add(news);
            _context.SaveChanges();
            return news;
        }

        public string Delete(int id)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == id);
            if (news == null)
            {
                return null;
            }
            var bookmarks = _context.BookMarks.Where(x => x.NewsId == id);
            _context.BookMarks.RemoveRange(bookmarks);
            _context.News.Remove(news);
            _context.SaveChanges();
            return "News Deleted SuccessFully";

        }

        public News GetById(int id)
        {
            var news = _context.News.Find(id);
            if (news == null) throw new KeyNotFoundException("News not found");
            return news;
           
        }

        public News Update(News news)
        {
            var request = _context.News.Where(x => x.Id ==news.Id).FirstOrDefault();

            if (request == null)
            {
                return null;
            }
            _context.News.Update(request);
            _context.SaveChanges();
            return request;
        }
        
    }
}
