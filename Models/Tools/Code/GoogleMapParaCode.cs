namespace SiteOfSweetsApp.Models.Tools.Code
{
    /// <summary>
    /// GoogleMapのURLパラメータコード
    /// </summary>
    public class GoogleMapParaCode
    {
        /// <summary>
        /// 探索用　地点名称（名称または、その場所のロケーション）
        /// </summary>
        public const string SearchPara_query = "&query=";
        /// <summary>
        /// 探索用　地点ID
        /// </summary>
        public const string SearchPara_query_place_id = "&query_place_id=";
        /// <summary>
        /// 道順用　スタート地点名称（名称または、その場所のロケーション）
        /// </summary>
        public const string DirectionPara_origin = "&origin=";
        /// <summary>
        /// 道順用　目的地点名称（名称または、その場所のロケーション）
        /// </summary>
        public const string DirectionPara_destination = "&destination=";
        /// <summary>
        /// 道順用　目的地までの手段
        /// </summary>
        public const string DirectionPara_travelmode = "&travelmode=";
    }
}
