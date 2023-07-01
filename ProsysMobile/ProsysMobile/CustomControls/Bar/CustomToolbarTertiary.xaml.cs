using Rg.Plugins.Popup.Extensions;
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
    public partial class CustomToolbarTertiary : PancakeView
    {  
        /// <summary>
        /// Title Property
        /// Label text
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
            typeof(string),
            typeof(CustomBackDropToolbarPrimary),
            default(string),
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
        /// Close Clicked
        /// </summary>
        public event EventHandler<EventArgs> ItemButtonCloseClicked;
         
        public CustomToolbarTertiary()
        {
            InitializeComponent();

            ItemTitle.Text = Title;

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
            else if (propertyName == IsOnlineProperty.PropertyName)
            {
                IsOnlineChangeDesign();
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