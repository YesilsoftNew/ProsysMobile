using ProsysMobile.CustomControls.Backdrop;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using ProsysMobile.Helper;
using ProsysMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ProsysMobile.Services.Navigation
{
    public class NavigationServiceHelper
    {
        protected async Task InternalNavigateToAsync(Type viewModelType, object parameter, bool animated)
        {
            Page page = CreatePage(viewModelType);

            await Shell.Current.Navigation.PushAsync(page, animated);

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }
        protected async Task InternalNavigateToBackdropAsync(Type viewModelType, object parameter, string diffrencePageName)
        {
            try
            {
                BottomToTopBackdropPopupPage page = CreateBottomToTopBackdropPopupPage(viewModelType, diffrencePageName);
                await PopupNavigation.Instance.PushAsync(page);
                await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }
        protected async Task InternalNavigateToPopupAsync(Type viewModelType, object parameter)
        {
            PopupPage page = CreatePopupPage(viewModelType);
            await PopupNavigation.Instance.PushAsync(page);
            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }
        protected async Task InternalNavigateToModalAsync(Type viewModelType, object parameter, bool animated)
        {
            try
            {
                Page page = CreatePage(viewModelType);
                await Shell.Current.Navigation.PushModalAsync(page, animated: animated);
                await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }
        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            // her yeni view model acıldıgında temizliyorum
            ViewModelLocator.diffrenceViewModelName = "";

            //var viewName = viewModelType.Namespace.Replace(GlobalSetting.Instance.ViewModelPath, GlobalSetting.Instance.PagesPath);
            //viewName += $".{viewModelType.Name.Replace("ViewModel", string.Empty)}";
            //var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            //var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            //var viewType = Type.GetType(viewAssemblyName);
            //return viewType;

            var viewName = "";

            if (viewModelType.Namespace.IndexOf(GlobalSetting.Instance.PagesPath) < 0)
                viewName = viewModelType.Namespace.Replace(GlobalSetting.Instance.ViewModelPath, GlobalSetting.Instance.PagesPath);
            else
                viewName = viewModelType.Namespace.Replace($".{GlobalSetting.Instance.ViewModelPath}", "");

            viewName += $".{viewModelType.Name.Replace("ViewModel", string.Empty)}";
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        protected Type GetPopupPageTypeForViewModel(Type viewModelType, string diffrencePageName = "")
        {
            var viewName = "";
            
            // her yeni view model acıldıgında temizliyorum
            ViewModelLocator.diffrenceViewModelName = "";

            if (viewModelType.Namespace.IndexOf(GlobalSetting.Instance.PagesPath) < 0)
                viewName = viewModelType.Namespace.Replace(GlobalSetting.Instance.ViewModelPath, GlobalSetting.Instance.PagesPath);
            else
                viewName = viewModelType.Namespace.Replace($".{GlobalSetting.Instance.ViewModelPath}", "");

            if (string.IsNullOrWhiteSpace(diffrencePageName))
                viewName += $".{viewModelType.Name.Replace("ViewModel", string.Empty)}";
            else
                viewName += $".{diffrencePageName}";

            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }
        protected Page CreatePage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
                throw new Exception($"Cannot locate page type for {viewModelType}");
            return Activator.CreateInstance(pageType) as Page;
        }
        protected PopupPage CreatePopupPage(Type viewModelType)
        {
            Type pageType = GetPopupPageTypeForViewModel(viewModelType);
            if (pageType == null)
                throw new Exception($"Cannot locate popup page type for {viewModelType}");
            return Activator.CreateInstance(pageType) as PopupPage;
        }
        protected BottomToTopBackdropPopupPage CreateBottomToTopBackdropPopupPage(Type viewModelType, string diffrencePageName)
        {
            try
            {
                Type pageType = GetPopupPageTypeForViewModel(viewModelType, diffrencePageName);
                if (pageType == null)
                    throw new Exception($"Cannot locate popup page type for {viewModelType}");
                return Activator.CreateInstance(pageType) as BottomToTopBackdropPopupPage;
            }
            catch (Exception ex)
            {
                return Activator.CreateInstance(null) as BottomToTopBackdropPopupPage;

            }
        }
    }
}
