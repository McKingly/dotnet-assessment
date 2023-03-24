using System;
using System.Collections.Generic;

namespace TimeChimp.Backend.Assessment.Models
{
    public class NewsItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }  
        public DateTime PubDate { get; set; }
        public List<string> Categories { get; set; }

    }
}
