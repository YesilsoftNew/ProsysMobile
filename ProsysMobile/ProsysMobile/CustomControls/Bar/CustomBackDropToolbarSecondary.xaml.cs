using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using WiseDynamicMobile.Helper;
using WiseMobile.Helper;
using WiseMobile.Pages.System;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace WiseMobile.CustomControls.Bar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomBackDropToolbarSecondary : PancakeView
    {
        /// <summary>
        /// Title Property
        /// Label text
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
            typeof(string),
            typeof(CustomBackDropToolbarSecondary),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Close Page When Button Is Clicked Property
        /// ClosePageWhenButtonIsClicked
        /// </summary>
        public static readonly BindableProperty ClosePageWhenButtonIsClickedProperty = BindableProperty.Create(nameof(Title),
            typeof(bool),
            typeof(CustomBackDropToolbarPrimary),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// ImageProperty
        /// Image Source
        /// </summary>
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource),
            typeof(string),
            typeof(CustomBackDropToolbarSecondary),
            "ChevronLeftBlack",
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Is Online
        /// </summary>
        public static readonly BindableProperty IsOnlineProperty = BindableProperty.Create(nameof(IsOnline),
            typeof(bool),
            typeof(CustomToolbarPrimary),
            false,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// IsOnline
        /// </summary>
        public bool IsOnline
        {
            get
            {
                return (bool)GetValue(IsOnlineProperty);
            }
            set
            {
                SetValue(IsOnlineProperty, value);
            }
        }

        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }

            set
            {
                SetValue(TitleProperty, value);
            }
        }

        /// <summary>
        /// ClosePageWhenButtonIsClicked
        /// </summary>
        public bool ClosePageWhenButtonIsClicked
        {
            get
            {
                return (bool)GetValue(ClosePageWhenButtonIsClickedProperty);
            }

            set
            {
                SetValue(ClosePageWhenButtonIsClickedProperty, value);
            }
        }

        /// <summary>
        /// ImageSource
        /// </summary>
        public string ImageSource
        {
            get
            {
                return (string)GetValue(ImageSourceProperty);
            }

            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }

        /// <summary>
        /// Close Clicked
        /// </summary>
        public event EventHandler<EventArgs> ItemButtonCloseClicked;

        public CustomBackDropToolbarSecondary()
        {
            InitializeComponent();

            ItemTitle.Text = Title;
            ItemButtonPrevious.Source = ImageSource;

            TOOLS.ConnectionStausChanged = ConnectionStausChangedCommand;
            IsOnline = GlobalSetting.Instance.IsInternetConnectionAvailable;

            IsOnlineChangeDesign();
        }

        /// <summary>
        /// Property Change
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
                ItemTitle.Text = Title;
            else if (propertyName == ImageSourceProperty.PropertyName)
                ItemButtonPrevious.Source = ImageSource;
            else if (propertyName == IsOnlineProperty.PropertyName)
                IsOnlineChangeDesign();
        }

        /// <summary>
        /// Back Button Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ItemButtonClose_ImageButtonClicked(object sender, EventArgs e)
        {
            if (ClosePageWhenButtonIsClicked)
            {
                try
                {
                    if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                    for (int i = PopupNavigation.Instance.PopupStack.Count - 1; i >= 0; i--)
                    {
                        if (PopupNavigation.Instance.PopupStack[i].ToString() == new ToastMessagePage().ToString())
                            continue;

                        PopupNavigation.Instance.RemovePageAsync(PopupNavigation.Instance.PopupStack[i], true);

                        break;
                    }

                    //Application.Current.MainPage.Navigation.PopPopupAsync();

                    DoubleTapping.ResumeTap();
                }
                catch (Exception ex)
                {
                    WiseLogger.Instance.CrashLog(ex);
                    DoubleTapping.ResumeTap();
                    return;
                }
            }

            if (ItemButtonCloseClicked != null)
                ItemButtonCloseClicked(sender, e);
        }

        /// <summary>
        /// Change Design for IsOnline
        /// </summary>
        void IsOnlineChangeDesign()
        {
            try
            {
                Application.Current.Resources.TryGetValue("Green.Base", out var green);
                Application.Current.Resources.TryGetValue("Red.Base", out var red);

                ItemBoxViewConnectionStatus.BackgroundColor = IsOnline ? (Color)green : (Color)red;
                ItemLabelConnectionStatus.Text = IsOnline ? "Çevrimiçi" : "Çevrimdışı";
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        public ICommand ConnectionStausChangedCommand => new Command(async () =>
        {
            IsOnline = GlobalSetting.Instance.IsInternetConnectionAvailable;
        });
    }
}