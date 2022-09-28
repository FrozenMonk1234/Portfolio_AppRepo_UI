using Portfolio_AppRepo_UI.Models;

namespace Portfolio_AppRepo_UI.Classes
{
    public class LogWorker
    {
        public LogWorker()
        {

        }

        public void LogError(Exception? e)
        {

            LogModel logModel = new LogModel()
            {
                Date = DateTime.Now,
                InnerException = e.InnerException.Message,
                Message = e.Message,
                Source = e.StackTrace,
                Type = e.TargetSite.Name
            };

            string file = @$"C:\AppRepoLogs\Error_Log_{DateTime.Today}.txt";
            string error = $"Error [Type:{logModel.Type}, Message:{logModel.Message}, Inner:{logModel.InnerException},  Date:{logModel.Date}, Stack Trace:{logModel.Source} ].";
            File.AppendAllText(file, error);
            File.AppendAllText(file, "--end--");
        }

        //public void Test(string text)
        //{
        //    string file = @"C:\AppRepoLogs\Tester.txt";
        //    File.AppendAllText(file, text);
        //}
    }
}
