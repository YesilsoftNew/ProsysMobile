using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.OtherModels;
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

        private ObservableRangeCollection<ItemListType> _listTypes;
        public ObservableRangeCollection<ItemListType> ListTypes
        {
            get => _listTypes ?? (_listTypes = new ObservableRangeCollection<ItemListType>());
            set
            {
                _listTypes = value;
                PropertyChanged(() => ListTypes);
            }
        }

        #endregion

        #region Command

        public ICommand ListTypeClickCommand => new Command((sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                if (sender is ItemListType itemListType)
                {
                    SetAndClosePage(itemListType.EnItemListType);
                }
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
            ListTypes = new ObservableRangeCollection<ItemListType>
            {
                new ItemListType
                {
                    EnItemListType = enItemListType.Primary,
                    Image = "SingleView",
                    Label = "Single View"
                },
                new ItemListType
                {
                    EnItemListType = enItemListType.Secondary,
                    Image = "DoubleView",
                    Label = "Double View"
                },
                new ItemListType
                {
                    EnItemListType = enItemListType.Tertiary,
                    Image = "NoPictureView",
                    Label = "No Picture View"
                },
            };
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