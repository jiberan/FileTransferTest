using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using FileTransferService.DataObjects;
using Microsoft.Azure.Mobile.Server.Files;
using Microsoft.Azure.Mobile.Server.Files.Controllers;

namespace FileTransferService.Controllers
{
    [RoutePrefix("tables/todoitem")]
    public class TodoItemStorageController : StorageController<TodoItem>
    {
        [HttpPost]
        [Route("{id}/StorageToken")]
        public async Task<IHttpActionResult> PostStorageTokenRequest(string id, StorageTokenRequest value)
        {
            StorageToken token = await GetStorageTokenAsync(id, value);

            return Ok(token);
        }

        [HttpGet]
        [Route("{id}/MobileServiceFiles")]
        public async Task<IHttpActionResult> GetFiles(string id)
        {
            IEnumerable<MobileServiceFile> files = await GetRecordFilesAsync(id);

            return Ok(files);
        }

        [HttpDelete]
        [Route("{id}/MobileServiceFiles/{name}")]
        public Task Delete(string id, string name)
        {
            return base.DeleteFileAsync(id, name);
        }
    }
}