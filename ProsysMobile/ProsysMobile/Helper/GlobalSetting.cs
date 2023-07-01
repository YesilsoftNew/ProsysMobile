using System;
using WiseDynamicMobile.Helper;

namespace WiseMobile.Helper
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
        public string ImageFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/WiseMobileImages/";
        #endregion

        #region Api
        public string JWTToken { get; set; } = "";
        public DateTime JWTTokenExpireDate { get; set; }
        public User User { get; set; }
        //public string WebAppLink => "http://sprov.wise-dynamic.com/";
        //public string WebAppLink => "http://apimigtest.wise-dynamic.com/";
        public string WebAppLink => "https://appv2.wise-dynamic.com/";
        #endregion

        #region Other
        public string WorkOrderProcessCancelledReasonGuid => "CB60DFF4-89C9-4ACA-9413-7CB6F907016B";
        public decimal threadDepthTolerance = TOOLS.ToDecimal(0.5);
        public string ImageThumbnailPrefix => "thumbnail_";
        public System.Timers.Timer tmrDataManagement; // bekleyen verileri gondermek icin kullanıyoruz
        public bool SendWaitingDataProcessRunning { get; set; } // localde gonderilmeyi bekleyen kayıtlar gonderiliyorsa bu alanı true yapıyorum. (bu alanı DataManagement ekranında ve arka planda çalışan otomatik service icerisinde kullanıyoruz)
        #endregion
    }
}