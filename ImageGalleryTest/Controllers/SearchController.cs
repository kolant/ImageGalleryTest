using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageGalleryTest.Models;
using ImageGalleryTest.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageGalleryTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IPictureService _pictureService;

        public SearchController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        [HttpGet("{searchTerm}")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var pictures = await _pictureService.FindAll(searchTerm);

            return Ok(pictures);
        }
    }
}
