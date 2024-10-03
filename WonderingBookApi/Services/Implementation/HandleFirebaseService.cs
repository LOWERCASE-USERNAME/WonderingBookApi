
using Google.Cloud.Storage.V1;

namespace WonderingBookApi.Services.Implementation
{
    public class HandleFirebaseService : IHandleFirebaseService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName = "exe201-8a294.appspot.com";

        public HandleFirebaseService(StorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<string> UploadImageAsync(IFormFile image, Guid fileId)
        {
            if (image == null || image.Length == 0 || fileId == null )
            {
                return null;
            }

            string fileName = $"{fileId}_{image.FileName}";

            // Save the file to Firebase Storage
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Upload to Firebase Storage
                await _storageClient.UploadObjectAsync(_bucketName, $"images/{fileName}", null, memoryStream);
            }

            // Return the filename
            return GetImageUrl(fileName);
        }

        public string GetImageUrl(string fileName)
        {
            string baseUrl = $"https://firebasestorage.googleapis.com/v0/b/{_bucketName}/o/";
            string encodedFileName = Uri.EscapeDataString($"images/{fileName}");
            string fullUrl = $"{baseUrl}{encodedFileName}?alt=media";
            return fullUrl;
        }
    }
}
