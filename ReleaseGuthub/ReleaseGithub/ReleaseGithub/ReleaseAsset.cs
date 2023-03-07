using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReleaseGithub
{
    public class ReleaseAsset
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string DownloadUrl { get; set; }
    }
}
