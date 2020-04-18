using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGalleryTest
{
    public class AppSettings
    {
        public string ApiBaseUrl { get; set; }

        public string ApiKey { get; set; }

        public string SessionTokenKey { get; set; }

        public int TokenStoringTimeInMinutes { get; set; }

        public int PictureCacheStoringTimeInSeconds { get; set; }
    }
}
