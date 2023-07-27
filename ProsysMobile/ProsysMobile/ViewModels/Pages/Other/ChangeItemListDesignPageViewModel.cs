using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Other
{
    public class ChangeItemListDesignPageViewModel : ViewModelBase
    {
        NavigationModel<ChangeItemListDesignPageViewParamModel> _navigationModel;

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is NavigationModel<ChangeItemListDesignPageViewParamModel> navigationModel)
                _navigationModel = navigationModel;
            else
                throw new ArgumentNullException(nameof(navigationData), $"It is mandatory to send parameter of type {nameof(ChangeItemListDesignPageViewModel)}!");

            PageLoad();
            
            return Task.CompletedTask;
        }

        #region Properties

        

        #endregion

        #region Command

        public ICommand ListDesign1ClickCommand => new Command(() =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                SetAndClosePage(enItemListType.Primary);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            DoubleTapping.ResumeTap();
        });
        
        public ICommand ListDesign2ClickCommand => new Command(() =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                SetAndClosePage(enItemListType.Secondary);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            DoubleTapping.ResumeTap();
        });
        
        public ICommand ListDesign3ClickCommand => new Command(() =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                SetAndClosePage(enItemListType.Tertiary);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            DoubleTapping.ResumeTap();
        });

        #endregion

        #region Methods

        private void PageLoad()
        {
            
        }

        private async void SetAndClosePage(enItemListType itemListType)
        {
            try
            {
                _navigationModel.Model.ItemListType = itemListType;
                
                await NavigationService.NavigatePopBackdropAsync();
                
                _navigationModel.ClosedPageEventCommand.Execute(_navigationModel.Model);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        #endregion
        
    }
}