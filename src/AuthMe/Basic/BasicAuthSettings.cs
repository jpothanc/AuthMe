using System.Collections.Generic;

namespace AuthMe.Basic
{
    public class BasicAuthSettings
    {
        public Dictionary<string, string> Users { get; set; } = new();
    }
}
