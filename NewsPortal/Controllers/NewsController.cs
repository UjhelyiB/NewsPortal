using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Models.CSharpModels;
using NewsPortal.Models.DatabaseObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortal.Controllers
{
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private DAL dal = new DAL();

        [HttpGet("[action]")]
        public IEnumerable<News> GetAllNews()
        {
            return dal.GetAllNews();
        }

        [HttpGet("{id}")]
        public async Task<NewsModel> GetNews([FromRoute]int id)
        {
            return await dal.GetNews(id);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteNews([FromRoute]int id)
        {
            await dal.DeleteNews(id);
            return NoContent();
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> CreateNews([FromBody]NewsModel model)
        {
            var createdNews = await dal.CreateNews(model);
            return Created($"api/news/{createdNews.Id}", createdNews);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateNews([FromRoute]int id, [FromBody]NewsModel model)
        {
            await dal.UpdateNews(id, model);
            return Accepted(model);
        }

        [HttpGet("[action]")]
        public IEnumerable<NewsListItemModel> search([FromQuery]string q)
        {
            return dal.SearchNews(q);
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
