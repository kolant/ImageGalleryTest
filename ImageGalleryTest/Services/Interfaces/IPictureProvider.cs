using ImageGalleryTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGalleryTest.Services.Interfaces
{
    public interface IPictureProvider
    {
        Task<List<Picture>> GetAll();

        Task<List<Picture>> FetchDetails(List<Picture> pictures);
    }
}
