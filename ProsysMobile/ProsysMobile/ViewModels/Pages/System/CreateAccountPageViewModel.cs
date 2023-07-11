using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Services.API.UserMobile;
using ProsysMobile.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.System
{
    public class CreateAccountPageViewModel : ViewModelBase
    {
        private ISignUpService _signUpService;

        public CreateAccountPageViewModel(ISignUpService signUpService)
        {
            _signUpService = signUpService;
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (Debugger.IsAttached)
            {
                FirstName = "Yasin";
                Surname = "Altıntop";
                CompanyCode = "123456";
                Email = "altintopyasin8@gmail.com";
            }

            return base.InitializeAsync(navigationData);
        }

        #region Propertys
        private string _firstName;
        public string FirstName { get => _firstName; set { _firstName = value; PropertyChanged(() => FirstName); } }

        private string _surname;
        public string Surname { get => _surname; set { _surname = value; PropertyChanged(() => Surname); } }
        
        private string _companyCode;
        public string CompanyCode { get => _companyCode; set { _companyCode = value; PropertyChanged(() => CompanyCode); } }
        
        private string _email;
        public string Email { get => _email; set { _email = value; PropertyChanged(() => Email); } }
        #endregion

        #region Commands

        public ICommand RegisterClickCommand => new Command(async () =>
        {
            try
            {
                await GetUserSignUpAsync();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });
        #endregion

        async Task GetUserSignUpAsync()
        {
            try
            {
                if (IsBusy) return;

                IsBusy = true;

                if (!GlobalSetting.Instance.IsConnectedInternet)
                {
                    DialogService.WarningToastMessage("Lütfen internet bağlantınızı kontrol ediniz! QQQ");
                    IsBusy = false;
                    return;
                }

                if (string.IsNullOrWhiteSpace(FirstName))
                {
                    DialogService.WarningToastMessage("Lütfen isminizi yazınız! QQQ");
                    IsBusy = false;
                    return;
                }

                if (string.IsNullOrWhiteSpace(Surname))
                {
                    DialogService.WarningToastMessage("Lütfen soyisminizi yazınız! QQQ");
                    IsBusy = false;
                    return;
                }

                if (string.IsNullOrWhiteSpace(Email))
                {
                    DialogService.WarningToastMessage("Lütfen email adresinizi yazınız! QQQ");
                    IsBusy = false;
                    return;
                }

                if (string.IsNullOrWhiteSpace(CompanyCode))
                {
                    DialogService.WarningToastMessage("Lütfen şirket kodunu yazınız! QQQ");
                    IsBusy = false;
                    return;
                }

                UserMobileDto userMobileDto = new UserMobileDto();
                userMobileDto.FIRSTNAME = FirstName;
                userMobileDto.SURNAME = Surname;
                userMobileDto.EMAIL = Email;
                userMobileDto.COMPANYCODE = CompanyCode;

                var result = await _signUpService.SignUp(userMobileDto, Models.CommonModels.Enums.enPriorityType.UserInitiated);

                if (result.ResponseData != null && result.IsSuccess)
                {
                    DialogService.SuccessToastMessage("Talebiniz gönderildi! QQQ");
                    NavigationService.NavigatePopBackdropAsync();
                }
                else
                {
                    DialogService.WarningToastMessage("Bir hata oluştu! QQQ");
                    IsBusy = false;
                    return;
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                DialogService.WarningToastMessage("Bir hata oluştu! QQQ");
                ProsysLogger.Instance.CrashLog(ex);
                IsBusy = false;
            }
        }
    }
}