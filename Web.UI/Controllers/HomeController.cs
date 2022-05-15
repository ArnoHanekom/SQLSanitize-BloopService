using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web.UI.Models;

namespace Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Messenger()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [Route("SendMessage")]
        public async Task<JsonResult> SendMessage([FromBody]string message)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://localhost:44382/api/Bloop", content);            
            if (response.IsSuccessStatusCode)
            {
                string msgJsonStr = await response.Content.ReadAsStringAsync();
                return new JsonResult(JsonConvert.DeserializeObject<string>(msgJsonStr));
            }

            return new JsonResult(message);
        }
    }
}
