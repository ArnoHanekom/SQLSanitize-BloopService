using Data.Processing.Models;
using Data.Processing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensitivewords.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloopController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public BloopController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]        
        public async Task<JsonResult> PostAsync([FromBody]string message)
        {
            return new JsonResult(await _messageService.BloopAsync(message));
        }
    }
}
