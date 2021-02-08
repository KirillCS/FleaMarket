using FleaMarket.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleaMarket.Services
{
    /// <summary>
    /// Service saves files received from the form
    /// </summary>
    public interface IFormFileSaver
    {

        /// <summary>
        /// Save the form file asynchronously
        /// </summary>
        /// <param name="file">The form file</param>
        /// <param name="path">The file save path</param>
        /// <returns>The name of the saving file</returns>
        Task<string> SaveFile(IFormFile file, string path);

        /// <summary>
        /// Save the collection of form files asynchronously
        /// </summary>
        /// <param name="files">The collection of form files</param>
        /// <param name="path">The files save path</param>
        /// <returns>Names of saving files</returns>
        Task<IEnumerable<string>> SaveFiles(IEnumerable<IFormFile> files, string path);
    }
}
