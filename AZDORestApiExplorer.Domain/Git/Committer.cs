using System;

namespace AZDORestApiExplorer.Domain.Git
{
    public class Committer
    {
        public DateTime date { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }
}