using System.Collections.Generic;
using Traveling.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Traveling.Services;

namespace Traveling.Controllers
{
    public class TravelController : Controller
    {
        private readonly ITravelService _service = null;
        public TravelController(ITravelService service)
        {
            _service = service;
        }
        [Route("create")]
        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(TravelViewModel viewModel)
        {
            await _service.Create(viewModel);

            return RedirectToAction("NewsFeed");
        }
        [Route("travel-details/{id}")]
        public async Task<IActionResult> GetTravelById(int id)
        {

            TravelViewModel viewModel = await _service.GetTravelById(id);
            ViewBag.data = viewModel; ;
            return View();
        }


        [Route("news-feed")]
        public async Task<IActionResult> NewsFeed()
        {
            List<TravelViewModel> travels = await _service.NewsFeed();
            return await Task.FromResult(View(travels));

        }
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _service.Delete(id);
            if (result)
            {
                return RedirectToAction("NewsFeed");
            }
            else
            {
                return RedirectToAction("GetTravelById", id);
            }
        }

        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var viewModel = await _service.Update(id);
            return await Task.FromResult(View(viewModel));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, TravelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _service.Update(id, viewModel);

                return RedirectToAction("NewsFeed");
            }
            return await Task.FromResult(View());
        }
    }
}
