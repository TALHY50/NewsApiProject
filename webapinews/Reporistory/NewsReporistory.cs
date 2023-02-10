﻿using Microsoft.EntityFrameworkCore;
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
        public IEnumerable<News> GetAll()

        {
            var news = _context.News;
            return news;
        }
        public PaginatedList<News> Get(OwnerStringParameter ownerStringParameter)
        {
            var qurey = _context.News.AsQueryable();
            if (!string.IsNullOrEmpty(ownerStringParameter.search))
            {
                qurey = qurey.Where(hh => hh.Title.Contains(ownerStringParameter.search));
            }
            qurey = qurey.OrderBy(hh => hh.Aurthor);
            if (!string.IsNullOrEmpty(ownerStringParameter.SortBy))
            {
                switch (ownerStringParameter.SortBy)
                {
                    case "title_desc": qurey = qurey.OrderByDescending(hh => hh.Title); break;
                    case "aurthor_asc": qurey = qurey.OrderBy(hh => hh.Aurthor); break;
                    case "aurthor_desc": qurey = qurey.OrderByDescending(hh => hh.Aurthor); break;
                    case "date_asc": qurey = qurey.OrderBy(hh => hh.CreationDate); break;
                    case "date_desc": qurey = qurey.OrderByDescending(hh => hh.CreationDate); break;
                }
            }

            var result = PaginatedList<News>.Create(qurey, ownerStringParameter);
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

        public void Delete(int id)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == id);
            if (news == null) throw new KeyNotFoundException("News not found");
            var bookMark = _context.BookMarks.FirstOrDefault(x => x.NewsId== id);
            _context.BookMarks.RemoveRange(bookMark);
            _context.News.Remove(news);
            _context.SaveChanges();
           
        }

        public News GetById(int id)
        {
            var news = _context.News.Find(id);
            if (news == null) throw new KeyNotFoundException("News not found");
            return news;
           
        }

        public bool Update(int id ,News news)
        {
            var request = _context.News.FirstOrDefault(x => x.Id == id);

            if (request == null)
            {
                return false;
            }
          
            request.Title = news.Title;
            request.Aurthor = news.Aurthor;
            request.Content = news.Content;
            _context.News.Update(news);
            _context.SaveChanges();
            return true;
        }
        
    }
}