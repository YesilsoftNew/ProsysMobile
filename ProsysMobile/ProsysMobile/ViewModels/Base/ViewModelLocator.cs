using Autofac;
using ProsysMobile.CustomControls.Backdrop;
using ProsysMobile.Handler;
using ProsysMobile.Helper;
using ProsysMobile.Selector;
using ProsysMobile.Services.Base;
using ProsysMobile.Services.Navigation;
using Rg.Plugins.Popup.Pages;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Base
{
    public static class ViewModelLocator
    {
        private static IContainer _container;
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator),
                default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }


        public static readonly BindableProperty DiffrenceViewModelNameProperty =
            BindableProperty.CreateAttached("DiffrenceViewModelName", typeof(string), typeof(ViewModelLocator),
                "", propertyChanged: OnDiffrenceViewModelNameChanged);

        public static string GetDiffrenceViewModelName(BindableObject bindable)
        {
            return (string)bindable.GetValue(DiffrenceViewModelNameProperty);
        }
        public static void SetDiffrenceViewModelName(BindableObject bindable, string value)
        {
            bindable.SetValue(DiffrenceViewModelNameProperty, value);
        }

        public static string diffrenceViewModelName;
        private static void OnDiffrenceViewModelNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            diffrenceViewModelName = TOOLS.ToString(newValue);
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Element view))
                return;

            var viewType = view.GetType();
            string viewName = "";
            if (view is BackdropPage)
                if (viewType.FullName.IndexOf(GlobalSetting.Instance.PagesPath) < 0)
                    viewName = viewType.FullName.Replace($".{GlobalSetting.Instance.ViewsPath}.", $".{GlobalSetting.Instance.ViewModelPath}.");
                else
                    viewName = viewType.FullName.Replace($".{GlobalSetting.Instance.PagesPath}.", $".{GlobalSetting.Instance.ViewModelPath}.{GlobalSetting.Instance.PagesPath}.");
            else if (view is PopupPage)
                if (viewType.FullName.IndexOf(GlobalSetting.Instance.PagesPath) < 0)
                    viewName = viewType.FullName.Replace($".{GlobalSetting.Instance.ViewsPath}.", $".{GlobalSetting.Instance.ViewModelPath}.");
                else
                    viewName = viewType.FullName.Replace($".{GlobalSetting.Instance.PagesPath}.", $".{GlobalSetting.Instance.ViewModelPath}.{GlobalSetting.Instance.PagesPath}.");
            else if (view is Page)
            {
                if (viewType.FullName.IndexOf(GlobalSetting.Instance.PagesPath) < 0)
                    viewName = viewType.FullName.Replace($".{GlobalSetting.Instance.PagesPath}.", $".{GlobalSetting.Instance.ViewModelPath}.");
                else
                    viewName = viewType.FullName.Replace($".{GlobalSetting.Instance.PagesPath}.", $".{GlobalSetting.Instance.ViewModelPath}.{GlobalSetting.Instance.PagesPath}.");
            }
            else if (view is ContentView)
                viewName = viewType.FullName.Replace($".{GlobalSetting.Instance.CustomControlsPath}.",
                    $".{GlobalSetting.Instance.ViewModelPath}.");
            else if (viewType.FullName.IndexOf("CustomControls") > 0)
                viewName = viewType.FullName.Replace($".{GlobalSetting.Instance.CustomControlsPath}.", $".{GlobalSetting.Instance.ViewModelPath}.{GlobalSetting.Instance.CustomControlsPath}.");

            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture,
                "{0}ViewModel, {1}", viewName, viewAssemblyName);

            if (!string.IsNullOrWhiteSpace(diffrenceViewModelName))
                viewModelName = viewModelName.Replace(viewType.Name + "ViewModel", diffrenceViewModelName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
                return;
            var viewModel = _container.Resolve(viewModelType);

            view.BindingContext = viewModel;
        }
        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
        /// <summary>
        /// Use GlobalSetting before Init
        /// </summary>
        /// <param name="assembly"></param>
        public static void Init<TViewModel>() where TViewModel : ViewModelBase
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .Where(t => t.Namespace.Contains(GlobalSetting.Instance.ViewModelPath));

            builder.RegisterGeneric(typeof(ApiRequest<>)).
                As(typeof(IApiRequest<>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(ApiRequestSelector<>)).
               As(typeof(IApiRequestSelector<>)).InstancePerDependency();


            //IMobileServiceBase

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .AssignableTo<IMobileServiceBase>()
                   .AsImplementedInterfaces()
                   .SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .AssignableTo<IApiServiceBase>()
                   .AsImplementedInterfaces()
                   .SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<ISQLiteServiceBase>()
                .AsImplementedInterfaces()
                .SingleInstance();

            //builder.RegisterGeneric(typeof(IServiceBase<>)).AsImplementedInterfaces().SingleInstance();

            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //       .AssignableTo<IServiceBase>()
            //       .AsImplementedInterfaces()
            //       .SingleInstance();

            if (_container != null)
                _container.Dispose();

            _container = builder.Build();

            var navigationService = Resolve<INavigationService>();
            navigationService.InitializeAsync<TViewModel>();
        }
    }
}
