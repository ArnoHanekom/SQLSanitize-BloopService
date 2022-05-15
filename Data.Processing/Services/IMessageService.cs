using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Processing.Services
{
    public interface IMessageService
    {
        public Task<string> BloopAsync(string message);
    }
}
