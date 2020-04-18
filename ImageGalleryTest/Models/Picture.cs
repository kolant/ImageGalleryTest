using AccountingTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGalleryTest.Models
{
    public class Picture : BaseEntity
    {
        public string Cropped_Picture { get; set; }
        public string Author { get; set; }
        public string Camera { get; set; }
        public string Tags { get; set; }
        public string Full_Picture { get; set; }
    }
}
