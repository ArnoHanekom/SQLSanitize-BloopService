using Company.UI.Models;
using Data.Processing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Company.UI.Controllers
{
    public class WordController : Controller
    {
        private HttpClient client = new HttpClient();
        private const string apiPath = "https://localhost:44382/api/BloopService";

        private Func<BloopResponse<IEnumerable<Sensitiveword>>, WordListViewModel> ListToListViewModel = (listResponse) =>
        {
            return new WordListViewModel()
            {
                Success = listResponse.Success,
                Message = listResponse.Message,
                WordList = listResponse.ResponseObject.ToList()
            };
        };
        private Func<BloopResponse<Sensitiveword>, BloopResponse<IEnumerable<Sensitiveword>>, WordListViewModel> SingleToListViewModel = (singleResponse, listResponse) =>
        {
            return new WordListViewModel()
            {
                Success = singleResponse.Success,
                Message = singleResponse.Message,
                WordList = listResponse.ResponseObject.ToList()
            };
        };
        private Func<BloopResponse<IEnumerable<Sensitiveword>>, BloopResponse<IEnumerable<Sensitiveword>>, WordListViewModel> ImportToListViewModel = (importResponse, listResponse) => {
            return new WordListViewModel()
            {
                Success = importResponse.Success,
                Message = importResponse.Message,
                WordList = listResponse.ResponseObject.ToList()
            };
        };

        public WordController()
        {
            client.DefaultRequestHeaders.Add("X-API-Key", "09281bea-0dbc-448d-9e12-6cc39c308872");
        }

        public IActionResult Index()
        {
            WordListViewModel model = Task.Run(async () => await ConvertIndexToModelAsync()).Result;
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Sensitiveword word)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(word), Encoding.UTF8, "application/json");
            WordListViewModel model = Task.Run(async () => await ConvertUpdateToModelAsync(content)).Result;
            return View("Index", model);
        }        

        [HttpPost]
        public IActionResult Create(Sensitiveword word)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(word), Encoding.UTF8, "application/json");
            WordListViewModel model = Task.Run(async () => await ConvertCreateToModelAsync(content)).Result;
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            WordListViewModel model = Task.Run(async () => await ConvertDeleteToModelAsync(id)).Result;
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult Import(IFormFile importFile)
        {
            List<Sensitiveword> importWords = Task.Run(async () => await LoadImportListAsync(importFile)).Result;
            StringContent content = new StringContent(JsonConvert.SerializeObject(importWords), Encoding.UTF8, "application/json");
            WordListViewModel model = Task.Run(async () => await ConvertImportToModelAsync(content)).Result;
            return View("Index", model);
        }

        private async Task<List<Sensitiveword>> LoadImportListAsync(IFormFile importFile)
        {
            List<Sensitiveword> importWords = new List<Sensitiveword>();
            if (importFile.Length > 0)
            {
                string tmpPath = Path.GetTempFileName();
                using (Stream stream = System.IO.File.Create(tmpPath))
                {
                    await importFile.CopyToAsync(stream);
                }
                using (StreamReader sr = new StreamReader(tmpPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = await sr.ReadLineAsync();
                        line = Regex.Replace(line, "[^a-zA-Z0-9]", string.Empty, RegexOptions.IgnoreCase);
                        if (line.Length > 0)
                            importWords.Add(new Sensitiveword() { WordDefinition = line, Id = 0 });
                    }
                }
                System.IO.File.Delete(tmpPath);
            }
            return importWords;
        }

        private async Task<WordListViewModel> ConvertIndexToModelAsync()
        {
            return ListToListViewModel(await ListBloopResponseAsync());
        }
        private async Task<WordListViewModel> ConvertUpdateToModelAsync(StringContent content)
        {
            HttpResponseMessage response = await client.PutAsync($"{apiPath}/Update", content);
            return SingleToListViewModel(await SingleBloopResponseAsync(response), await ListBloopResponseAsync());
        }
        private async Task<WordListViewModel> ConvertCreateToModelAsync(StringContent content)
        {
            HttpResponseMessage response = await client.PostAsync($"{apiPath}/Create", content);
            return SingleToListViewModel(await SingleBloopResponseAsync(response), await ListBloopResponseAsync());
        }
        private async Task<WordListViewModel> ConvertDeleteToModelAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{apiPath}/Delete/{id}");
            return SingleToListViewModel(await SingleBloopResponseAsync(response), await ListBloopResponseAsync());
        }
        private async Task<WordListViewModel> ConvertImportToModelAsync(StringContent content)
        {
            HttpResponseMessage response = await client.PostAsync($"{apiPath}/Import", content);
            return ImportToListViewModel(await ImportBloopResponseAsync(response), await ListBloopResponseAsync());
        }

        private async Task<BloopResponse<IEnumerable<Sensitiveword>>> ListBloopResponseAsync()
        {            
            HttpResponseMessage response = await client.GetAsync($"{apiPath}/Words");
            if (response.IsSuccessStatusCode)
            {
                string msgJsonStr = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BloopResponse<IEnumerable<Sensitiveword>>>(msgJsonStr);
            }

            return null;
        }
        private async Task<BloopResponse<Sensitiveword>> SingleBloopResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string msgJsonStr = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BloopResponse<Sensitiveword>>(msgJsonStr);
            }

            return null;
        }
        private async Task<BloopResponse<IEnumerable<Sensitiveword>>> ImportBloopResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string msgJsonStr = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BloopResponse<IEnumerable<Sensitiveword>>>(msgJsonStr);
            }

            return null;
        }
    }
}
