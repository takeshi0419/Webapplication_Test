namespace SiteOfSweetsApp.Models.DataAccess
{
    /// <summary>
    /// GCPのGeocodingAPIのレスポンス用データクラス（Jsonの項目名と同期）
    /// </summary>
    public class GoogleGeocodingAPIResponceData
    {
        /// <summary>
        /// 取得結果
        /// </summary>
        public List<Results_Geocode> results { get; set; }
        /// <summary>
        /// 処理ステータス
        /// </summary>
        public string status { get; set; }
    }
    public class Results_Geocode
    {
        /// <summary>
        /// 詳細住所のコンポーネント
        /// </summary>
        public List<Address_components> address_components { get; set; }
        /// <summary>
        /// 場所の住所文字列
        /// </summary>
        public string formatted_address { get; set; }
        /// <summary>
        /// 場所の座標情報
        /// </summary>
        public Geometry_Geocode geometry { get; set; }
        /// <summary>
        /// 場所の識別ID
        /// </summary>
        public string place_id { get; set; }
        /// <summary>
        /// 対象物のタイプ
        /// </summary>
        public List<string> types { get; set; }
    }
    public class Address_components
    {
        /// <summary>
        /// 指定地点の完全住所
        /// </summary>
        public string long_name { get; set; }
        /// <summary>
        /// 指定地点の省略住所
        /// </summary>
        public string short_name { get; set; }
        /// <summary>
        /// 住所コンポーネントのタイプ 詳しくはGoogleのタイプリストを参照
        /// </summary>
        public List<string> types { get; set; }
    }
    /// <summary>
    /// 位置情報クラス
    /// </summary>
    public class Geometry_Geocode
    {
        /// <summary>
        /// 場所を完全に含む形のビューポートの座標
        /// </summary>
        public Bounds_Geocode bounds { get; set; }
        /// <summary>
        /// 場所の緯度と経度
        /// </summary>
        public Location_Geocode location { get; set; }
        /// <summary>
        /// 指定した場所に関する追加データ
        /// </summary>
        public string location_type { get; set; }
        /// <summary>
        /// 場所を詳細表示するためのビューポートの座標
        /// </summary>
        public Bounds_Geocode viewport { get; set; }


    }
    /// <summary>
    /// 場所を長方形で示すビューポートの座標クラス
    /// </summary>
    public class Bounds_Geocode
    {
        /// <summary>
        /// 南西の座標
        /// </summary>
        public Location_Geocode northeast { get; set; }
        /// <summary>
        /// 北東の座標
        /// </summary>
        public Location_Geocode southwest { get; set; }

    }
    /// <summary>
    /// 指定地点の座標くらす
    /// </summary>
    public class Location_Geocode
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
