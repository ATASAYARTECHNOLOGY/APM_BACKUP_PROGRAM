using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SideNavSample
{
    public class GoogleDrive
    {
        static string[] Scopes = new[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile };
        static string ApplicationName = "SqlBackup"
;
        static DriveService service;
        public bool DriveLogin()
        {
            UserCredential credential;
            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);

                // string credPath = System.IO.Directory.GetParent(@".\\").FullName;

                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                   new[] { DriveService.Scope.Drive },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;

            }
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            service.HttpClient.Timeout = TimeSpan.FromMinutes(100);
            return true;
        }


        public bool CreateDirectory(string folderName)
        {


            var body = new Google.Apis.Drive.v3.Data.File();
            body.Name = folderName;
            body.MimeType = "application/vnd.google-apps.folder";
            try
            {
                var request = service.Files.Create(body);
                request.Fields = "id";
                var _FF = request.Execute();
            }
            catch (Exception e)
            {
                MessageBox.Show("Hata Oluştu. : " + e.Message);
            }
            return true;
        }

        public bool UploadFiles(string durum, string zipKonum, string dosya, string folderId)
        {
            string FullFileName = zipKonum;
            string FileName = durum + ":" + dosya;
            if (System.IO.File.Exists(FullFileName))
            {
                var fileMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = FileName,
                    Parents = new List<string>
                    {
                        folderId
                    }
                };
                Google.Apis.Drive.v3.FilesResource.CreateMediaUpload request;
                using (var stream = new System.IO.FileStream(FullFileName, FileMode.Open))
                {
                    request = service.Files.Create(fileMetaData, stream, "zip/zip");
                    request.Fields = "id";
                    request.SupportsTeamDrives = true;

                   
                    request.Upload();
                    LogWriter.Write("Send File GoogleDrive");
                }
                var fileId = request.ResponseBody;
            }
            return true;
        }
        public void DeleteFiles(string fileId)
        {
            FilesResource.DeleteRequest deleteRequest = service.Files.Delete(fileId);
            deleteRequest.Execute();
        }
        public IList<Google.Apis.Drive.v3.Data.File> GetFiles()
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 100;
            listRequest.Fields = "nextPageToken, files(id, name)";

            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;

            return files;
        }





    }
}
