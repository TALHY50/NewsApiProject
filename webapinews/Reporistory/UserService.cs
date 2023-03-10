using Microsoft.Extensions.Options;
using webapinews.Models;
using BCrypt.Net;
using webapinews.Interface;
using webapinews.Entities;
using webapinews.ExceptionHandler;

namespace webapinews.Services
{
    public class UserService : IUserService
    {
        private NewsApiContext _context;
        private IJwtAuth _jwtUtils;
        private readonly AppSettings _appSettings;
       

        public UserService(NewsApiContext context, IJwtAuth jwtUtils, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public UserDataResponse Authenticate(UserDataRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

            // validate
            if (user == null)
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new UserDataResponse(user, jwtToken);
            
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
            
        }

        public User GetById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public void Register(User model)
        {
            if (_context.Users.Any(x => x.UserName == model.UserName))
                throw new AppException("Username '" + model.UserName + "' is already taken");

            model.Id = model.Id;
            model.UserName = model.UserName;
           model.Email = model.Email;
            model.Password = model.Password;
            //model.Role = model.Role;
            _context.Users.Add(model);
            _context.SaveChanges();
        }

        public bool Update(User model)
        {

            var user = _context.Users.Where(s => s.Id == model.Id).FirstOrDefault();
            if (user == null)
            {

                return false;
            } 
            user.Id = model.Id;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.Password = model.Password;
            user.Role = model.Role;
            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            var bookmarks = _context.BookMarks.Where(b => b.UserId == user.Id);
            _context.BookMarks.RemoveRange(bookmarks);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }


    }
}
