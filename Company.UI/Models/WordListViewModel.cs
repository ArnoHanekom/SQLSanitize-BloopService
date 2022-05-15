using Data.Processing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.UI.Models
{
    public class WordListViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } 
        public List<Sensitiveword> WordList { get; set; }
    }
}
