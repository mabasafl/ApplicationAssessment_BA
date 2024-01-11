using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.Dtos.Core
{
    public class ResponseDto
    {
        public bool Success { get; set; }
        public List<string[]> Message { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
