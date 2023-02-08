using webapinews.Interface;
using webapinews.Models;

namespace webapinews.Reporistory
{
    public class NewsService : INews
    {

        private NewsApiContext _context;

        public NewsService(NewsApiContext context)
        {
            _context = context;
            
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
            throw new NotImplementedException();
        }

        public IEnumerable<News> GetAll()
        {
            return _context.News;
            throw new NotImplementedException();
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

        private News GetNews(int id)
        {
            var news = _context.News.Find(id);
            if (news == null) throw new KeyNotFoundException("News not found");
            return news;
        }
    }
}
