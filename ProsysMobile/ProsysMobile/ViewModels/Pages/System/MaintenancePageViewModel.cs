using System;
using System.Threading.Tasks;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.Resources.Language;
using ProsysMobile.ViewModels.Base;

namespace ProsysMobile.ViewModels.Pages.System
{
    public class MaintenancePageViewModel : ViewModelBase
    {
        NavigationModel<MaintenancePageViewParamModel> _navigationModel;
        
        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is NavigationModel<MaintenancePageViewParamModel> { Model: { } } navigationModel)
                _navigationModel = navigationModel;
            else
                throw new ArgumentNullException(nameof(navigationData), $"It is mandatory to send parameter of type {nameof(MaintenancePageViewModel)}!");
            
            PageLoad();
            
            return Task.CompletedTask;
        }
        
        #region Propertys
        
        private string _infoText;
        public string InfoText { get => _infoText; set { _infoText = value; PropertyChanged(() => InfoText); } }

        #endregion

        #region Commands

        #endregion

        #region Methods

        private void PageLoad()
        {
            var time = $"{_navigationModel.Model.CheckTimeResponseModel.StartTime ?? ""} - {_navigationModel.Model.CheckTimeResponseModel.EndTime ?? ""}";
            
            InfoText = Resource.StoreMaintenanceWorkPleaseClosedDueToXxxAgainBetweenHoursTry.Replace(Constants.ReplaceWord, time);
        }

        #endregion
    }
}