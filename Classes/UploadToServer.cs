using Portfolio_AppRepo_UI.Models;
using Renci.SshNet;

namespace Portfolio_AppRepo_UI.Classes
{
    public class UploadToServer
    {

        public async Task UploadAsync(List<FileUploadModel> fileList, int appId)
        {
            var pwdConnectionInfo = new PasswordConnectionInfo("10.202.2.100", Settings.SharepointUsername, Settings.SharepointPassword);
            using (SftpClient client = new SftpClient(pwdConnectionInfo))
            {
                client.Connect();
                foreach (var file in fileList)
                {
                    using (FileStream stream = File.Create(Settings.FileBaseUrl))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        await file.UploadedFile.OpenReadStream().CopyToAsync(stream);
                    }
                }

                client.Disconnect();
            }
        }
    }
}
