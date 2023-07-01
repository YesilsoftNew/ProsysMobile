using System;
using System.Windows.Input;
using WiseDynamicMobile.Helper;
using WiseMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace WiseMobile.CustomControls.Bar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomToolbarSecondary : PancakeView
    {
        /// <summary>
        /// RightButtonSource Property
        /// </summary>
        public static readonly BindableProperty IsRightButtonVisibleProperty = BindableProperty.Create(nameof(IsRightButtonVisible),
            typeof(bool),
            typeof(CustomToolbarSecondary),
            default(bool),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// RightButtonSource Property
        /// </summary>
        public static readonly BindableProperty RightButtonSourceProperty = BindableProperty.Create(nameof(RightButtonSource),
            typeof(string),
            typeof(CustomToolbarSecondary),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Is Online
        /// </summary>
        public static readonly BindableProperty IsOnlineProperty = BindableProperty.Create(nameof(IsOnline),
            typeof(bool),
            typeof(CustomToolbarSecondary),
            false,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Title Property
        /// Label text
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
            typeof(string),
            typeof(CustomToolbarSecondary),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// ImageProperty
        /// Image Source
        /// </summary>
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource),
            typeof(string),
            typeof(CustomToolbarSecondary),
            "ChevronLeftBlack",
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// IsRightButtonVisible
        /// </summary>
        public bool IsRightButtonVisible
        {
            get
            {
                return (bool)GetValue(IsRightButtonVisibleProperty);
            }
            set
            {
                SetValue(IsRightButtonVisibleProperty, value);
            }
        }

        /// <summary>
        /// RightButtonSource
        /// </summary>
        public string RightButtonSource
        {
            get
            {
                return (string)GetValue(RightButtonSourceProperty);
            }

            set
            {
                SetValue(RightButtonSourceProperty, value);
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
        public event EventHandler<EventArgs> RightButtonClicked;

        public CustomToolbarSecondary()
        {
            InitializeComponent();

            ItemTitle.Text = Title;
            ItemButtonPrevious.Source = ImageSource;
            ItemRightImageButton.Source = RightButtonSource;
            ItemRightImageButton.IsVisible = IsRightButtonVisible;

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
            {
                ItemTitle.Text = Title;
            }
            else if (propertyName == ImageSourceProperty.PropertyName)
            {
                ItemButtonPrevious.Source = ImageSource;
            }
            else if (propertyName == IsOnlineProperty.PropertyName)
            {
                IsOnlineChangeDesign();
            }
            else if (propertyName == RightButtonSourceProperty.PropertyName)
            {
                ItemRightImageButton.Source = RightButtonSource;
            }
            else if (propertyName == IsRightButtonVisibleProperty.PropertyName)
            {
                ItemRightImageButton.IsVisible = IsRightButtonVisible;
            }
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
        /// Change Design for IsOnline
        /// </summary>
        void IsOnlineChangeDesign()
        {
            Application.Current.Resources.TryGetValue("Green.Base", out var green);
            Application.Current.Resources.TryGetValue("Red.Base", out var red);

            ItemBoxViewConnectionStatus.BackgroundColor = IsOnline ? (Color)green : (Color)red;
            ItemLabelConnectionStatus.Text = IsOnline ? "Çevrimiçi" : "Çevrimdışı";
        }

        private async void ItemRightImageButton_ImageButtonClicked(object sender, EventArgs e)
        {
            if (RightButtonClicked != null)
            {
                RightButtonClicked(sender, e);
            }
        }

        public ICommand ConnectionStausChangedCommand => new Command(async () =>
        {
            IsOnline = GlobalSetting.Instance.IsInternetConnectionAvailable;
        });
    }
}