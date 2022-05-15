using Data.Processing.Models;
using Data.Processing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sensitivewords.API.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensitivewords.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class BloopServiceController : ControllerBase
    {
        private readonly IBloopService _bloopService;
        public BloopServiceController(IBloopService bloopService)
        {
            _bloopService = bloopService;
        }

        [HttpGet]
        [Route("Words")]
        public async Task<JsonResult> GetSensitivewords()
        {
            return new JsonResult(await _bloopService.GetAsync());
        }

        [HttpGet]
        [Route("Word/{id}")]
        public async Task<JsonResult> GetSensitiveword(int id)
        {
            return new JsonResult(await _bloopService.GetAsync(id));
        }

        [HttpPost]
        [Route("Create")]
        public async Task<JsonResult> PostSensitiveword(Sensitiveword sensitiveword)
        {
            return new JsonResult(await _bloopService.CreateAsync(sensitiveword));
        }

        [HttpPost]
        [Route("Import")]
        public async Task<JsonResult> PostSensitivewords(List<Sensitiveword> sensitivewords)
        {
            return new JsonResult(await _bloopService.ImportAsync(sensitivewords));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<JsonResult> PutSensitiveword([FromBody]Sensitiveword sensitiveword)
        {
            return new JsonResult(await _bloopService.UpdateAsync(sensitiveword));
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<JsonResult> DeleteSensitiveword(int id)
        {
            return new JsonResult(await _bloopService.DeleteAsync(id));
        }
    }
}
