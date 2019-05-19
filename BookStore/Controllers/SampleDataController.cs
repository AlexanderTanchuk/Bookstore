using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
// http://bookstore/api/SampleData/WeatherForecasts?startDateIndex=0
namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private BookStoreContext db;
        public SampleDataController(BookStoreContext context)
        {
            db = context;
        }

        [HttpGet("[action]")]
        public IList<Book> Create()
        {
            var book1 = new Book
            {
                Name = "Shrek"
            };
            var book2 = new Book
            {
                Name = "Shrek Retard"
            };

            db.Books.Add(book1);
            db.Books.Add(book2);
            db.SaveChanges();
            var nomad = db.Books.Select(row => row).ToList();
            return nomad;
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts(int startDateIndex, string tanch)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index + startDateIndex).ToString("d")+ tanch,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
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
