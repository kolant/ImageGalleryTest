using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGalleryTest.Models
{
    public class ImageResponse
    {
        public IEnumerable<Picture> Pictures { get; set; }

        public int Page { get; set; }

        public int PageCount { get; set; }

        public bool HasMore { get; set; }
    }
}
