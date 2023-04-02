using System.ComponentModel;

namespace SiteOfSweetsApp.Models
{
    /// <summary>
    /// アカウントビューデータ
    /// </summary>
    public class AccountViewData
    {
        /// <summary>
        /// アカウント名
        /// </summary>
        [DisplayName("氏名")]
        public string Name { get; set; }

    }
}
