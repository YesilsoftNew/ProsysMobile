using ProsysMobile.Helper;
using ProsysMobile.Helper.ApiClient;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Base;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ProsysMobile.ViewModels.Pages.System
{
    public class SplashPageViewModel : ViewModelBase
    {
        private IDefaultSettingsSQLiteService _defaultSettingsSQLiteService;

        public SplashPageViewModel(IDefaultSettingsSQLiteService defaultSettingsSQLiteService)
        {
            _defaultSettingsSQLiteService = defaultSettingsSQLiteService;
        }

        public override Task InitializeAsync(object navigationData)
        {
            GoToLoginPage();

            return base.InitializeAsync(navigationData);
        }
        async Task GoToLoginPage()
        {
            try
            {
                await Task.Delay(500);

                await NavigationService.SetMainPageAsync<LoginPageViewModel>();


                //TODO : USER daha önce girdiyse mainpage e girmediyse loginPage e yönlendir

                //DefaultSettings userDefaultSetting = _defaultSettingsSQLiteService.getSettings("UserId");

                //if (!(userDefaultSetting is null))
                //{
                //    user = _userSQLiteService.GetUser(TOOLS.ToLong(userDefaultSetting.Value));

                //    if (user != null)
                //    {
                //        GlobalSetting.Instance.User = user;

                //        DefaultSettings userTokenDefaultSetting = _defaultSettingsSQLiteService.getSettings("UserToken");
                //        DefaultSettings userTokenExpiredDateDefaultSetting = _defaultSettingsSQLiteService.getSettings("UserTokenExpiredDate");

                //        if (userTokenDefaultSetting != null)
                //        {
                //            GlobalSetting.Instance.JWTToken = userTokenDefaultSetting.Value;
                //            GlobalSetting.Instance.JWTTokenExpireDate = Convert.ToDateTime(userTokenExpiredDateDefaultSetting.Value);
                //        }
                //        else
                //        {
                //            userTokenDefaultSetting = new DefaultSettings();
                //            userTokenExpiredDateDefaultSetting = new DefaultSettings();
                //        }

                //        if (string.IsNullOrWhiteSpace(GlobalSetting.Instance.JWTToken) || DateTime.Now >= GlobalSetting.Instance.JWTTokenExpireDate)
                //        {
                //            #region JWTToken
                //            UserAuthRequestModel userModel = new UserAuthRequestModel();
                //            userModel.Username = user.UserName;
                //            userModel.Password = user.Password;
                //            userModel.UserType = (int)enAccessTokenUserType.WiseUser;
                //            userModel.DeviceGuid = Guid.NewGuid().ToString();
                //            userModel.Token = "";

                //            var result = await RunSafeApi(ApiClient.Instance.AuthApi.UserAuthentication(userModel));
                //            //if (result.ExceptionMessage == "retryTask")
                //            //    result = await RunSafeApi(ApiClient.ApiClient.Instance.AuthApi.UserAuthentication(userModel));

                //            if (result.ResponseData != null && result.IsSuccess)
                //            {
                //                var jwt = result.ResponseData.Token.ToString();
                //                var handler = new JwtSecurityTokenHandler();
                //                var token = handler.ReadJwtToken(jwt);

                //                GlobalSetting.Instance.JWTToken = result.ResponseData.Token.ToString();
                //                GlobalSetting.Instance.JWTTokenExpireDate = Convert.ToDateTime(token.Claims.First(claim => claim.Type == "ExpiredDateTime").Value);

                //                userTokenDefaultSetting.Key = "UserToken";
                //                userTokenDefaultSetting.Value = GlobalSetting.Instance.JWTToken;
                //                _defaultSettingsSQLiteService.Save(userTokenDefaultSetting);

                //                userTokenExpiredDateDefaultSetting.Key = "UserTokenExpiredDate";
                //                userTokenExpiredDateDefaultSetting.Value = TOOLS.ToString(GlobalSetting.Instance.JWTTokenExpireDate);
                //                _defaultSettingsSQLiteService.Save(userTokenExpiredDateDefaultSetting);
                //            }
                //            #endregion
                //        }

                //        #region Current MULTIUSER -- İnternet varsa güncel verileri alıyorum
                //        if (GlobalSetting.Instance.IsInternetConnectionAvailable)
                //        {
                //            if (!await GetCurrentMultiUserApi())
                //            {
                //                DialogService.WarningToastMessage(GlobalSetting.translateExtension.GetTranstlateValue("pgFirstSenkPage_cs_SEKNRONIZASYONISLEMIYAPILAMADI"));
                //            }
                //        }
                //        #endregion

                //        await NavigationService.SetMainPageAsync<AppShellViewModel>();
                //    }
                //    else
                //        await NavigationService.SetMainPageAsync<LoginPageViewModel>();
                //}
                //else
                //    await NavigationService.SetMainPageAsync<LoginPageViewModel>();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        async Task<bool> GetCurrentMultiUserApi()
        {
            try
            {
                //#region MultiUser
                //var result = await _multiUserService.Get(new ApiFilterRequestModel()
                //{
                //    UserId = TOOLS.ToLong(GlobalSetting.Instance.User.Id),
                //    FleetGuid = GlobalSetting.Instance.User.FleetGuid
                //});

                //if (!(result != null && result.IsSuccess && result.ResponseData is MultiUserResponseModel multiUserResponseModel))
                //{
                //    DialogService.WarningToastMessage("Güncel kullanıcı verileri alınamadı.");
                //    return false;
                //}

                //multiUserResponseModel.FleetDto.ForEach(x => x.isSync = (int)enIsSync.Sent);
                //multiUserResponseModel.PermissionDto.ForEach(x => x.isSync = (int)enIsSync.Sent);
                //multiUserResponseModel.ServiceProviderDefinitionDto.ForEach(x => x.isSync = (int)enIsSync.Sent);
                //multiUserResponseModel.ServiceCenterDefinitionDto.ForEach(x => x.isSync = (int)enIsSync.Sent);
                //multiUserResponseModel.StockCenterDefinitionDto.ForEach(x => x.isSync = (int)enIsSync.Sent);
                //multiUserResponseModel.LocationDefinationDto.ForEach(x => x.isSync = (int)enIsSync.Sent);
                //multiUserResponseModel.FleetBranchLocationRelationDto.ForEach(x => x.isSync = (int)enIsSync.Sent);
                //multiUserResponseModel.ServiceDefinitionDto.ForEach(x => x.isSync = (int)enIsSync.Sent);
                //multiUserResponseModel.FleetBranchDto.ForEach(x => x.isSync = (int)enIsSync.Sent);
                //multiUserResponseModel.UserTypeDefinitionRelationDto.isSync = (int)enIsSync.Sent;
                //multiUserResponseModel.ServiceDefinitionMaterialDto.ForEach(x => x.isSync = (int)enIsSync.Sent);

                //_fleetSQLiteService.SaveAll(multiUserResponseModel.FleetDto);
                //_permissionSQLiteService.SaveAll(multiUserResponseModel.PermissionDto);
                //_serviceProviderDefinitionSQLiteService.SaveAll(multiUserResponseModel.ServiceProviderDefinitionDto);
                //_serviceCenterDefinitionSQLiteService.SaveAll(multiUserResponseModel.ServiceCenterDefinitionDto);
                //_stockCenterDefinitionSQLiteService.SaveAll(multiUserResponseModel.StockCenterDefinitionDto);
                //_locationDefinationSQLiteService.SaveAll(multiUserResponseModel.LocationDefinationDto);
                //_fleetBranchLocationRelationSQLiteService.SaveAll(multiUserResponseModel.FleetBranchLocationRelationDto);
                //_serviceDefinitionSQLiteService.SaveAll(multiUserResponseModel.ServiceDefinitionDto);
                //_fleetBranchSQLiteService.SaveAll(multiUserResponseModel.FleetBranchDto);
                //_userTypeDefinitionRelationSQLiteService.Save(multiUserResponseModel.UserTypeDefinitionRelationDto);
                //_serviceDefinitionMaterialSQLiteService.SaveAll(multiUserResponseModel.ServiceDefinitionMaterialDto);
                //#endregion

                //DialogService.SuccessToastMessage("Güncel kullanıcı verileri alındı.");

                return true;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
                return false;
            }
        }
    }
}