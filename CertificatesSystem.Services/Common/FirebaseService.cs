using Firebase.Auth;
using Firebase.Storage;

namespace CertificatesSystem.Services.Common
{
    public static class FirebaseService
    {
        private const string ApiKey = "";
        private const string Bucket = "";
        private const string AuthEmail = "";
        private const string AuthPassword = "";

        public static async Task UploadFile(MemoryStream fileStream, string fileName)
        {
            // Authentication
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            
            var _ = await new FirebaseStorage(Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken),
                    ThrowOnCancel = true,
                })
                .Child(fileName)
                .PutAsync(fileStream);
        }
        
        public static async Task<string> GetFileDownloadUrl(string fileName)
        {
            // Authentication
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
        
            var url = await new FirebaseStorage(Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken),
                    ThrowOnCancel = true,
                })
                .Child(fileName)
                .GetDownloadUrlAsync();

            return url;
        }
        
        public static async Task DeleteFile(string fileName)
        {
            // Authentication
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            await new FirebaseStorage(Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken),
                    ThrowOnCancel = true,
                })
                .Child(fileName)
                .DeleteAsync();
        }
    }
}