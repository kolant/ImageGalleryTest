using ImageGalleryTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGalleryTest.Services.Interfaces
{
    public interface ITokenProvider
    {
        string Token { get; }
    }
}
