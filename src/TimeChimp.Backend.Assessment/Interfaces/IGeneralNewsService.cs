using System.Collections.Generic;
using System.Threading.Tasks;
using TimeChimp.Backend.Assessment.Models;

namespace TimeChimp.Backend.Assessment.Interfaces
{
    public interface IGeneralNewsService
    {
        Task<List<NewsItem>> GetGeneralNews(string title, bool sortByAsc);
        Task<List<NewsItem>> GetGeneralNewsByCategory(string title, bool sortByAsc, string category);

    }
}
