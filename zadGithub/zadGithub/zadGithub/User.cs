using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadGithub
{
    class User
    {
        [JsonProperty("login")]
        public string Login { get; set; }
    }
}
