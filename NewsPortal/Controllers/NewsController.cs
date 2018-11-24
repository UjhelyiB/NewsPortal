using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Models;
using NewsPortal.Models.DatabaseObjects;

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
