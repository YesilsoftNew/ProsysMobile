using System;
using System.Windows.Input;
using ProsysMobile.Helper;
using ProsysMobile.Resources.Language;
using ProsysMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Order
{
    public class UserOrdersPageViewModel : ViewModelBase
    {
        private string _url = $"https://few-snails-post.loca.lt";
        public string Url { get => _url; set { _url = value; PropertyChanged(() => Url); } }
        
        private string _token = GlobalSetting.Instance.JWTToken;
        public string Token { get => _token; set { _token = value; PropertyChanged(() => Token); } }
        
        
            
        #region Commands
        public ICommand ClosePageClickCommand => new Command(async () =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                NavigationService.NavigatePopModalAsync();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            DoubleTapping.ResumeTap();
        });
        #endregion

    }
}