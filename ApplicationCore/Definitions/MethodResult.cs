using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Definitions
{
    public class MethodResult
    {
        public string Message { get; set; }
        public bool Successful { get; set; }
        public object ObjectResult { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
