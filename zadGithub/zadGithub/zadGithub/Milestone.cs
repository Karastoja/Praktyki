using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadGithub
{
    class Milestone
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
