using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing.Printing;
using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Interface;
using webapinews.Models;
using webapinews.Services;
using AllowAnonymousAttribute = webapinews.Services.AllowAnonymousAttribute;
using AuthorizeAttribute = webapinews.Services.AuthorizeAttribute;

namespace webapinews.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private INews _newsService;

        public NewsController(INews newsService)
        {
            _newsService = newsService;
        }
        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<News>>> Get()
        {
            if (_newsService == null)
            {
                return NotFound("Not Found");

            }
            return  _newsService.GetAll().ToList();
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<News>>> Get([FromQuery] OwnerStringParameter ownerStringParameter)
        {
            if (_newsService == null)
            {
                return NotFound("No Data found");
            }
            var model = _newsService.Get(ownerStringParameter);
            var metadata = new
            {
               
                model.TotalCount,
                model.PageSize,
                model.CurrentPage,
                model.TotalPages,
                model.HasNextPage,
                model.HasPreviousPage
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(model);
        }

        [Authorize(Role.Admin, Role.User)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var news = _newsService.GetById(id);
            return Ok(news);
        }

        [Authorize(Role.Admin)]
        [HttpPost("[action]")]
        public IActionResult Add(News news)
        {
            if (news == null)
            {
            return BadRequest(new { message = "News Not SucessFully Added" });
            }
            _newsService.Add(news);
            return Ok(new { message = "News successful Added" });
        }

        [Authorize(Role.Admin)]
        [HttpPut("{id}")]
        public IActionResult Update(int id, News news)
        {
            if(news == null)
            {
                return BadRequest(new { message = "News Id Not Found " });
            }
            _newsService.Update(id, news);
            return Ok(new { message = "News updated successfully" });
        }

        [Authorize(Role.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(_newsService == null)
            {
                return BadRequest(new { message = "Deleted News Id not Found" });
            }
            _newsService.Delete(id);
            return Ok(new { message = "News deleted successfully" });

        }
    }
}
