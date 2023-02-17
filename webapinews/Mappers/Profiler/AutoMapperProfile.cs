using AutoMapper;
using webapinews.Command.BookMark_Commands;
using webapinews.Command.News_Commands;
using webapinews.Command.User_Commands;
using webapinews.Helpers;
using webapinews.Mappers.BookMarkMapper;
using webapinews.Mappers.News_Mapper;
using webapinews.Mappers.NewsMapper;
using webapinews.Mappers.UserMapper;
using webapinews.Models;

namespace webapinews.Mappers.Profiler
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<User, UserViewModel>();
            CreateMap<PaginatedList<User>, PaginatedList<UserViewModel>>();
            CreateMap<RegistorUserCommand, User>();
            CreateMap<User, RegistorUserViewModel>();
            CreateMap<UpdateUserCommand, User>();
            CreateMap<User, UpdateUserViewModel>();


            CreateMap<News, NewsViewModel>();
            CreateMap<AddNewsCommand, News>();
            CreateMap<News, AddNewsViewModel>();
            CreateMap<UpdateNewsCommand, News>();
            CreateMap<News, UpdateNewsViewModel>();




            CreateMap<BookMark, BookMarksViewModel>();
            CreateMap<List<BookMark>, BookMarksViewModel>();
            //.ForMember(dest => dest.BookMarks, opt => opt.MapFrom(src => src));
            CreateMap<DeleteBookMarkCommand, BookMark>();
            CreateMap<PaginatedList<News>, PaginatedList<NewsViewModel>>();
            //CreateMap<BookMark, BookMarksViewModel>()
            ////.ForMember(dest => dest.BookMarks, opt => opt.MapFrom(src => src));
        }
    }
}
