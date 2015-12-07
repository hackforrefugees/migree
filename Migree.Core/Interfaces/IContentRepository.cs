using Migree.Core.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Migree.Core.Interfaces
{
    public interface IContentRepository
    {
        /// <summary>
        /// Upload new image to storage
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fileStream"></param>
        /// <param name="imageType"></param>
        /// <returns></returns>
        Task PutImageAsync(Guid userId, Stream fileStream, ImageType imageType);

        /// <summary>
        /// Get url to image
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageType"></param>
        /// <returns></returns>
        string GetImageUrl(Guid userId, ImageType imageType);
    }
}
