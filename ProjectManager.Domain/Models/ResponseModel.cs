using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Models
{
    public class ResponseModel<T>
    {
        public T Dados { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
