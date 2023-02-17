using System;
namespace ZadGitlab
{
    public class Issue
    {
        public User author { get; set; }
        public int? iid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }
}

