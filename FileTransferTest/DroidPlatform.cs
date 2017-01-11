using System;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Metadata;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Media;
using Plugin.Media.Abstractions;

[assembly: Xamarin.Forms.Dependency(typeof(FileTransferTest.DroidPlatform))]
namespace FileTransferTest
{
    public class DroidPlatform : IPlatform
    {
        public async Task DownloadFileAsync<T>(IMobileServiceSyncTable<T> table, MobileServiceFile file)
        {
            try
            {


                var path = await FileHelper.GetLocalFilePathAsync(file.ParentId, file.Name);
                await table.DownloadFileAsync(file, path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IMobileServiceFileDataSource> GetFileDataSource(MobileServiceFileMetadata metadata)
        {
            var filePath = await FileHelper.GetLocalFilePathAsync(metadata.ParentDataItemId, metadata.FileName);
            return new PathMobileServiceFileDataSource(filePath);
        }

        public async Task<string> TakePhotoAsync(object context)
        {
            try
            {
                var uiContext = context as Context;
                if (uiContext != null)
                {
                    var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());

                    return photo.Path;
                }
            }
            catch (TaskCanceledException)
            {
            }

            return null;
        }

        public Task<string> GetTodoFilesPathAsync()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filesPath = Path.Combine(appData, "TodoItemFiles");

            if (!Directory.Exists(filesPath))
            {
                Directory.CreateDirectory(filesPath);
            }

            return Task.FromResult(filesPath);
        }
    }
}