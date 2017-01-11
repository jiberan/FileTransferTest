using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Metadata;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Xamarin.Forms;

namespace FileTransferTest
{
    public class TodoItemFileSyncHandler : IFileSyncHandler
    {
        private readonly TodoItemManager todoItemManager;

        public TodoItemFileSyncHandler(TodoItemManager itemManager)
        {
            this.todoItemManager = itemManager;
        }

        public Task<IMobileServiceFileDataSource> GetDataSource(MobileServiceFileMetadata metadata)
        {
            IPlatform platform = DependencyService.Get<IPlatform>();
            return platform.GetFileDataSource(metadata);
        }

        public async Task ProcessFileSynchronizationAction(MobileServiceFile file, FileSynchronizationAction action)
        {
            if (action == FileSynchronizationAction.Delete)
            {
                await FileHelper.DeleteLocalFileAsync(file);
            }
            else // Create or update. We're aggressively downloading all files.
            {
                await this.todoItemManager.DownloadFileAsync(file);
            }
        }
    }

    public interface IPlatform
    {
        Task<string> GetTodoFilesPathAsync();

        Task<IMobileServiceFileDataSource> GetFileDataSource(MobileServiceFileMetadata metadata);

        Task<string> TakePhotoAsync(object context);

        Task DownloadFileAsync<T>(IMobileServiceSyncTable<T> table, MobileServiceFile file);
    }
}