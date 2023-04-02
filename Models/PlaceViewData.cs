using System.ComponentModel;

namespace SiteOfSweetsApp.Models
{
    /// <summary>
    /// 場所情報のビューモデル
    /// </summary>
    public class PlaceViewData
    {
        /// <summary>
        /// 地点の名称
        /// </summary>
        [DisplayName("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 地点の住所
        /// </summary>
        [DisplayName("住所")]
        public string Address { get; set; }
        /// <summary>
        /// GoogleMapのリンクURL
        /// </summary>
        [DisplayName("地図リンク")]
        public string Place_URL { get; set; }
        /// <summary>
        /// GoogleMapの徒歩での道順のリンクURL
        /// </summary>
        [DisplayName("徒歩道順リンク")]
        public string Direction_Working_URL { get; set; }
        /// <summary>
        /// GoogleMapの自転車での道順のリンクURL
        /// </summary>
        [DisplayName("自転車道順リンク")]
        public string Direction_Cycling_URL { get; set; }
        /// <summary>
        /// GoogleMapの車での道順のリンクURL
        /// </summary>
        [DisplayName("車道順リンク")]
        public string Direction_Driving_URL { get; set; }
        /// <summary>
        /// 写真のURL
        /// </summary>
        [DisplayName("写真")]
        public string? Photo_URL { get; set; }
        /// <summary>
        /// GoogleMapの評価点数
        /// </summary>
        [DisplayName("評価点数")]
        public float rating { get; set; }
    }
}
