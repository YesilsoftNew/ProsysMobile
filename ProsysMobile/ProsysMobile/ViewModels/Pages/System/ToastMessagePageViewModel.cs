using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.Pages.System;
using ProsysMobile.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ProsysMobile.Models.CommonModels.Enums;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.System
{
    public class ToastMessagePageViewModel : ViewModelBase
    {
        NavigationModel<ToastMessagePageViewParamModel> _toastMessagePageViewParamModel;
        public ToastMessagePageViewModel()
        {

        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                if (navigationData is NavigationModel<ToastMessagePageViewParamModel> navigationModel)
                    _toastMessagePageViewParamModel = navigationModel;
                else
                    throw new ArgumentNullException(nameof(navigationData), "It is mandatory to send parameter of type ToastMessagePageViewParamModel!");

                await pageLoad();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        async Task pageLoad()
        {
            try
            {
                IsBusy = true;

                MessageText = _toastMessagePageViewParamModel.Model.MessageText;

                switch (_toastMessagePageViewParamModel.Model.ToastMessageType)
                {
                    case enToastMessageType.Success:
                        IconImageSource = "SuccessGreen";
                        CloseImageSource = "CloseGreen";
                        MessageTextBackColor = Color.FromHex("#ECFCE5");
                        MessageTextColor = Color.FromHex("#198155");
                        break;
                    case enToastMessageType.Info:
                        IconImageSource = "nfoBlue";
                        CloseImageSource = "CloseBlue";
                        MessageTextBackColor = Color.FromHex("#C9F0FF");
                        MessageTextColor = Color.FromHex("#0065D0");
                        break;
                    case enToastMessageType.Error:
                        IconImageSource = "ErrorRed";
                        CloseImageSource = "CloseRed";
                        MessageTextBackColor = Color.FromHex("#FFE5E5");
                        MessageTextColor = Color.FromHex("#D3180C");
                        break;
                    case enToastMessageType.Warning:
                        IconImageSource = "WarningYellow";
                        CloseImageSource = "CloseYellow";
                        MessageTextBackColor = Color.FromHex("#FFEFD7");
                        MessageTextColor = Color.FromHex("#A05E03");
                        break;
                }

                _ = Task.Delay(3000).ContinueWith(async _ =>
                {
                    //if (IsClosedPage) return;

                    //IsClosedPage = true;
                    NavigationService.RemovePopupAsync(new ToastMessagePage());
                });

                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        #region Propertys
        private Color _messageTextBackColor;
        public Color MessageTextBackColor { get => _messageTextBackColor; set { _messageTextBackColor = value; PropertyChanged(() => MessageTextBackColor); } }

        private Color _messageTextColor;
        public Color MessageTextColor { get => _messageTextColor; set { _messageTextColor = value; PropertyChanged(() => MessageTextColor); } }

        private string _iconImageSource;
        public string IconImageSource { get => _iconImageSource; set { _iconImageSource = value; PropertyChanged(() => IconImageSource); } }

        private string _messageText;
        public string MessageText { get => _messageText; set { _messageText = value; PropertyChanged(() => MessageText); } }

        private string _closeImageSource;
        public string CloseImageSource { get => _closeImageSource; set { _closeImageSource = value; PropertyChanged(() => CloseImageSource); } }
        #endregion

        #region Commands
        public ICommand CloseMessageClickCommand => new Command(() =>
        {
            try
            {
                if (IsBusy) return;

                //if (IsClosedPage) return;
                
                //IsClosedPage = true;
                IsBusy = true;

                NavigationService.RemovePopupAsync(new ToastMessagePage());

                IsBusy = false;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);

                //IsClosedPage = false;
                IsBusy = false;
            }
        });
        #endregion
    }
}