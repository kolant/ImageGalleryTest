using AccountingTest.Infrastructure.Implementations;
using ImageGalleryTest.Models;
using ImageGalleryTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGalleryTest.Repositories
{
    public class PictureRepository : Repository<Picture>, IPictureRepository
    {
        public PictureRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Picture>> FindAll(string searchTerm)
        {
            return await entities
                .Where(x => x.Author.Contains(searchTerm) ||
                    x.Camera.Contains(searchTerm) ||
                    x.Tags.Contains(searchTerm) ||
                    x.Cropped_Picture.Contains(searchTerm) ||
                    x.Full_Picture.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
