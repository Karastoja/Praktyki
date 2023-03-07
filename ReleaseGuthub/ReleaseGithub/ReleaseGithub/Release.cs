using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReleaseGithub
{
    public class Release
    {
        public List<ReleaseAsset> Assets { get; set; }
    }
}
