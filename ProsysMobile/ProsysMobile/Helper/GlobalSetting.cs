using System;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.SQLiteModels;

namespace ProsysMobile.Helper
{
    public class GlobalSetting
    {
        public static readonly TranslateExtension translateExtension = new TranslateExtension();
        private static readonly GlobalSetting _instance = new GlobalSetting();
        public static GlobalSetting Instance
        {
            get { return _instance; }
        }

        public string DeviceGuid { get; set; } = Guid.NewGuid().ToString();
        public string AppLanguage { get; set; } = "TR";

        #region FlderPath
        public string LoadingText { get { return "Yükleniyor.."; } }
        public string BaseEndpoint { get; set; }
        public bool UseNativeHttpHandler => false;
        public string ViewsPath { get; set; } = "Views";
        public string PagesPath { get; set; } = "Pages";
        public string ViewModelPath { get; set; } = "ViewModels";
        public string CustomControlsPath { get; set; } = "CustomControls";
        public string ImageFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/ProsysMobileImages/";
        #endregion

        #region Api
        public string JWTToken { get; set; } = "";
        public DateTime JWTTokenExpireDate { get; set; }
        public USERMOBILE User { get; set; }
        public int ListPageSize => 20;

        //public string WebAppLink => "http://sprov.wise-dynamic.com/";
        //public string WebAppLink => "http://apimigtest.wise-dynamic.com/";
        //public string WebAppLink => "https://appv2.wise-dynamic.com/";
        #endregion

        #region Other
        public string ImageThumbnailPrefix => "thumbnail_";
        #endregion

        #region Internet
        public bool IsConnectedInternet { get; set; }
        #endregion
    }
}