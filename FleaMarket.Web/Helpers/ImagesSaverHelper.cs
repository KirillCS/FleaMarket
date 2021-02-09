using FleaMarket.Business.Extensions;
using FleaMarket.Business.Services;
using FleaMarket.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleaMarket.Helpers
{
    public static class ImagesSaverHelper
    {
        public static async Task<IEnumerable<Image>> SaveFormImages(IFormFile cover, IEnumerable<IFormFile> images, string path)
        {
            var saver = new FormFileSaver();
            var result = new List<Image>();
            if (cover != null)
            {
                var fileName = await saver.SaveFile(cover, path);
                result.Add(new Image(fileName, true));
            }

            if (images.IsNullOrEmpty())
            {
                var imagesNames = await saver.SaveFiles(images, path);
                result.AddRange(imagesNames.Select(n => new Image(n)));
            }

            return result;
        }
    }
}
