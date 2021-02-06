using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FleaMarket.Services
{
    public sealed class FormFileSaver : IFormFileSaver
    {
        private readonly IFileNameGenerator nameGenerator;

        public FormFileSaver(IFileNameGenerator nameGenerator)
        {
            this.nameGenerator = nameGenerator;
        }

        public string SaveFile(IFormFile file, string path)
        {
            Guard.Requires(() => file != null, new ArgumentNullException(nameof(file)));
            Guard.Requires(() => !string.IsNullOrEmpty(path), new ArgumentException($"{nameof(path)} cannot be empty or equals null"));

            string fileName = nameGenerator.Generate(file.FileName);
            file.CopyTo(new FileStream(Path.Combine(path, fileName), FileMode.Create));

            return fileName;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string path)
        {
            Guard.Requires(() => file != null, new ArgumentNullException(nameof(file)));
            Guard.Requires(() => !string.IsNullOrEmpty(path), new ArgumentException($"{nameof(path)} cannot be empty or equals null"));

            string fileName = nameGenerator.Generate(file.FileName);
            await file.CopyToAsync(new FileStream(Path.Combine(path, fileName), FileMode.Create));

            return fileName;
        }

        public IEnumerable<string> SaveFiles(IEnumerable<IFormFile> files, string path)
        {
            Guard.Requires(() => files != null, new ArgumentNullException(nameof(files)));
            Guard.Requires(() => !string.IsNullOrEmpty(path), new ArgumentException($"{nameof(path)} cannot be empty or equals null"));

            var names = new List<string>();
            foreach (var file in files)
            {
                Guard.Requires(() => file != null, new ArgumentNullException($"{nameof(files)} cannot contain null"));

                string fileName = nameGenerator.Generate(file.FileName);
                file.CopyTo(new FileStream(Path.Combine(path, fileName), FileMode.Create));
                names.Add(fileName);
            }

            return names;
        }

        public async Task<IEnumerable<string>> SaveFilesAsync(IEnumerable<IFormFile> files, string path)
        {
            Guard.Requires(() => files != null, new ArgumentNullException(nameof(files)));
            Guard.Requires(() => !string.IsNullOrEmpty(path), new ArgumentException($"{nameof(path)} cannot be empty or equals null"));

            var names = new List<string>();
            foreach (var file in files)
            {
                Guard.Requires(() => file != null, new ArgumentNullException($"{nameof(files)} cannot contain null"));

                string fileName = nameGenerator.Generate(file.FileName);
                await file.CopyToAsync(new FileStream(Path.Combine(path, fileName), FileMode.Create));
                names.Add(fileName);
            }

            return names;
        }
    }
}
