using Migree.Core.Definitions;
using Migree.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Migree.ApiTests.Mocks
{
    public class MockContentRepository : IContentRepository
    {
        private HashSet<Guid> MockModels { get; } = new HashSet<Guid>();

        public string GetImageUrl(Guid? userId, ImageType imageType)
        {            
            return $"{(userId.HasValue && MockModels.Contains(userId.Value) ? userId.Value.ToString() : "0")}-{imageType.ToString()}.jpg";
        }

        public Task PutImageAsync(Guid userId, Stream fileStream, ImageType imageType)
        {
            MockModels.Add(userId);
            return Task.Delay(1);
        }
    }
}
