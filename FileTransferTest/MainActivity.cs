using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Files;
using Plugin.FilePicker;
using Plugin.Permissions;
using Xamarin.Forms;
using Button = Android.Widget.Button;

namespace FileTransferTest
{
    [Activity(Label = "FileTransferTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        TodoItemManager manager;
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            manager = TodoItemManager.DefaultManager;


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            manager = TodoItemManager.DefaultManager;
            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += delegate { CreateNewTodoItem(); };

            Button butRefresh = FindViewById<Button>(Resource.Id.butRefresh);
            butRefresh.Click += delegate { RefreshItems(); };


        }

        async void CreateNewTodoItem()
        {
            var item = new TodoItem { Name = "Awesome item" };
            //var dd = await CrossFilePicker.Current.PickFile();

            IPlatform mediaProvider = DependencyService.Get<IPlatform>();
            string sourceImagePath = await mediaProvider.TakePhotoAsync(Application.ApplicationContext);

            
            await manager.SaveTaskAsync(item, sourceImagePath);
        }

        async Task RefreshItems()
        {
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            var res = await manager.GetTodoItemsAsync(true);
            button.Text = res.Count.ToString();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

