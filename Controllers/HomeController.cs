using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SiteOfSweetsApp.Models;
using SiteOfSweetsApp.Models.AccessData;
using SiteOfSweetsApp.Models.DataAccess;
using SiteOfSweetsApp.Models.Tools;
using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.Extensions.Logging.Configuration;

namespace SiteOfSweetsApp.Controllers
{
    public class HomeController : Controller 
    {
        private readonly ILogger<HomeController> _logger;


        private readonly string SessionKeyUserID = "UserID";
        private readonly string SessionKeyPassword = "Password";
        private readonly int Radius = 3000;
        private readonly string Keyword = "お菓子屋さん";
        private readonly int PhotoWidht = 150;
        private readonly int PhotoHeight = 150;

        private Logger log;
        private readonly string SystemID;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            var conbuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SystemID = conbuilder.GetConnectionString("SystemID");
            log = new Logger(SystemID);

        }
        public IActionResult Login(string? UserID,string? Password)
        {
            // 画面入力無ければセッション確認
            if(UserID == null)
            {
                UserID = HttpContext.Session.GetString(SessionKeyUserID);
            }
            if (Password == null)
            {
                Password = HttpContext.Session.GetString(SessionKeyPassword);
            }

            LoginViewData viewdata = new LoginViewData();
            viewdata.UserID = UserID;
            viewdata.Password = Password;
            // ユーザＩＤ・パスワードがどちらかしか入力されてないときはエラー
            if(UserID != null || Password != null)
            {
                if (UserID == null)
                {
                    viewdata.SystemMessage = "※ユーザＩＤを入力してください";
                    return View("Login", viewdata);
                }
                if (Password == null)
                {
                    viewdata.SystemMessage = "※パスワードを入力してください";
                    return View("Login", viewdata);
                }
            }

            // ユーザＩＤとパスワード両方入力されているときはアカウントチェック
            if (UserID != null && Password != null)
            {
                //アカウントチェック
                if (HomeModel.AccountCheck(UserID, Password))
                {
                    // セッションを格納してメイン画面へ
                    HttpContext.Session.SetString(SessionKeyUserID, UserID);
                    HttpContext.Session.SetString(SessionKeyPassword, Password);

                    log.Log("ログインしました", UserID);
                    return RedirectToAction("Main");
                }
                else
                {
                    viewdata.SystemMessage = "※ユーザＩＤもしくはパスワードが間違っています";
                    log.Log("ログイン失敗しました(UserID = " + UserID + ",Password = " + Password + ")" , UserID);
                }
            }

            // ログイン画面へ
            return View("Login",viewdata);
        }
        public IActionResult Main(string? Search)
        {
            string? UserID = HttpContext.Session.GetString(SessionKeyUserID);

            if (UserID != null) {
                // セッションを格納してメイン画面へ
                HttpContext.Session.SetString(SessionKeyUserID, UserID);
                HomeModel model = new HomeModel(UserID);
                if(Search == null)
                {
                    return View("Index", model.GetViewData(this.Radius, this.Keyword, this.PhotoWidht, this.PhotoHeight));

                }
                else
                {
                    return View("Index", model.GetViewData(this.Radius, this.Keyword, this.PhotoWidht, this.PhotoHeight,Search));
                }
            }
            else
            {
                // セッションにデータが無くなってたらログイン画面へ
                return View("Login");
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}