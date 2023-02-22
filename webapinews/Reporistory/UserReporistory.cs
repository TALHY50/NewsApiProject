using Microsoft.Extensions.Options;
using webapinews.Models;
using webapinews.Interface;
using webapinews.Entities;
using webapinews.ExceptionHandler;
using webapinews.Helpers;
using NuGet.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using webapinews.Helpers.Paging;
using webapinews.FilterandSorting;

namespace webapinews.Services
{
    public class UserReporistory : IUserReporistory
    {
        private NewsApiCodeContext _context;
        private IJwtAuth _jwtUtils;
        private readonly AppSettings _appSettings;


        public UserReporistory(NewsApiCodeContext context, IJwtAuth jwtUtils, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public User Authenticate(UserInputModel model)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

            // validate
            if (user == null)
                throw new AppException("Username or password is incorrect");

            return user;
        }

        public List<User> GetAll()
        {
            var user = _context.Users;
            return user.ToList();
        }
        public PaginatedList<User> Get(PaginatedViewModel paginatedViewModel)
        {
            var model = _context.Users.AsQueryable();
            var filter = Filtering.Filter<User>(paginatedViewModel.columnName, paginatedViewModel.search, model);
            var sort = Sorting<User>.Sort(paginatedViewModel.SortBy, paginatedViewModel.columnName, filter.AsQueryable());
            var result = PaginationHelper.Create(sort.AsQueryable(), paginatedViewModel);
            _context.SaveChanges();
            return result;
        }
        public User GetById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public User Register(User model)
        {
            if (_context.Users.Any(x => x.UserName == model.UserName))
                throw new AppException("Username '" + model.UserName + "' is already taken");
            _context.Users.Add(model);
            _context.SaveChanges();
            return model;
        }

        public User Update(User model)
        {
            var user = _context.Users.Where(s => s.Id == model.Id).FirstOrDefault();
            if (user == null)
            {

                return null;
            }
            user.Id = model.Id;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.Password = model.Password;
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public User Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            var bookmarks = _context.BookMarks.Where(b => b.UserId == user.Id);
            _context.BookMarks.RemoveRange(bookmarks);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }


    }
}

