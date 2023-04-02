namespace SiteOfSweetsApp.Models.DataAccess
{
    /// <summary>
    /// GCPのPlaceAPIのレスポンス用データクラス（Jsonの項目名と同期）
    /// </summary>

    public class GooglePlaceAPIResponceData
    {
        /// <summary>
        /// 場所情報の取得結果
        /// </summary>

        public List<Results_Place> results { get; set; }
        /// <summary>
        /// 処理ステータス
        /// </summary>
        public string status { get; set; }
    }
    /// <summary>
    /// API処理結果クラス
    /// </summary>
    public class Results_Place
    {
        /// <summary>
        /// 位置情報　場所の緯度・経度とビューポート情報
        /// </summary>
        public Geometry_Place geometry { get; set; }
        /// <summary>
        /// 地図アイコンのＵＲＬ
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 地図アイコンの背景のカラーコード
        /// </summary>
        public string icon_background_color { get; set; }
        /// <summary>
        /// 不明
        /// </summary>
        public string icon_mask_base_uri { get; set; }
        /// <summary>
        /// 場所名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 場所の写真情報（最大１０）
        /// </summary>
        public List<Photos_Place> photos { get; set; }
        /// <summary>
        /// 場所の特定ＩＤ
        /// </summary>
        public string place_id { get; set; }
        /// <summary>
        /// 場所の評価（1.0～5.0）
        /// </summary>
        public float rating { get; set; }
        /// <summary>
        /// 不明　※現在つかわれていない項目
        /// </summary>
        public string reference { get; set; }
        /// <summary>
        /// 不明　※現在つかわれていない項目
        /// </summary>
        public string scope { get; set; }
        /// <summary>
        /// 指定された場所を説明する機能タイプの配列　GooglePlaceAPIのサポートタイプを参照
        /// </summary>
        public List<string> types { get; set; }
        /// <summary>
        /// 住所（大体の簡略化されたもので、通り名等は含まれない）
        /// </summary>
        public string vicinity { get; set; }

    }
    /// <summary>
    /// 位置情報クラス
    /// </summary>
    public class Geometry_Place
    {
        /// <summary>
        /// 場所の緯度と経度
        /// </summary>
        public Location_Place location { get; set; }
        /// <summary>
        /// 場所を長方形で示した地理座標
        /// </summary>
        public Bounds_Place viewport { get; set; }

    }
    /// <summary>
    /// 場所の写真情報クラス
    /// </summary>
    public class Photos_Place
    {
        /// <summary>
        /// 写真の高さ
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// 写真投稿者のGoogleアカウント情報のリンク
        /// </summary>
        public List<string> html_attributions { get; set; }
        /// <summary>
        /// 写真リクエスト用の識別コード（GooglePhoto）
        /// </summary>
        public string photo_reference { get; set; }
        /// <summary>
        /// 写真の幅
        /// </summary>
        public int width { get; set; }
    }
    /// <summary>
    /// 地理座標の長方形を示すクラス
    /// </summary>
    public class Bounds_Place
    {
        /// <summary>
        /// 南西の座標
        /// </summary>
        public Location_Place northeast { get; set; }
        /// <summary>
        /// 北東の座標
        /// </summary>
        public Location_Place southwest { get; set; }

    }
    /// <summary>
    /// 場所の緯度と経度情報クラス
    /// </summary>
    public class Location_Place
    {
        /// <summary>
        /// 緯度
        /// </summary>
        public double lat { get; set; }
        /// <summary>
        /// 経度
        /// </summary>
        public double lng { get; set; }
    }
}
