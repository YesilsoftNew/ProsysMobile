using ProsysMobile.Helper;
using ProsysMobile.ViewModels.Base;
using ProsysMobile.ViewModels.Pages.System;
using Xamarin.Forms;

[assembly: ExportFont("poppins_black.ttf", Alias = "poppins_black")]
[assembly: ExportFont("poppins_black_italic.ttf", Alias = "poppins_black_italic")]
[assembly: ExportFont("poppins_bold.ttf", Alias = "poppins_bold")]
[assembly: ExportFont("poppins_bold_italic.ttf", Alias = "poppins_bold_italic")]
[assembly: ExportFont("poppins_extra_bold.ttf", Alias = "poppins_extra_bold")]
[assembly: ExportFont("poppins_extra_bold_italic.ttf", Alias = "poppins_extra_bold_italic")]
[assembly: ExportFont("poppins_extra_light.ttf", Alias = "poppins_extra_light")]
[assembly: ExportFont("poppins_extra_light_italic.ttf", Alias = "poppins_extra_light_italic")]
[assembly: ExportFont("poppins_italic.ttf", Alias = "poppins_italic")]
[assembly: ExportFont("poppins_light.ttf", Alias = "poppins_light")]
[assembly: ExportFont("poppins_light_italic.ttf", Alias = "poppins_light_italic")]
[assembly: ExportFont("poppins_medium.ttf", Alias = "poppins_medium")]
[assembly: ExportFont("poppins_medium_italic.ttf", Alias = "poppins_medium_italic")]
[assembly: ExportFont("poppins_regular.ttf", Alias = "poppins_regular")]
[assembly: ExportFont("poppins_semi_bold.ttf", Alias = "poppins_semi_bold")]
[assembly: ExportFont("poppins_semi_bold_italic.ttf", Alias = "poppins_semi_bold_italic")]
[assembly: ExportFont("poppins_thin.ttf", Alias = "poppins_thin")]
[assembly: ExportFont("poppins_thin_italic.ttf", Alias = "poppins_thin_italic")]

namespace ProsysMobile
{
    public partial class App : Application
    {
        public static string Prosys_Api = "ysın";   // dev test

        public App()
        {
            InitializeComponent();

            GlobalSetting.Instance.PagesPath = "Pages";
            GlobalSetting.Instance.ViewsPath = "Views";
            GlobalSetting.Instance.ViewModelPath = "ViewModels";
            GlobalSetting.Instance.BaseEndpoint = Prosys_Api;

            ViewModelLocator.Init<SplashPageViewModel>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
