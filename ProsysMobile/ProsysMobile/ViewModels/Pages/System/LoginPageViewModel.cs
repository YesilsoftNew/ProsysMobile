using ProsysMobile.Helper;
using ProsysMobile.Helper.ApiClient;
using ProsysMobile.Helper.SQLite;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.API.Auth;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.System
{
    public class LoginPageViewModel : ViewModelBase
    {
        private IAuthService _authService;
        private IDefaultSettingsSQLiteService _defaultSettingsSQLiteService;

        public LoginPageViewModel(IAuthService authService, IDefaultSettingsSQLiteService defaultSettingsSQLiteService)
        {
            _authService = authService;
            _defaultSettingsSQLiteService = defaultSettingsSQLiteService;

            //// filo secimi yaparken hata aldıysa login'e dusuruyoruz ordada cift kullanıcı kayıtlı olmasın diye drop&create yapıyoruz
            //Database.SQLConnection.DropTable<User>();
            //Database.SQLConnection.DropTable<DefaultSettings>();

            //Database.SQLConnection.CreateTable<User>();
            //Database.SQLConnection.CreateTable<DefaultSettings>();
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (Debugger.IsAttached)
            {
                Email = "uaremines"; 
                Password = "uaremines";
            }

            return base.InitializeAsync(navigationData);
        }

        #region Propertys
        private string _email;
        public string Email { get => _email; set { _email = value; PropertyChanged(() => Email); } }

        private string _password;
        public string Password { get => _password; set { _password = value; PropertyChanged(() => Password); } }
        #endregion

        #region Commands
        public ICommand LoginClickCommand => new Command(async () =>
        {
            try
            {
                await GetUserAuthAsync();

            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });

        public ICommand RegisterClickCommand => new Command(async () =>
        {
            try
            {
                NavigationService.NavigateToBackdropAsync<CreateAccountPageViewModel>();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });
        #endregion

        async Task GetUserAuthAsync()
        {
            try
            {
                if (IsBusy) return;

                IsBusy = true;

                //if (!GlobalSetting.Instance.IsInternetConnectionAvailable)
                //{
                //    DialogService.WarningToastMessage("Lütfen internet bağlantınızı kontrol ediniz!");
                //    IsBusy = false;
                //    return;
                //}

                //if (string.IsNullOrWhiteSpace(UserName))
                //{
                //    DialogService.WarningToastMessage("Lütfen kullanıcı adınızı yazınız!");
                //    IsBusy = false;
                //    return;
                //}

                //if (string.IsNullOrWhiteSpace(Password))
                //{
                //    DialogService.WarningToastMessage("Lütfen şifrenizi yazınız!");
                //    IsBusy = false;
                //    return;
                //}

                SignIn signIn = new SignIn();

                signIn.Email = Email;
                signIn.Password = Password;
                signIn.DeviceGuid = Guid.NewGuid();
                signIn.Token = "";

                var result = await RunSafeApi(ApiClient.Instance.AuthApi.SignIn(signIn));

                if (result.ResponseData != null && result.IsSuccess)
                {
                    var jwt = result.ResponseData.SignIn.Token.ToString();
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(jwt);

                    GlobalSetting.Instance.JWTToken = result.ResponseData.SignIn.Token.ToString();
                    GlobalSetting.Instance.JWTTokenExpireDate = Convert.ToDateTime(token.Claims.First(claim => claim.Type == "ExpiredDateTime").Value);

                    //Kullanıcı doğrulaması başarılı ise yönlendirme yapılır. Veriler çekilmeye başlar.
                    if (result.ResponseData.UserMobile != null)
                    {
                        Database.SQLConnection.Insert(result.ResponseData.UserMobile, "OR REPLACE");
                        GlobalSetting.Instance.User = result.ResponseData.UserMobile;
                    }

                    //BURDA SAYFA AÇILIYO
                    //NavigationModel<FleetListPageViewParamModel> navigationModel = new NavigationModel<FleetListPageViewParamModel>
                    //{
                    //    Model = new FleetListPageViewParamModel()
                    //};

                    //await NavigationService.SetMainPageAsync<FirstSenkPageViewModel>();

                    List<DefaultSettings> TokenSettings = new List<DefaultSettings>()
                    {
                    new DefaultSettings{Key="UserToken",Value=GlobalSetting.Instance.JWTToken},
                    new DefaultSettings{Key="UserTokenExpiredDate",Value=TOOLS.ToString(GlobalSetting.Instance.JWTTokenExpireDate)},
                    new DefaultSettings{Key="UserId",Value=GlobalSetting.Instance.User.ID.ToString()}
                    };

                    _defaultSettingsSQLiteService.SaveAll(TokenSettings);
                }
                else
                {
                    //TODO ACILCAK
                    //DialogService.WarningToastMessage("Kullanıcı adınız veya şifreniz hatalı!");
                    IsBusy = false;
                    return;
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
                IsBusy = false;
            }
        }
    }
}