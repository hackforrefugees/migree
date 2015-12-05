using Migree.Core.Definitions;
using System;
using System.IO;

namespace Migree.Core.Interfaces
{
    public interface IContentRepository
    {
        void PutImage(Guid userId, Stream fileStream, ImageType imageType);
        string GetImageUrl(Guid userId, ImageType imageType);
    }
}
