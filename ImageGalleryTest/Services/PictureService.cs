using ImageGalleryTest.Models;
using ImageGalleryTest.Repositories.Interfaces;
using ImageGalleryTest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGalleryTest.Services
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;

        public PictureService(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        public async Task Create(List<Picture> pictures)
        {
            await _pictureRepository.CreateBatchAsync(pictures);
        }

        public async Task RemoveAll()
        {
            await _pictureRepository.DeleteAllAsync();
        }

        public async Task<IEnumerable<Picture>> GetAll()
        {
            return await _pictureRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Picture>> FindAll(string searchTerm)
        {
            return await _pictureRepository.FindAll(searchTerm);
        }
    }
}
