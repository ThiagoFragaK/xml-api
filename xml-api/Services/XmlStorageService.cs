using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
namespace xml_api.Services
{
    public class XmlStorageService
    {
        private readonly string _connectionString;
        private readonly string _containerName;

        public XmlStorageService(IConfiguration config)
        {
            _connectionString = config["AzureStorage:ConnectionString"];
            _containerName = config["AzureStorage:ContainerName"];
        }

        public async Task<string> UploadToAzureStorageAsync(Stream fileStream, string fileName)
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            await blobContainerClient.CreateIfNotExistsAsync();

            var blobClient = blobContainerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);

            return blobClient.Uri.ToString();
        }
    }
}
