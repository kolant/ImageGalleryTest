using AccountingTest.Infrastructure.Abstractions;
using ImageGalleryTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGalleryTest.Repositories.Interfaces
{
    public interface IPictureRepository : IRepository<Picture>
    {
        Task<List<Picture>> FindAll(string searchTerm);
    }
}
