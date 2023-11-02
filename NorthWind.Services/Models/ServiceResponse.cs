using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Services.Models
{
    public class ServiceResponse<T>
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        public T Data { get; set; }
    }
}
