namespace SiteOfSweetsApp.Models
{
    public class HomeViewData
    {
        /// <summary>
        /// アカウントデータ
        /// </summary>
        public AccountViewData accdata{ get; set; }
        /// <summary>
        /// 店舗情報
        /// </summary>
        public List<PlaceViewData> placedata { get; set; }
    }
}
