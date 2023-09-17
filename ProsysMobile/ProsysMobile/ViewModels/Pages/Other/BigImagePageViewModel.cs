using System;
using System.Threading.Tasks;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.ViewModels.Base;

namespace ProsysMobile.ViewModels.Pages.Other
{
    public class BigImagePageViewModel : ViewModelBase
    {
        
        NavigationModel<BigImagePageViewParamModel> _navigationModel;

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is NavigationModel<BigImagePageViewParamModel> navigationModel)
                _navigationModel = navigationModel;
            else
                throw new ArgumentNullException(nameof(navigationData), $"It is mandatory to send parameter of type {nameof(BigImagePageViewModel)}!");

            PageLoad();
            
            return Task.CompletedTask;
        }
        
        #region Properties

        private string _source;
        public string Source { get => _source; set { _source = value; PropertyChanged(() => Source); } }

        #endregion

        #region Command


        #endregion

        #region Methods

        private void PageLoad()
        {
            if (_navigationModel != null)
            {
                Source = _navigationModel.Model.Source;
            }
            else
            {
                DialogService.WarningToastMessage("Bir hata oluştu");
                NavigationService.NavigatePopBackdropAsync();
            }
        }

        #endregion
        
    }
}