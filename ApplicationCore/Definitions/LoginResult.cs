using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Definitions
{
    public class LoginResult
    {
        //[JsonProperty("Id")]
        public string Uid { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpires { get; set; }
        public DateTime? LastAccess { get; set; }
    }
}
