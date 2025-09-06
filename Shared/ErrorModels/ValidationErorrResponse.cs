using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ValidationErorrResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<ValidationErorr> Errors { get; set; }
    }
    public class ValidationErorr
    {
        public string Field { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}

