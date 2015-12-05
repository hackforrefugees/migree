using Migree.Core.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Migree.Core.Interfaces
{
    public interface IContentRepository
    {
        Task PutImageAsync(Guid userId, Stream fileStream, ImageType imageType);
        string GetImageUrl(Guid userId, ImageType imageType);
    }
}
