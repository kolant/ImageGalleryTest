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
    public class ImagesController : ControllerBase
    {
        private readonly IPictureProvider _pictureProvider;
        private readonly IPictureService _pictureService;

        public ImagesController(
            IPictureProvider pictureProvider,
            IPictureService pictureService)
        {
            _pictureProvider = pictureProvider;
            _pictureService = pictureService;
        }

        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            var storedPictures = await _pictureService.GetAll();

            return Ok(storedPictures);
        }

        [HttpGet("load")]
        public async Task<IActionResult> LoadPicturesFromAPI()
        {
            // get all from api
            var pictures = await _pictureProvider.GetAll();
            pictures = await _pictureProvider.FetchDetails(pictures);

            // clear and save
            await _pictureService.RemoveAll();
            await _pictureService.Create(pictures);

            return Ok();
        }
    }
}