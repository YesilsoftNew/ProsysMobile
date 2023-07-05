using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;
using ProsysMobile.Services.Base;
using ProsysMobile.ViewModels.Base;

namespace ProsysMobile.Services.Navigation
{
    public interface INavigationService : IMobileServiceBase
    {
        ViewModelBase PreviousPageViewModel { get; }
        Task InitializeAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>(object parameter, bool animated) where TViewModel : ViewModelBase;
        Task NavigateToModalAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToModalAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigateToModalAsync<TViewModel>(object parameter, bool animated) where TViewModel : ViewModelBase;
        Task NavigatePopAsync();
        Task NavigatePopAsync(bool animated);
        Task NavigatePopBackdropAsync();
        Task NavigatePopPopupAsync();
        Task NavigatePopModalAsync();
        Task NavigatePopModalAsync(bool animated);
        Task NavigateToPopupAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToPopupAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task RemovePopupAsync();
        Task RemovePopupAsync(PopupPage page);
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync(bool includeLastPage = false);
        Task SetMainPageAsync<TViewModel>(bool wrapInNavigationPage = true, object parameter = null) where TViewModel : ViewModelBase;
        void SetShellCurrentTabItem(int index);
        Task NavigatePopAllBackdropAsync();

        Task NavigateToBackdropAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToBackdropAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigateToBackdropAsync<TViewModel>(object parameter, string diffrencePageName) where TViewModel : ViewModelBase;
    }
}
