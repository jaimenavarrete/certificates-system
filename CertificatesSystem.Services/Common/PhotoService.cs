using System.Text.RegularExpressions;

namespace CertificatesSystem.Services.Common
{
    public static class PhotoService
    {
        public static async Task<string?> SavePhotoAsFile(string photoBase64)
        {
            try
            {
                if (string.IsNullOrEmpty(photoBase64)) return null;

                var photoId = Guid.NewGuid().ToString();
                var photoName = $@"{photoId}.png";

                var base64Data = Regex.Match(photoBase64, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                var binData = Convert.FromBase64String(base64Data);

                var photoStream = new MemoryStream(binData);

                await FirebaseService.UploadFile(photoStream, photoName);

                return photoId;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<string?> GetPhotoAsBase64(string? photoId)
        {
            var photoName = $@"{photoId}.png";

            if (string.IsNullOrEmpty(photoId))
                return null;

            // Get image and transforms to Base64
            using var client = new HttpClient();
            var url = await FirebaseService.GetFileDownloadUrl(photoName);
            var bytes = await client.GetByteArrayAsync(url);
            var base64Data = Convert.ToBase64String(bytes);
            var base64Image = $"data:image/png;base64,{base64Data}";

            return base64Image;
        }

        public static async void DeletePhoto(string? photoId)
        {
            var photoName = $@"{photoId}.png";

            if (string.IsNullOrEmpty(photoId)) 
                return;

            await FirebaseService.DeleteFile(photoName);
        }
    }
}