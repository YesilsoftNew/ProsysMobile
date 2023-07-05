using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProsysMobile.Helper;
using ProsysMobile.Pages.System;
using ProsysMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ProsysMobile.Services.Navigation
{
    public class NavigationService : NavigationServiceHelper, INavigationService
    {
        // Android tarafında animated false set edilmisti onu true set ettim problem olurs bakalım 20230222_1129
        //private bool animated => Device.RuntimePlatform == Device.Android ? false : true; 
        private bool animated => Device.RuntimePlatform == Device.Android ? true : true;
        public ViewModelBase PreviousPageViewModel
        {
            get
            {
                var stack = Shell.Current.Navigation.NavigationStack;
                var viewModel = stack[stack.Count - 1].BindingContext;
                return viewModel as ViewModelBase;
            }
        }
        public Task InitializeAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return SetMainPageAsync<TViewModel>(true, null);
        }
        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null, true);
        }
        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter, true);
        }

        public Task NavigateToAsync<TViewModel>(object parameter, bool animated) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter, animated);
        }
        
        public Task NavigateToModalAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToModalAsync(typeof(TViewModel), null, true);
        }
        public Task NavigateToModalAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToModalAsync(typeof(TViewModel), parameter, true);
        }

        public Task NavigateToModalAsync<TViewModel>(object parameter, bool animated) where TViewModel : ViewModelBase
        {
            return InternalNavigateToModalAsync(typeof(TViewModel), parameter, animated);
        }

        public async Task NavigatePopAsync()
        {
            await Shell.Current.Navigation.PopAsync(animated);
        }

        public async Task NavigatePopAsync(bool animated)
        {
            await Shell.Current.Navigation.PopAsync(animated);
        }

        public async Task NavigatePopModalAsync()
        {
            await NavigatePopModalAsync(true);
        }

        public async Task NavigatePopModalAsync(bool animated)
        {
            if (Shell.Current.Navigation.ModalStack.Count > 0)
                await Shell.Current.Navigation.PopModalAsync(animated: animated);
        }

        #region Popup
        public async Task NavigatePopPopupAsync()
        {
            await Shell.Current.Navigation.PopPopupAsync(animated);
        }

        public Task NavigateToPopupAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToPopupAsync(typeof(TViewModel), null);
        }

        public Task NavigateToPopupAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToPopupAsync(typeof(TViewModel), parameter);
        }

        public Task RemovePopupAsync()
        {
            try
            {
                if (PopupNavigation.Instance.PopupStack.Count > 0)
                {
                    PopupNavigation.Instance.PopAsync(animated);
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            return Task.FromResult(true);
        }

        public Task RemovePopupAsync(PopupPage page)
        {
            try
            {
                foreach (var item in PopupNavigation.Instance.PopupStack)
                {
                    if (item.ToString() == page.ToString())
                    {
                        PopupNavigation.Instance.RemovePageAsync(item, animated);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            return Task.FromResult(true);
        }
        #endregion

        #region Backdrop
        public Task NavigateToBackdropAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToBackdropAsync(typeof(TViewModel), null, null);
        }

        public Task NavigateToBackdropAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToBackdropAsync(typeof(TViewModel), parameter, null);
        }

        public Task NavigateToBackdropAsync<TViewModel>(object parameter, string diffrencePageName) where TViewModel : ViewModelBase
        {
            return InternalNavigateToBackdropAsync(typeof(TViewModel), parameter, diffrencePageName);
        }

        public async Task NavigatePopBackdropAsync()
        {
            try
            {
                for (int i = PopupNavigation.Instance.PopupStack.Count - 1; i >= 0; i--)
                {
                    if (PopupNavigation.Instance.PopupStack[i].ToString() == new ToastMessagePage().ToString())
                        continue;

                    await PopupNavigation.Instance.RemovePageAsync(PopupNavigation.Instance.PopupStack[i], animated);

                    break;
                }

                //await Shell.Current.Navigation.PopPopupAsync(animated);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        public Task NavigatePopAllBackdropAsync()
        {
            try
            {
                PopupNavigation.Instance.PopAllAsync();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            return Task.FromResult(true);
        }
        #endregion

        public Task RemoveLastFromBackStackAsync()
        {
            var stack = Shell.Current.Navigation.NavigationStack;
            Shell.Current.Navigation.RemovePage(stack[stack.Count - 2]);
            return Task.FromResult(true);
        }
        public Task RemoveBackStackAsync(bool includeLastPage = false)
        {
            var stack = Shell.Current.Navigation.NavigationStack;

            var count = includeLastPage ? 0 : 1;
            for (int i = 0; i < stack.Count - count; i++)
            {
                var page = stack[i];
                Shell.Current.Navigation.RemovePage(page);
            }

            return Task.FromResult(true);
        }
        public async Task SetMainPageAsync<TViewModel>(bool wrapInNavigationPage, object parameter) where TViewModel : ViewModelBase
        {
            try
            {
                Page page = CreatePage(typeof(TViewModel));
                Shell.SetNavBarIsVisible(page, wrapInNavigationPage);
                Application.Current.MainPage = page;
                await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);

            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }
        public void SetShellCurrentTabItem(int index)
        {
            if (Shell.Current.CurrentItem.Items.Count >= index)
                Shell.Current.CurrentItem.CurrentItem = Shell.Current.CurrentItem.Items[index];
        }
    }
}
