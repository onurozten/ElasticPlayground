using EasticPlayground.Elastic;
using EasticPlayground.Models;
using Elastic.Clients.Elasticsearch;
using ElasticPlayground.Elastic;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EasticPlayground.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IElasticClient _elasticClient;

        public HomeController(ILogger<HomeController> logger, IElasticClient elasticClient)
        {
            _logger = logger;
            _elasticClient = elasticClient;

            var data = _elasticClient.CreateSampleData();

            //var result = _elasticClient.IndexMany(data);

            _elasticClient.SearchWithHighlights("onur").Wait();
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
