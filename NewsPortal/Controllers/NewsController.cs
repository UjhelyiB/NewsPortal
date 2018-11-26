using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Models.CSharpModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortal.Controllers
{
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private DAL dal = new DAL();

        [HttpGet("[action]")]
        public IEnumerable<NewsModel> GetAllNews()
        {
            return dal.GetAllNews();
        }

        [HttpGet("[action]")]
        public IEnumerable<NewsModel> GetNewsByCategory([FromQuery]string category)
        {
            return dal.GetNewsByCategoryId(Int32.Parse(category));
        }

        [HttpGet("[action]")]
        public IEnumerable<NewsModel> GetValidNews()
        {
            return dal.GetValidNews();
        }

        [HttpGet("[action]/{id}")]
        public NewsModel GetNews([FromRoute]int id)
        {
            return dal.GetNews(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews([FromRoute]int id)
        {
            await dal.DeleteNews(id);
            return NoContent();
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateNews([FromBody] JObject data)
        {
            NewsModel model = data["model"].ToObject<NewsModel>();

            var createdNews = await dal.CreateNews(model);
            return Created($"api/news/{createdNews.Id}", createdNews);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews([FromRoute]int id, [FromBody]NewsModel model)
        {
            await dal.UpdateNews(id, model);
            return Accepted(model);
        }

        [HttpGet("[action]")]
        public IEnumerable<NewsListItemModel> Search([FromQuery]string q)
        {
            return dal.SearchNews(q);
        }
    }
}
