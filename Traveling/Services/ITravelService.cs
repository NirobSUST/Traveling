using Traveling.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Traveling.Services
{
    public interface ITravelService
    {
        Task<List<TravelViewModel>> NewsFeed();
        Task<TravelViewModel> GetTravelById(int id);
        //Task GetTravelById(int id, string image);
        Task Create(TravelViewModel viewModel);
        Task<bool> Delete(int id);
        Task<TravelViewModel> Update(int id);
        Task Update(int id, TravelViewModel viewModel);
    }
}
