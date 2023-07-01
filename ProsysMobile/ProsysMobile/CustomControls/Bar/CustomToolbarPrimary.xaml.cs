using System;
using System.Windows.Input;
using WiseDynamicMobile.Helper;
using ProsysMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Bar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomToolbarPrimary : PancakeView
    {
        /// <summary>
        /// IsPreviousButtonVisible
        /// </summary>
        public static readonly BindableProperty IsPreviousButtonVisibleProperty = BindableProperty.Create(nameof(IsPreviousButtonVisible),
            typeof(bool),
            typeof(CustomToolbarPrimary),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// IsButtonVisible
        /// </summary>
        public static readonly BindableProperty IsButtonVisibleProperty = BindableProperty.Create(nameof(IsButtonVisible),
            typeof(bool),
            typeof(CustomToolbarPrimary),
            true,
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
        /// Title Property
        /// Label text
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
            typeof(string),
            typeof(CustomToolbarPrimary),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// ImageProperty
        /// Image Source
        /// </summary>
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource),
            typeof(string),
            typeof(CustomToolbarPrimary),
            "ChevronLeftBlack",
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Text Property
        /// Button text
        /// </summary>
        public static readonly BindableProperty ButtonTextProperty = BindableProperty.Create(nameof(ButtonText),
            typeof(string),
            typeof(CustomToolbarPrimary),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// IsButtonVisible
        /// </summary>
        public bool IsButtonVisible
        {
            get
            {
                return (bool)GetValue(IsButtonVisibleProperty);
            }
            set
            {
                SetValue(IsButtonVisibleProperty, value);
            }
        }

        /// <summary>
        /// IsPreviousButtonVisible
        /// </summary>
        public bool IsPreviousButtonVisible
        {
            get
            {
                return (bool)GetValue(IsPreviousButtonVisibleProperty);
            }
            set
            {
                SetValue(IsPreviousButtonVisibleProperty, value);
            }
        }

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
        /// Placeholder
        /// </summary>
        public string ButtonText
        {
            get
            {
                return (string)GetValue(ButtonTextProperty);
            }

            set
            {
                SetValue(ButtonTextProperty, value);
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
        /// Previous Clicked
        /// </summary>
        public event EventHandler<EventArgs> ItemPreviousClicked;

        /// <summary>
        /// Entry Focused
        /// </summary>
        public event EventHandler<EventArgs> ButtonClicked;

        public CustomToolbarPrimary()
        {
            InitializeComponent();

            ItemTitle.Text = Title;
            ItemButton.Text = ButtonText;
            ItemButtonPrevious.Source = ImageSource;
            ItemButton.IsVisible = IsButtonVisible;

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
            else if (propertyName == ButtonTextProperty.PropertyName)
                ItemButton.Text = ButtonText;
            else if (propertyName == IsOnlineProperty.PropertyName)
                IsOnlineChangeDesign();
            else if (propertyName == IsButtonVisibleProperty.PropertyName)
                ItemButton.IsVisible = IsButtonVisible;
            else if (propertyName == IsPreviousButtonVisibleProperty.PropertyName)
                ItemButtonPrevious.IsVisible = IsPreviousButtonVisible;
        }

        /// <summary>
        /// Back Button Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ItemButtonPrevious_Clicked(object sender,EventArgs e)
        {
            if (ItemPreviousClicked != null)
            {
                ItemPreviousClicked(sender, e);
            }
        }

        /// <summary>
        /// Button Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ItemButton_Clicked(object sender, EventArgs e)
        {
            if (ButtonClicked != null)
            {
                ButtonClicked(sender, e);
            }
        }

        /// <summary>
        /// Change Design for IsOnline
        /// </summary>
        void IsOnlineChangeDesign()
        {
            Application.Current.Resources.TryGetValue("Green.Base", out var green);
            Application.Current.Resources.TryGetValue("Red.Base", out var red);

            ItemBoxViewConnectionStatus.BackgroundColor = IsOnline ? (Color)green : (Color)red;
            ItemLabelConnectionStatus.Text = IsOnline ? "Çevrimiçi" : "Çevrimdışı";
        }

        public ICommand ConnectionStausChangedCommand => new Command(async () =>
        {
            IsOnline = GlobalSetting.Instance.IsInternetConnectionAvailable;
        });
    }
}