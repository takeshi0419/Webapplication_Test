using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace SiteOfSweetsApp.Models.Tools
{
    public class DBAccessClass
    {
        public string ConnectionString { get;}

       /// <summary>
       /// コンストラクタ：connectionstringの指定が無いときは、[appsettings.json]の[ConnectDBName]項目から自動取得する
       /// </summary>
        public DBAccessClass()
        {
            var conbuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            this.ConnectionString = conbuilder.GetConnectionString("ConnectDBName");
        }
        public DBAccessClass(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        /// <summary>
        /// 抽出ＳＱＬ実行メソッド
        /// </summary>
        /// <typeparam name="T">セットするクラスの型指定</typeparam>
        /// <param name="sql">SELECT SQL</param>
        /// <returns></returns>
        public List<T> Select<T> (string sql) where T : class,new()
        {
            // 指定クラスのインスタンスリスト作成
            List<T> list = new List<T>();
            // 指定クラスのプロパティリスト取得
            List<PropertyInfo> prop = typeof(T).GetProperties().ToList();
            // SQLコマンド実行
            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand cmd = cnn.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;

                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //指定クラスのインスタンス作成
                        T t = new T();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            // null（結果無し)は次の列へ
                            if (reader.IsDBNull(i))
                            {
                                continue;
                            }
                            //プロパティの名前とSQLの検索結果の項目名が同じプロパティを探す
                            PropertyInfo? info = prop.Find(n => n.Name.Equals(reader.GetName(i))); 
                            if(info == null)
                            {
                                continue;
                            }
                            //指定クラスの同じプロパティにDBの値をセットする。
                            info.SetValue(t,reader.GetValue(i));
                        }
                        list.Add(t);
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw;
                }
                finally
                {
                    cnn.Close();
                }
            }
            return list;
        }
        /// <summary>
        /// 抽出ＳＱＬ実行メソッド
        /// </summary>
        /// <typeparam name="T">セットするクラスの型指定</typeparam>
        /// <param name="sql">SELECT SQL</param>
        /// <returns></returns>
        public T? SelecttoFirst<T>(string sql) where T : class, new()
        {
            try
            {
                return this.Select<T>(sql).First();
            }
            catch(Exception ex)
            {
                if(ex.Data.Count == 0)
                {
                    return null;
                }
                throw;
            }
        }
        /// <summary>
        /// ＩＮＳＥＲＴＳＱＬの実行メソッド
        /// </summary>
        /// <param name="sql">実行するＳＱＬ</param>
        /// <returns>True:成功、False:失敗</returns>
        public bool Insert(string sql)
        {
            try
            {
                return this.ExecNonQuery(sql);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// ＤＥＬＥＴＥＳＱＬの実行メソッド
        /// </summary>
        /// <param name="sql">実行するＳＱＬ</param>
        /// <returns>True:成功、False:失敗</returns>
        public bool Delete(string sql)
        {
            return this.ExecNonQuery(sql);
        }
        private bool ExecNonQuery(string sql)
        {
            int num = 0;
            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand cmd = cnn.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    num = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw;
                }
                finally
                {
                    cnn.Close();
                }
            }
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
