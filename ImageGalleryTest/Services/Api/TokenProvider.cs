using Flurl;
using Flurl.Http;
using ImageGalleryTest.Extensions;
using ImageGalleryTest.Models;
using ImageGalleryTest.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ImageGalleryTest.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly ILogger<TokenProvider> _logger;
        private readonly IOptions<AppSettings> _appSettings;

        public TokenProvider(
            IHttpContextAccessor httpContextAccessor,
            ILogger<TokenProvider> logger,
            IOptions<AppSettings> appSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _logger = logger;
            _appSettings = appSettings;
        }

        private string BaseUrl
        {
            get { return _appSettings.Value.ApiBaseUrl; }
        }

        private string SessionTokenKey
        {
            get { return _appSettings.Value.SessionTokenKey; }
        }

        private string ApiKey
        {
            get { return _appSettings.Value.ApiKey; }
        }


        public string Token
        {
            get
            {
                var token = _session.GetComplexData<string>($"{SessionTokenKey}{ApiKey}");
                return !string.IsNullOrEmpty(token) ? token : GetToken().Result;
            }
        }

        private async Task<string> GetToken()
        {
            try
            {
                var response = await BaseUrl
                    .AppendPathSegment("auth")
                    .PostJsonAsync(new
                    {
                        apiKey = ApiKey
                    })
                    .ReceiveJson<AuthResponse>();

                if (response.Auth)
                {
                    _session.SetComplexData($"{SessionTokenKey}{ApiKey}", response.Token);
                    return response.Token;
                }
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == HttpStatusCode.Unauthorized)
                {
                    _logger.LogError("Wasn't able to get token. The api key is wrong.");
                }
            }

            return null;
        }
    }
}
