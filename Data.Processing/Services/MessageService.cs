using Data.Processing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data.Processing.Services
{
    public class MessageService : IMessageService
    {
        private readonly IBloopService _bloopService;

        public MessageService(IBloopService bloopService)
        {
            _bloopService = bloopService;
        }

        public async Task<string> BloopAsync(string message)
        {
            var words = await WordsAsync();

            words.ForEach(w => 
            {
                message = Regex.Replace(
                    message,
                    $@"\b{w.WordDefinition}\b",
                    string.Empty.PadLeft(w.WordDefinition.Length, '*'),
                    RegexOptions.IgnoreCase);                
            });

            return message;
        }

        private async Task<List<Sensitiveword>> WordsAsync()
        {
            var response = await _bloopService.GetAsync();
            if(response.Success)
            {
                return response.ResponseObject.ToList();
            }

            return null;
        }
    }
}
