namespace WonderingBookApi.Services
{
    public interface IHandleFirebaseService
    {
        Task<string> UploadImageAsync(IFormFile image, Guid fileId);

        Task<string> GetImageUrlAsync(string fileName);

        Task DeleteImageAsync(IEnumerable<string> imageUrls);
    }
}
