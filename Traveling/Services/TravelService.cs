using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Traveling.Data;
using Traveling.Models;

namespace Traveling.Services
{
    public class TravelService : ITravelService
    {
        private readonly ApplicationDbContext _context = null;
        private readonly IWebHostEnvironment _env = null;

        public TravelService(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public async Task Create(TravelViewModel viewModel)
        {
            TravelModel model = new TravelModel();
            model.Title = viewModel.Title;
            model.Location = viewModel.Location;
            model.Bus = viewModel.Bus;
            model.ApproxCost = viewModel.ApproxCost;
            model.Experience = viewModel.Experience;

            if (viewModel.ProfileImage != null)
            {
                string folder = "upload/";
                folder += Guid.NewGuid().ToString() + "_" + viewModel.ProfileImage.FileName;
                model.ProfileImageUrl = folder;
                string serverFolder = Path.Combine(_env.WebRootPath, folder);
                await viewModel.ProfileImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                var stream = new MemoryStream();
                viewModel.ProfileImage.CopyTo(stream);
            }
            await _context.Travels.AddAsync(model);
            await _context.SaveChangesAsync();
        }
        public async Task<TravelViewModel> GetTravelById(int id)
        {
            var model = _context.Travels.Where(x => x.Id == id).FirstOrDefault();
            TravelViewModel viewModel = new TravelViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                Location = model.Location,
                Bus = model.Bus,
                ApproxCost = model.ApproxCost,
                Experience = model.Experience,
                ProfileImageUrl = model.ProfileImageUrl
            };
            return viewModel;
        }


        public async Task<List<TravelViewModel>> NewsFeed()
        {
            var model = _context.Travels.ToList().OrderBy(x => x.Id);
            List<TravelViewModel> travels = new List<TravelViewModel>();

            foreach (var travel in model)
            {
                TravelViewModel viewModel = new TravelViewModel()
                {
                    Id = travel.Id,
                    Title = travel.Title,
                    Location = travel.Location,
                    Bus = travel.Bus,
                    ApproxCost = travel.ApproxCost,
                    Experience = travel.Experience,
                    ProfileImageUrl = travel.ProfileImageUrl
                };
                travels.Add(viewModel);
            }
            return travels;
        }



        public async Task<bool> Delete(int id)
        {
            var model = _context.Travels.Find(id);

            if (model != null)
            {
                _context.Travels.Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }



        public async Task<TravelViewModel> Update(int id)
        {
            var model = _context.Travels.Where(x => x.Id == id).FirstOrDefault();
            TravelViewModel viewModel = new TravelViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                Location = model.Location,
                Bus = model.Bus,
                ApproxCost = model.ApproxCost,
                Experience = model.Experience
            };

            return viewModel;
        }



        public async Task Update(int id, TravelViewModel viewModel)
        {
            var model = _context.Travels.Find(id);


            model.Id = viewModel.Id;
            model.Title = viewModel.Title;
            model.Location = viewModel.Location;
            model.Bus = viewModel.Bus;
            model.ApproxCost = viewModel.ApproxCost;
            model.Experience = viewModel.Experience;

            if (viewModel.ProfileImage != null)
            {
                string folder = "uploads/";
                folder += Guid.NewGuid().ToString() + "_" + viewModel.ProfileImage.FileName;
                viewModel.ProfileImageUrl = folder;
                string serverFolder = Path.Combine(_env.WebRootPath, folder);
                await viewModel.ProfileImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                model.ProfileImageUrl = viewModel.ProfileImageUrl;

                var stream = new MemoryStream();
                await viewModel.ProfileImage.CopyToAsync(stream);

            }
            _context.Travels.Update(model);
            await _context.SaveChangesAsync();
        }

    }
}
