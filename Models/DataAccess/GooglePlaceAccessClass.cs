using SiteOfSweetsApp.Models.Tools;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace SiteOfSweetsApp.Models.DataAccess
{
    /// <summary>
    /// GooglePlaceAPI通信用のクラス
    /// </summary>
    public class GooglePlaceAccessClass
    {
        private string url { get; set; }
        private string apikey { get; set; }
        private const string url_key = "key=?";
        private const string url_Nearbysearch = "/place/nearbysearch/json?";
        private const string url_location = "&location=?";
        private const string url_radius = "&radius=?";
        private const string url_language = "&language=ja";
        private const string url_keyword = "&keyword=?";

        private const string url_photo = "/place/photo?";
        private const string url_photo_reference = "&photo_reference=?";
        private const string url_maxwidth = "&maxwidth=?";
        private const string url_maxheight = "&maxheight=?";

        /// <summary>
        /// コンストラクタ URLはConnectionStringの"GoogleAPIURL"を参照する。
        /// </summary>
        /// <param name="apikey">APIのキー</param>
        public GooglePlaceAccessClass(string apikey)
        {
            var conbuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            this.url = conbuilder.GetConnectionString("GoogleAPIURL");
            this.apikey = apikey;
        }
        /// <summary>
        /// 周辺検索
        /// </summary>
        /// <param name="lat">緯度</param>
        /// <param name="lng">経度</param>
        /// <param name="radius">範囲（m)</param>
        /// <param name="keyword">検索キーワード</param>
        /// <returns>周辺検索結果</returns>
        public GooglePlaceAPIResponceData GetNearbysearchData(double lat,double lng,int radius,string keyword)
        {
            string urlstr = this.url + url_Nearbysearch + url_key.Replace("?", apikey) +
                                                          url_location.Replace("?", lat.ToString() + "," + lng.ToString() + 
                                                          url_radius.Replace("?",radius.ToString()) +
                                                          url_language + url_keyword.Replace("?",keyword));
            return APIAccessClass.GetData<GooglePlaceAPIResponceData>(urlstr).Result;
        }
        /// <summary>
        /// 写真取得
        /// </summary>
        /// <param name="reference">写真の参照情報</param>
        /// <param name="maxwidth">最大幅</param>
        /// <param name="maxheight">最大高</param>
        /// <returns>写真データのURL</returns>
        public string? GetPhoto(string reference,int maxwidth,int maxheight)
        {
            if (reference == null) {
                return null;
            }
            return this.url + url_photo + url_key.Replace("?", apikey) +
                                                   url_photo_reference.Replace("?", reference) +
                                                   url_maxwidth.Replace("?", maxwidth.ToString()) +
                                                   url_maxheight.Replace("?", maxheight.ToString());

        }
    }
}
