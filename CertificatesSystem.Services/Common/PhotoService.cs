using System.Text.RegularExpressions;

namespace CertificatesSystem.Services.Common;

public static class PhotoService
{
    private const string BasePhotoPath = @"C:\Documentos";

    public static async Task<string?> SavePhotoAsFile(string photoBase64)
    {
        if (string.IsNullOrEmpty(photoBase64)) return null;
        
        var photoId = Guid.NewGuid().ToString();
        var photoPath = $@"{BasePhotoPath}\{photoId}.png";

        var base64Data = Regex.Match(photoBase64, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
        var binData = Convert.FromBase64String(base64Data);

        await File.WriteAllBytesAsync(photoPath, binData);

        return photoId;
    }

    public static async Task<string> GetPhotoAsBase64(string photoId)
    {
        var photoPath = $@"{BasePhotoPath}\{photoId}.png";

        if (string.IsNullOrEmpty(photoId))
        {
            photoPath = $@"{BasePhotoPath}\unknown.png";
        }
        
        var binData = await File.ReadAllBytesAsync(photoPath);
        var base64Data = Convert.ToBase64String(binData);
        var base64Image = $"data:image/png;base64,{base64Data}";

        return base64Image;
    }

    public static void DeletePhoto(string photoId)
    {
        var photoPath = $@"{BasePhotoPath}\{photoId}.png";

        if (string.IsNullOrEmpty(photoId)) return;
        
        File.Delete(photoPath);
    }
}