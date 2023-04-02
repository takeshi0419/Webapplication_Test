using SiteOfSweetsApp.Models.AccessData;
using SiteOfSweetsApp.Models.Tools;

namespace SiteOfSweetsApp.Models.DataAccess
{
    public class UserDataAccessClass
    {
        private const string SELECT_ALL     = "SELECT * FROM [aimset-sweetsdb].[dbo].[T_User]";
        private const string SELECT_USER    = "SELECT UserID,UserName,PostCode,Address FROM [aimset-sweetsdb].[dbo].[T_User]" +
                                              " WHERE UserID = '?'";
        private const string AND_PASS       = "AND Password = '?'";

        /// <summary>
        /// 指定ユーザの情報取得
        /// </summary>
        /// <param name="UserID">ユーザIＩＤ</param>
        /// <param name="Password">パスワード</param>
        /// <returns>ユーザデータ</returns>
        static public UserData? Getdata(string UserID,string Password)
        {
            DBAccessClass dba = new DBAccessClass();
            string sql = SELECT_USER.Replace("?",UserID);
            sql = sql + AND_PASS.Replace("?", Password);
            return dba.SelecttoFirst<UserData>(sql);
        }
        /// <summary>
        /// 指定ユーザの情報取得
        /// </summary>
        /// <param name="UserID">ユーザＩＤ</param>
        /// <returns>ユーザデータ</returns>
        static public UserData Getdata(string UserID)
        {
            DBAccessClass dba = new DBAccessClass();
            return dba.SelecttoFirst<UserData>(SELECT_USER.Replace("?",UserID));
        }

        /// <summary>
        /// 全ユーザの情報取得
        /// </summary>
        /// <returns>ユーザデータのリスト</returns>
        static public List<UserData> GetLust()
        {
            DBAccessClass dba = new DBAccessClass();
            return dba.Select<UserData>(SELECT_ALL);
        }
    }
}
