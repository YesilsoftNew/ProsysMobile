using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Services.API.UserMobile;
using ProsysMobile.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using ProsysMobile.Resources.Language;
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

        private async Task GetUserSignUpAsync()
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                if (!GlobalSetting.Instance.IsConnectedInternet)
                {
                    DialogService.WarningToastMessage(Resource.PleaseCheckYourInternetConnection);
                    DoubleTapping.ResumeTap();
                    return;
                }

                if (string.IsNullOrWhiteSpace(FirstName))
                {
                    DialogService.WarningToastMessage(Resource.PleaseWriteYourName);
                    DoubleTapping.ResumeTap();
                    return;
                }

                if (string.IsNullOrWhiteSpace(Surname))
                {
                    DialogService.WarningToastMessage(Resource.PleaseWriteYourSurname);
                    DoubleTapping.ResumeTap();
                    return;
                }

                if (string.IsNullOrWhiteSpace(Email))
                {
                    DialogService.WarningToastMessage(Resource.PleaseWriteYourEMailAddress);
                    DoubleTapping.ResumeTap();
                    return;
                }

                if (string.IsNullOrWhiteSpace(CompanyCode))
                {
                    DialogService.WarningToastMessage(Resource.PleaseWriteTheCompanyCode);
                    DoubleTapping.ResumeTap();
                    return;
                }

                var userMobileDto = new UserMobileDto
                {
                    FIRSTNAME = FirstName,
                    SURNAME = Surname,
                    EMAIL = Email,
                    COMPANYCODE = CompanyCode
                };

                var result = await _signUpService.SignUp(userMobileDto, Models.CommonModels.Enums.enPriorityType.UserInitiated);

                if (result.ResponseData != null && result.IsSuccess)
                {
                    DialogService.SuccessToastMessage(Resource.YourRequestHasBeenSent);
                    NavigationService.NavigatePopBackdropAsync();
                }
                else
                {
                    DialogService.WarningToastMessage(Resource.AnErrorHasOccurred);
                }

            }
            catch (Exception ex)
            {
                DialogService.WarningToastMessage(Resource.AnErrorHasOccurred);
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            DoubleTapping.ResumeTap();
        }
    }
}