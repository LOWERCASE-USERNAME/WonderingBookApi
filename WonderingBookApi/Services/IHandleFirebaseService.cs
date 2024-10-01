namespace WonderingBookApi.Services
{
    public interface IHandleFirebaseService
    {
        Task<string> UploadImageAsync(IFormFile image, Guid fileId);
    }
}
