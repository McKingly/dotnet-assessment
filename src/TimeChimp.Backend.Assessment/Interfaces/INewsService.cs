using System.Collections.Generic;
using System.Threading.Tasks;
using TimeChimp.Backend.Assessment.Models;

namespace TimeChimp.Backend.Assessment.Interfaces
{
    public interface INewsService
    {
        List<NewsItem> GetNews(NewsType type, string title, bool sortByAsc, string category);
    }
}
