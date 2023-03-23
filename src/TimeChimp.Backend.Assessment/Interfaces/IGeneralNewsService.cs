using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeChimp.Backend.Assessment.Interfaces
{
    public interface IGeneralNewsService
    {
        Task<List<string>> GetGeneralNews(string titleFilter, bool sortByTitle); 
    }
}
