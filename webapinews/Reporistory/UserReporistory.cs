using Microsoft.Extensions.Options;
using webapinews.Models;
using webapinews.Interface;
using webapinews.Entities;
using webapinews.ExceptionHandler;
using webapinews.Helpers;

namespace webapinews.Services
{
    public class UserReporistory : IUserReporistory
    {
        private NewsApiContext _context;
        private IJwtAuth _jwtUtils;
        private readonly AppSettings _appSettings;
       

        public UserReporistory(NewsApiContext context, IJwtAuth jwtUtils, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public UserInputResponse Authenticate(UserInputModel model)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

            // validate
            if (user == null)
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new UserInputResponse(user, jwtToken);
            
        }

        public List<User> GetAll()
        {
            var user = _context.Users;
            return user.ToList();
        }       
        public PaginatedList<User> Get(PaginatedViewModel paginatedViewModel)
        {
            var model = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(paginatedViewModel.search))
            {
                model = model.Where(hh => hh.UserName.Contains(paginatedViewModel.search));
            }
            if (!string.IsNullOrEmpty(paginatedViewModel.SortBy))
            {
                switch (paginatedViewModel.SortBy)
                {
                    case "user_desc": model = model.OrderByDescending(hh => hh.UserName); break;
                    case "Id_asc": model = model.OrderBy(hh => hh.Id); break;
                    case "Id_desc": model = model.OrderByDescending(hh => hh.Id); break;
                    case "email_asc": model = model.OrderBy(hh => hh.Email); break;
                    case "email_desc": model = model.OrderByDescending(hh => hh.Email); break;
                }
            }
            var result = PaginationHelper.Create(model, paginatedViewModel);
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
