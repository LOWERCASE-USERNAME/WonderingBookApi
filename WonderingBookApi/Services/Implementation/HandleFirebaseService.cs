
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

            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Upload to Firebase Storage
                var obj = await _storageClient.UploadObjectAsync(_bucketName, $"images/{fileName}", image.ContentType, memoryStream);

                // Set the uploaded file to be publicly readable
                var acl = new[] { PredefinedObjectAcl.PublicRead };
                await _storageClient.UpdateObjectAsync(obj, new UpdateObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead });
            }

            // Return the filename
            return $"https://storage.googleapis.com/{_bucketName}/images/{fileName}";
        }

        // Retrieve the image URL from Firebase
        public async Task<string> GetImageUrlAsync(string fileName)
        {
            try
            {
                var imageObject = await _storageClient.GetObjectAsync(_bucketName, $"images/{fileName}");

                // Create a public URL
                return $"https://storage.googleapis.com/{_bucketName}/images/{fileName}";
            }
            catch (Exception)
            {
                return null; // Handle error scenarios
            }
        }
    }
}
