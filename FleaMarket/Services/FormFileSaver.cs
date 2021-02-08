using FleaMarket.Extensions;
using FleaMarket.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FleaMarket.Services
{
    public sealed class FormFileSaver : IFormFileSaver
    {
        /// <inheritdoc/>
        public async Task<string> SaveFile(IFormFile file, string path)
        {
            Guard.Requires(() => file != null, new ArgumentNullException(nameof(file)));
            Guard.Requires(() => !string.IsNullOrEmpty(path), new ArgumentException($"{nameof(path)} cannot be empty or equals null"));

            string fileName = GetUniqueFileName(file.FileName);
            await file.CopyToAsync(new FileStream(Path.Combine(path, fileName), FileMode.Create));

            return fileName;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> SaveFiles(IEnumerable<IFormFile> files, string path)
        {
            Guard.Requires(() => files != null, new ArgumentNullException(nameof(files)));
            Guard.Requires(() => !string.IsNullOrEmpty(path), new ArgumentException($"{nameof(path)} cannot be empty or equals null"));

            var names = new List<string>();
            foreach (var file in files)
            {
                Guard.Requires(() => file != null, new ArgumentNullException($"{nameof(files)} cannot contain null"));

                string fileName = GetUniqueFileName(file.FileName);
                await file.CopyToAsync(new FileStream(Path.Combine(path, fileName), FileMode.Create));
                names.Add(fileName);
            }

            return names;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Image>> SaveFormImages(IFormFile cover, IEnumerable<IFormFile> images, string path)
        {
            var result = new List<Image>();
            if (cover != null)
            {
                var fileName = await SaveFile(cover, path);
                result.Add(new Image(fileName, true));
            }

            if (images.IsNullOrEmpty())
            {
                var imagesNames = await SaveFiles(images, path);
                result.AddRange(imagesNames.Select(n => new Image(n)));
            }

            return result;
        }

        private string GetUniqueFileName(string currentName) =>
            $"{Guid.NewGuid()}_{currentName}";
    }
}
