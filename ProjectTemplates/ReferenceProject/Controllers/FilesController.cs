using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ReferenceProject.Controllers
{
    /// <summary>
    /// An example of a controller to upload files to demonstrate the request size limit setting (https://www.talkingdotnet.com/how-to-increase-file-upload-size-asp-net-core/)
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class FilesController: ControllerBase
    {
        /// <summary>
        /// Upload a file
        /// </summary>
        /// <remarks>
        /// Play around with file size. Try to upload a file more 1048576 bytes length
        /// </remarks>
        /// <param name="file">A file to upload</param>
        [HttpPost]
        [Route("upload")]
        [RequestSizeLimit(1048576)]
        // This is not recommended
        // [DisableRequestSizeLimit]
        public Task<IActionResult> Upload(IFormFile file)
        {
            return Task.FromResult<IActionResult>(Ok($"Uploaded: {file.FileName}"));
        }
    }
}
