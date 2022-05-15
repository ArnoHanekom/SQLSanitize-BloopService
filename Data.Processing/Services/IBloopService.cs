using Data.Processing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Processing.Services
{
    public interface IBloopService
    {
        public Task<BloopResponse<IEnumerable<Sensitiveword>>> GetAsync();
        public Task<BloopResponse<Sensitiveword>> GetAsync(int id);
        public Task<BloopResponse<Sensitiveword>> CreateAsync(Sensitiveword word);
        public Task<BloopResponse<IEnumerable<Sensitiveword>>> ImportAsync(List<Sensitiveword> words);
        public Task<BloopResponse<Sensitiveword>> UpdateAsync(Sensitiveword word);
        public Task<BloopResponse<Sensitiveword>> DeleteAsync(int id);
    }
}
