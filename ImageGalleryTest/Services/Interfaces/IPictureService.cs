using ImageGalleryTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGalleryTest.Services.Interfaces
{
    public interface IPictureService
    {
        Task Create(List<Picture> pictures);

        Task RemoveAll();

        Task<IEnumerable<Picture>> GetAll();

        Task<IEnumerable<Picture>> FindAll(string searchTerm);
    }
}
