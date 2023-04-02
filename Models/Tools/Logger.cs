using SiteOfSweetsApp.Models.Tools.Code;

namespace SiteOfSweetsApp.Models.Tools
{
    public class Logger
    {
        private static string sql = "INSERT INTO [aimset-sweetsdb].[dbo].[T_Log] ( [SystemID],[Date],[UserID],[LogType],[LogMessage]) VALUES" +
                             "('{SystemID}',getdate() ,'{UserID}','{LogType}','{LogMessage}')";
        private string SystemID;
        private string UserID;
        public Logger(string SystemID)
        {
            this.SystemID = SystemID;
            this.UserID = "";
        }
        /// <summary>
        /// エラーログ出力
        /// </summary>
        /// <param name="ErrMessage">エラーメッセージ</param>
        public void ErrLog(string ErrMessage)
        {
            WriteLog(LogType.Error, ErrMessage);

        }
        /// <summary>
        /// エラーログ出力
        /// </summary>
        /// <param name="UserID">システム利用者のID</param>
        /// <param name="ErrMessage">エラーメッセージ</param>
        public void ErrLog(string ErrMessage,string UserID)
        {
            this.UserID = UserID;
            WriteLog(LogType.Error, ErrMessage);
        }
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="Message">ログメッセージ</param>
        public void Log(string Message)
        {
            WriteLog(LogType.Success,Message);
        }
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="UserID">システム利用者のID</param>
        /// <param name="Message">ログメッセージ</param>
        public void Log(string Message,string UserID)
        {
            this.UserID = UserID;
            WriteLog(LogType.Success, Message);
        }
        /// <summary>
        /// ログ書き込み
        /// </summary>
        /// <param name="LogType">ログ種類</param>
        /// <param name="Message">ログメッセージ</param>
        private void WriteLog(string LogType,string Message)
        {
            DBAccessClass dba = new DBAccessClass();
            dba.Insert(sql.Replace("{SystemID}",SystemID).Replace("{UserID}", UserID).Replace("{LogType}", LogType).Replace("{LogMessage}", Message));
        }

    }
}
