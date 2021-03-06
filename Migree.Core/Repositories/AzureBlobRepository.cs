﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Migree.Core.Definitions;
using Migree.Core.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Migree.Core.Repositories
{
    public class AzureBlobRepository : IContentRepository
    {
        private const string IMAGE_CONTAINER_NAME = "migree";
        private const string DEFAULT_IMAGE_NAME = "default-profile.jpg";
        private const string JPEG_CONTENT_TYPE = "image/jpeg";
        private ISettingsServant SettingsServant { get; }

        public AzureBlobRepository(ISettingsServant settingsServant)
        {
            SettingsServant = settingsServant;
        }

        public async Task PutImageAsync(Guid userId, Stream fileStream, ImageType imageType)
        {
            fileStream.Position = 0;
            var imageBlob = GetImageBlobReference(userId, imageType);
            imageBlob.Properties.ContentType = JPEG_CONTENT_TYPE;
            imageBlob.SetProperties();
            await imageBlob.UploadFromStreamAsync(fileStream);
        }

        public string GetImageUrl(Guid? userId, ImageType imageType)
        {
            var item = GetImageBlobReference(userId, imageType);
            return item.Uri.ToString();
        }

        private CloudBlockBlob GetImageBlobReference(Guid? userId, ImageType imageType)
        {
            var storageAccount = CloudStorageAccount.Parse(SettingsServant.StorageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(IMAGE_CONTAINER_NAME);
            container.CreateIfNotExists();
            return container.GetBlockBlobReference(GetImageBlobName(userId, imageType));
        }

        private string GetImageBlobName(Guid? userId, ImageType imageType)
        {
            if (userId.HasValue)
            {
                string strId = userId.ToString();
                return $"{strId.Remove(2)}/{imageType}-{strId}.jpg";
            }

            return DEFAULT_IMAGE_NAME;
        }
    }
}
