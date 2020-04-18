using Flurl;
using Flurl.Http;
using ImageGalleryTest.Models;
using ImageGalleryTest.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGalleryTest.Services
{
    public class PictureProvider : IPictureProvider
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IOptions<AppSettings> _appSettings;


        public PictureProvider(
            ITokenProvider tokenProvider,
            IOptions<AppSettings> appSettings)
        {
            _tokenProvider = tokenProvider;
            _appSettings = appSettings;
        }
        private string BaseUrl
        {
            get { return _appSettings.Value.ApiBaseUrl; }
        }

        public async Task<List<Picture>> GetAll()
        {
            var pictures = new List<Picture>();
            var imageResponses = new List<ImageResponse>();

            int page = 1;
            ImageResponse response;
            do
            {
                response = await GetPhotoPage(page);
                imageResponses.Add(response);
                page++;
            }
            while (response.HasMore);

            return imageResponses.SelectMany(x => x.Pictures).ToList();
        }

        public async Task<List<Picture>> FetchDetails(List<Picture> pictures)
        {
            foreach (var picture in pictures)
            {
                var response = await GetPhotoDetail(picture.Id);

                picture.Author = response.Author;
                picture.Camera = response.Camera;
                picture.Tags = response.Tags;
                picture.Full_Picture = response.Full_Picture;
            }

            return pictures;
        }

        private async Task<ImageResponse> GetPhotoPage(int page)
        {
            return await BaseUrl
                .AppendPathSegment("images")
                .WithOAuthBearerToken(_tokenProvider.Token)
                .WithHeaders(new { ContentType = "application/json" })
                .SetQueryParams(new { page })
                .GetJsonAsync<ImageResponse>();
        }

        private async Task<ImageDetailResponse> GetPhotoDetail(string id)
        {
            return await BaseUrl
                .AppendPathSegment($"images/{id}")
                .WithOAuthBearerToken(_tokenProvider.Token)
                .WithHeaders(new { ContentType = "application/json" })
                .GetJsonAsync<ImageDetailResponse>();
        }
    }
}
