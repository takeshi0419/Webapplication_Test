using SiteOfSweetsApp.Models.AccessData;
using SiteOfSweetsApp.Models.DataAccess;
using SiteOfSweetsApp.Models.Tools;
using SiteOfSweetsApp.Models.Tools.Code;
using System.Reflection.Metadata.Ecma335;

namespace SiteOfSweetsApp.Models
{
    class HomeModel
    {
        /// <summary>
        /// アカウント存在チェック
        /// </summary>
        /// <returns>成否</returns>
        public static bool  AccountCheck(string UserID,string Password)
        {
            if(UserID == null || Password == null)
            {
                
                return false;
            }


            if(UserDataAccessClass.Getdata(UserID, Password) == null)
            {
                return false;
            }

            return true;
        }

        private UserData user;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HomeModel(string UserID)
        {
            this.user = UserDataAccessClass.Getdata(UserID);
        }

        public HomeViewData GetViewData(int Radius, string Keyword, int widht, int height)
        {
            return this.GetViewData(Radius, Keyword, widht, height, user.Address);
        }
        public HomeViewData GetViewData(int Radius,string Keyword,int widht,int height,string Address)
        {
            // アカウント部分のビューデータ作成
            HomeViewData viewData = new HomeViewData();
            viewData.accdata = new AccountViewData();
            viewData.accdata.Name = this.user.UserName;

            // 場所情報のビューデータ作成
            viewData.placedata = new List<PlaceViewData>();

            var conbuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string apiKye = conbuilder.GetConnectionString("GoogleAPIKey");

            // 指定住所のロケーション取得
            GoogleGeocodingAccessClass accGeo = new GoogleGeocodingAccessClass(apiKye);
            GoogleGeocodingAPIResponceData resGeo = accGeo.GetData(Address);
            // 指定住所周辺情報取得
            GooglePlaceAccessClass accPlc = new GooglePlaceAccessClass(apiKye);
            GooglePlaceAPIResponceData resPlc = accPlc.GetNearbysearchData(resGeo.results[0].geometry.location.lat, resGeo.results[0].geometry.location.lng, Radius, Keyword);
            
            // 周辺情報のリストをビューデータへ変換
            foreach(var item in resPlc.results)
            {
                PlaceViewData tPlace = new PlaceViewData();
                tPlace.Name = item.name;
                tPlace.Address = item.vicinity;
                tPlace.Place_URL = CreateMapURL(item.place_id,item.geometry.location.lat,item.geometry.location.lng);

                // ユーザに登録された住所を取得。登録住所からの道順を取得
                GoogleGeocodingAPIResponceData tresGeo = accGeo.GetData(this.user.Address);
                tPlace.Direction_Working_URL = CreateMapURL(item.place_id, item.geometry.location.lat, item.geometry.location.lng, tresGeo.results[0].geometry.location.lat, tresGeo.results[0].geometry.location.lng, GoogleTravelMode.WALKING);
                tPlace.Direction_Cycling_URL = CreateMapURL(item.place_id, item.geometry.location.lat, item.geometry.location.lng, tresGeo.results[0].geometry.location.lat, tresGeo.results[0].geometry.location.lng, GoogleTravelMode.BICYCLING);
                tPlace.Direction_Driving_URL = CreateMapURL(item.place_id, item.geometry.location.lat, item.geometry.location.lng, tresGeo.results[0].geometry.location.lat, tresGeo.results[0].geometry.location.lng, GoogleTravelMode.DRIVING);
                if(item.photos != null)
                {
                    tPlace.Photo_URL = accPlc.GetPhoto(item.photos[0].photo_reference, widht, height);
                }
                tPlace.rating = item.rating;
                viewData.placedata.Add(tPlace);
            }

            return viewData;
        }
        /// <summary>
        /// GoogleMapの探索結果URL取得
        /// </summary>
        /// <param name="PlaceID">目的地ID</param>
        /// <param name="Place_lat">目的地緯度</param>
        /// <param name="Place_lng">目的地経度</param>
        private static string CreateMapURL(string PlaceID,double Place_lat,double Place_lng)
        {
            return GoogleMapURL.Search + GoogleMapParaCode.SearchPara_query_place_id + PlaceID +
                   GoogleMapParaCode.SearchPara_query + Place_lat.ToString() + "," + Place_lng.ToString();
        }
        /// <summary>
        /// GoogleMapの道順結果URL取得
        /// </summary>
        /// <param name="PlaceID">目的地ID</param>
        /// <param name="Place_lat">目的地緯度</param>
        /// <param name="Place_lng">目的地経度</param>
        /// <param name="Orign_Lat">出発地緯度</param>
        /// <param name="Orign_lng">出発地経度</param>
        /// <param name="TravelMode">GoogleTravelModeのコード</param>
        private static string CreateMapURL(string PlaceID, double Place_lat, double Place_lng, double Orign_Lat, double Orign_lng, string TravelMode)
        {
            return GoogleMapURL.Direction + GoogleMapParaCode.DirectionPara_origin + Orign_Lat.ToString() + "," + Orign_lng.ToString() +
                   GoogleMapParaCode.DirectionPara_destination + Place_lat.ToString() + "," + Place_lng.ToString() +
                   GoogleMapParaCode.DirectionPara_travelmode + TravelMode;
        }
    }
}
