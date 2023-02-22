using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Globalization;
using webapinews.Entities;
using webapinews.FilterandSorting;
using webapinews.Helpers;
using webapinews.Helpers.Paging;
using webapinews.Interface;
using webapinews.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace webapinews.Reporistory
{
    public class NewsReporistory : INewsReporistory
    {
        //PaginatedList<News> _paginationResult = new PaginatedList<News>;
        //Filtering _filtering = new Filtering();
        //Sorting<News> _sorting = new Sorting<News>();


        private NewsApiCodeContext _context;

        public NewsReporistory(NewsApiCodeContext context)
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
            var query = _context.News.AsQueryable();
            var filter = Filtering.Filter<News>(paginatedViewModel.columnName, paginatedViewModel.search, query);
            var sort = Sorting<News>.Sort(paginatedViewModel.SortBy, paginatedViewModel.columnName, filter.AsQueryable());
            var result = PaginationHelper.Create(sort.AsQueryable(),paginatedViewModel);
            _context.SaveChanges();
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
