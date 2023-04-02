using SiteOfSweetsApp.Models.Tools;
using static System.Net.WebRequestMethods;

namespace SiteOfSweetsApp.Models.DataAccess
{
    /// <summary>
    /// GoogleGeocodingAPI通信用のクラス
    /// </summary>
    public class GoogleGeocodingAccessClass
    {
        private string url { get; set; }
        private const string url_geocode = "/geocode/json?";
        private const string url_key = "key=?";
        private const string url_address = "&address=?";

        /// <summary>
        /// コンストラクタ URLはConnectionStringの"GoogleAPIURL"を参照する。
        /// </summary>
        /// <param name="apikey">APIのキー</param>
        public GoogleGeocodingAccessClass(string apikey)
        {
            var conbuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            this.url = conbuilder.GetConnectionString("GoogleAPIURL");
            this.url = this.url + url_geocode + url_key.Replace("?", apikey);

        }
        /// <summary>
        /// 位置情報の取得
        /// </summary>
        /// <param name="Address">住所</param>
        /// <returns>位置情報</returns>
        public GoogleGeocodingAPIResponceData GetData(string Address)
        {
            return APIAccessClass.GetData<GoogleGeocodingAPIResponceData>(url + url_address.Replace("?",Address)).Result;
        }
    }
}
