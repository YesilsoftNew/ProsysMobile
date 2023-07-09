using ProsysMobile.Helper;
using ProsysMobile.Services.API.Auth;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.System
{
    public class CreateAccountPageViewModel : ViewModelBase
    {
        //private IAuthService _authService;
        //private IDefaultSettingsSQLiteService _defaultSettingsSQLiteService;

        public CreateAccountPageViewModel(/*IAuthService authService, IDefaultSettingsSQLiteService defaultSettingsSQLiteService*/)
        {
            //_authService = authService;
            //_defaultSettingsSQLiteService = defaultSettingsSQLiteService;

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
        public ICommand LoginClickCommand => new Command(async () =>
        {
            try
            {
                //await GetUserAuthAsync();
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
                //await GetUserAuthAsync();
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

                //UserAuthRequestModel userModel = new UserAuthRequestModel();
                //userModel.Username = UserName;
                //userModel.Password = Password;
                //userModel.UserType = (int)enAccessTokenUserType.WiseUser;
                //userModel.DeviceGuid = Guid.NewGuid().ToString();
                //userModel.Token = "";

                //var result = await RunSafeApi(ApiClient.Instance.AuthApi.UserAuthentication(userModel));
                ////if (result.ExceptionMessage == "retryTask")
                ////    result = await RunSafeApi(ApiClient.ApiClient.Instance.AuthApi.UserAuthentication(userModel));
                //if (result.ResponseData != null && result.IsSuccess)
                //{
                //    var jwt = result.ResponseData.Token.ToString();
                //    var handler = new JwtSecurityTokenHandler();
                //    var token = handler.ReadJwtToken(jwt);

                //    GlobalSetting.Instance.JWTToken = result.ResponseData.Token.ToString();
                //    GlobalSetting.Instance.JWTTokenExpireDate = Convert.ToDateTime(token.Claims.First(claim => claim.Type == "ExpiredDateTime").Value);

                //    //Kullanıcı doğrulaması başarılı ise yönlendirme yapılır. Veriler çekilmeye başlar.
                //    if (result.ResponseData.usersDto != null)
                //    {
                //        Database.SQLConnection.Insert(result.ResponseData.usersDto, "OR REPLACE");
                //        GlobalSetting.Instance.User = result.ResponseData.usersDto;
                //    }

                //    NavigationModel<FleetListPageViewParamModel> navigationModel = new NavigationModel<FleetListPageViewParamModel>
                //    {
                //        Model = new FleetListPageViewParamModel()
                //    };

                //    await NavigationService.SetMainPageAsync<FleetListPageViewModel>(true, navigationModel);
                //    //NavigationService.NavigateToModalAsync<FleetListPageViewModel>(result.ResponseData);

                //    //await NavigationService.SetMainPageAsync<FirstSenkPageViewModel>();

                //    List<DefaultSettings> TokenSettings = new List<DefaultSettings>()
                //    {
                //    new DefaultSettings{Key="UserToken",Value=GlobalSetting.Instance.JWTToken},
                //    new DefaultSettings{Key="UserTokenExpiredDate",Value=TOOLS.ToString(GlobalSetting.Instance.JWTTokenExpireDate)},
                //    new DefaultSettings{Key="UserId",Value=GlobalSetting.Instance.User.Id.ToString()}
                //    };

                //    _defaultSettingsSQLiteService.SaveAll(TokenSettings);
                //}
                //else
                //{
                //    DialogService.WarningToastMessage("Kullanıcı adınız veya şifreniz hatalı!");
                //    IsBusy = false;
                //    return;
                //}

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