using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeChimp.Backend.Assessment.Interfaces
{
    public interface IGeneralNewsService
    {
        //Task<string> GetGeneralNews(); 
        Task<List<string>> GetGeneralNews(); 
    }
}
