using System;
using System.Windows.Input;
using WiseDynamicMobile.Helper;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Bluetooth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Entry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomAirPressure : Grid
    {
        /// <summary>
        /// AirPressureDataInputTypeProperty
        /// </summary>
        public static readonly BindableProperty AirPressureDataInputTypeProperty = BindableProperty.Create(
            nameof(AirPressureDataInputType),
            typeof(enDataInputType),
            typeof(CustomTreadDeptChannelsTertiary),
            enDataInputType.ManuelMobile,
            BindingMode.TwoWay);
          
        /// <summary>
        /// IsTireHotValueProperty
        /// </summary>
        public static readonly BindableProperty IsTireHotValueProperty = BindableProperty.Create(
            nameof(IsTireHotValue),
            typeof(bool),
            typeof(CustomAirPressure),
            false,
            BindingMode.TwoWay);

        /// <summary>
        /// IsTireHotVisibleProperty
        /// </summary>
        public static readonly BindableProperty IsTireHotVisibleProperty = BindableProperty.Create(
            nameof(IsTireHotVisible),
            typeof(bool),
            typeof(CustomAirPressure),
            false,
            BindingMode.TwoWay);

        /// <summary>
        /// EntryTitle
        /// </summary>
        public static readonly BindableProperty EntryTitleProperty = BindableProperty.Create(
            nameof(EntryTitle),
            typeof(string),
            typeof(CustomAirPressure),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// EntryPlaceHolder
        /// </summary>
        public static readonly BindableProperty EntryPlaceHolderProperty = BindableProperty.Create(
            nameof(EntryPlaceHolder),
            typeof(string),
            typeof(CustomAirPressure),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// AirPressureText
        /// </summary>
        public static readonly BindableProperty AirPressureTextProperty = BindableProperty.Create(
            nameof(AirPressureText),
            typeof(string),
            typeof(CustomAirPressure),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// is bluetooth connected
        /// </summary>
        static readonly BindableProperty IsBluetoothConnectedProperty = BindableProperty.Create(
            nameof(IsBluetoothConnected),
            typeof(bool),
            typeof(CustomAirPressure),
            default(bool),
            BindingMode.OneWay);

        /// <summary>
        /// RecommendedAirPressureTextProperty
        /// </summary>
        public static readonly BindableProperty RecommendedAirPressureTextProperty = BindableProperty.Create(
            nameof(RecommendedAirPressureText),
            typeof(string),
            typeof(CustomAirPressure),
            default(string),
            BindingMode.OneWay);

        /// <summary>
        /// IsRecommendedAirPressureVisibleProperty
        /// </summary>
        public static readonly BindableProperty IsRecommendedAirPressureVisibleProperty = BindableProperty.Create(
            nameof(IsRecommendedAirPressureVisible),
            typeof(bool),
            typeof(CustomAirPressure),
            true,
            BindingMode.OneWay);

        /// <summary>
        /// Title Is Visible
        /// </summary>
        public static readonly BindableProperty IsTitleVisibleProperty = BindableProperty.Create(
            nameof(IsTitleVisible),
            typeof(bool),
            typeof(CustomAirPressure),
            true,
            BindingMode.OneWay);

        /// <summary>
        /// Title Text
        /// </summary>
        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
            nameof(TitleText),
            typeof(string),
            typeof(CustomAirPressure),
            default(string),
            BindingMode.OneWay);


        /// <summary>
        /// IsBluetoothWaitProperty
        /// </summary>
        public static readonly BindableProperty IsBluetoothWaitProperty = BindableProperty.Create(
            nameof(IsBluetoothWait),
            typeof(bool),
            typeof(CustomAirPressure),
            false,
            BindingMode.TwoWay);

        /// <summary>
        /// IsBluetoothNotUsedProperty
        /// </summary>
        public static readonly BindableProperty IsBluetoothNotUsedProperty = BindableProperty.Create(
            nameof(IsBluetoothNotUsed),
            typeof(bool),
            typeof(CustomAirPressure),
            false,
            BindingMode.TwoWay);


        /// <summary>
        /// IsTireHotVisible 
        /// </summary>
        public bool IsTireHotVisible
        {
            get
            {
                return (bool)GetValue(IsTireHotVisibleProperty);
            }

            set
            {
                SetValue(IsTireHotVisibleProperty, value);
            }
        }

        /// <summary>
        /// AirPressureDataInputType 
        /// </summary>
        public enDataInputType AirPressureDataInputType
        {
            get
            {
                return (enDataInputType)GetValue(AirPressureDataInputTypeProperty);
            }

            set
            {
                SetValue(AirPressureDataInputTypeProperty, value);
            }
        }

        /// <summary>
        /// IsTireHotValue 
        /// </summary>
        public bool IsTireHotValue
        {
            get
            {
                return (bool)GetValue(IsTireHotValueProperty);
            }

            set
            {
                SetValue(IsTireHotValueProperty, value);
            }
        }

        /// <summary>
        /// IsBluetoothWait 
        /// </summary>
        public bool IsBluetoothWait
        {
            get
            {
                return (bool)GetValue(IsBluetoothWaitProperty);
            }

            set
            {
                SetValue(IsBluetoothWaitProperty, value);
            }
        }

        /// <summary>
        /// IsBluetoothNotUsed 
        /// </summary>
        public bool IsBluetoothNotUsed
        {
            get
            {
                return (bool)GetValue(IsBluetoothNotUsedProperty);
            }

            set
            {
                SetValue(IsBluetoothNotUsedProperty, value);
            }
        }

        /// <summary>
        /// EntryTitle
        /// </summary>
        public string EntryTitle
        {
            get
            {
                return (string)GetValue(EntryTitleProperty);
            }
            set
            {
                SetValue(EntryTitleProperty, value);
            }
        }

        /// <summary>
        /// EntryPlaceHolder
        /// </summary>
        public string EntryPlaceHolder
        {
            get
            {
                return (string)GetValue(EntryPlaceHolderProperty);
            }

            set
            {
                SetValue(EntryPlaceHolderProperty, value);
            }
        }

        /// <summary>
        /// AirPressure Text
        /// </summary>
        public string AirPressureText
        {
            get
            {
                return (string)GetValue(AirPressureTextProperty);
            }

            set
            {
                SetValue(AirPressureTextProperty, value);
            }
        }

        /// <summary>
        /// IsBluetoothConnected
        /// </summary>
        bool IsBluetoothConnected
        {
            get
            {
                return (bool)GetValue(IsBluetoothConnectedProperty);
            }

            set
            {
                SetValue(IsBluetoothConnectedProperty, value);
            }
        }

        /// <summary>
        /// RecommendedAirPressureText
        /// </summary>
        public string RecommendedAirPressureText
        {
            get
            {
                return (string)GetValue(RecommendedAirPressureTextProperty);
            }

            set
            {
                SetValue(RecommendedAirPressureTextProperty, value);
            }
        }

        /// <summary>
        /// IsRecommendedAirPressureVisible
        /// </summary>
        public bool IsRecommendedAirPressureVisible
        {
            get
            {
                return (bool)GetValue(IsRecommendedAirPressureVisibleProperty);
            }

            set
            {
                SetValue(IsRecommendedAirPressureVisibleProperty, value);
            }
        }

        /// <summary>
        /// Title Visible
        /// </summary>
        public bool IsTitleVisible
        {
            get
            {
                return (bool)GetValue(IsTitleVisibleProperty);
            }

            set
            {
                SetValue(IsTitleVisibleProperty, value);
            }
        }

        /// <summary>
        /// Title Text
        /// </summary>
        public string TitleText
        {
            get
            {
                return (string)GetValue(TitleTextProperty);
            }

            set
            {
                SetValue(TitleTextProperty, value);
            }
        }

        /// <summary>
        /// AirPressure Focused
        /// </summary>
        public event EventHandler<FocusEventArgs> AirPressureFocused;

        /// <summary>
        /// AirPressure TextChanged
        /// </summary>
        public event EventHandler<TextChangedEventArgs> AirPressureTextChanged;

        /// <summary>
        /// IsHotTireSwitchToggled
        /// </summary>
        public event EventHandler<ToggledEventArgs> IsHotTireSwitchToggled;

        public CustomAirPressure()
        {
            InitializeComponent();

            try
            {
                txtAirPressure.PropertyChanged += txtAirPressure_PropertyChanged;
                swtAirPressure.PropertyChanged += swtAirPressure_PropertyChanged;

                lblTitle.Text = TitleText;
                lblTitle.IsVisible = IsTitleVisible;
                grdRecommendedAirPressure.IsVisible = IsRecommendedAirPressureVisible;
                swtAirPressure.IsVisible = IsTireHotVisible;
                txtAirPressure.Text = AirPressureText;

                if (!string.IsNullOrWhiteSpace(EntryPlaceHolder))
                    txtAirPressure.Placeholder = EntryPlaceHolder;

                if (!string.IsNullOrWhiteSpace(EntryTitle))
                    txtAirPressure.Title = EntryTitle;

                if (!IsBluetoothNotUsed) // ble kullanılmıcak denmis ise IsBluetoothConnected false set etmek yeterli 
                    IsBluetoothConnected = GlobalSetting.Instance.isBluetoothConnected;
                else
                    IsBluetoothConnected = false;

                checkBluetoothConnected();

                MessagingCenter.Subscribe<object, string>("CustomAirPressure", "BluetoothDataCommand", async (sender, arg) =>
                {
                    try
                    {
                        BluetoothDataCommand.Execute(TOOLS.ToString(arg));
                    }
                    catch (Exception ex)
                    {
                        WiseLogger.Instance.CrashLog(ex);
                    }
                });

                MessagingCenter.Subscribe<object, enCustomBluetoothToolbar>("CustomAirPressure", "BluetoothConnectionStausChanged", async (sender, arg) =>
                {
                    try
                    {
                        ConnectionStausChangedCommand.Execute(null);
                    }
                    catch (Exception ex)
                    {
                        WiseLogger.Instance.CrashLog(ex);
                    }
                });
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void txtAirPressure_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e != null && e.PropertyName == "Text" && sender != null && sender is CustomDecimalEntrySecondary customDecimalEntry)
                AirPressureText = customDecimalEntry.Text;
        }

        private void swtAirPressure_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e != null && e.PropertyName == "IsToggled" && sender != null && sender is CustomSwitch customSwitch)
                IsTireHotValue = customSwitch.IsToggled;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            try
            {
                if (propertyName == TitleTextProperty.PropertyName)
                    lblTitle.Text = TitleText;
                else if (propertyName == IsTitleVisibleProperty.PropertyName)
                    lblTitle.IsVisible = IsTitleVisible;
                else if (propertyName == IsRecommendedAirPressureVisibleProperty.PropertyName)
                    grdRecommendedAirPressure.IsVisible = IsRecommendedAirPressureVisible;
                else if (propertyName == IsBluetoothConnectedProperty.PropertyName)
                    checkBluetoothConnected();
                else if (propertyName == AirPressureTextProperty.PropertyName)
                    txtAirPressure.Text = AirPressureText;
                else if (propertyName == RecommendedAirPressureTextProperty.PropertyName)
                    lblRecommendedAirPressure.Text = RecommendedAirPressureText;
                else if (propertyName == IsTireHotValueProperty.PropertyName)
                    swtAirPressure.IsToggled = IsTireHotValue;
                else if (propertyName == IsTireHotVisibleProperty.PropertyName)
                    swtAirPressure.IsVisible = IsTireHotVisible;
                else if (propertyName == EntryPlaceHolderProperty.PropertyName)
                    txtAirPressure.Placeholder = EntryPlaceHolder;
                else if (propertyName == EntryTitleProperty.PropertyName)
                    txtAirPressure.Title = EntryTitle;
                else if (propertyName == IsBluetoothWaitProperty.PropertyName)
                    checkBluetoothConnected();
                else if (propertyName == IsBluetoothNotUsedProperty.PropertyName)
                {
                    if (!IsBluetoothNotUsed) // ble kullanılmıcak denmis ise IsBluetoothConnected false set etmek yeterli 
                        IsBluetoothConnected = GlobalSetting.Instance.isBluetoothConnected;
                    else
                        IsBluetoothConnected = false;
                }
                else if (propertyName.Equals("Renderer", StringComparison.OrdinalIgnoreCase))
                {
                    // burdan aldım bu renderer'i
                    // https://github.com/michaelstonis/xamarin.forms-renderer-property 
                    var rr = DependencyService.Get<IRendererResolver>();

                    if (rr.HasRenderer(this))
                    {
                        Unregister();

                        var parent = Parent;

                        while (parent?.Parent != null && !(parent is ContentPage))
                        {
                            parent = parent.Parent;
                        }

                        if (parent is ContentPage page)
                        {
                            _parentPage = page;
                            Register();
                        }
                    }
                    else
                        Unregister();
                }
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        ContentPage _parentPage;
        bool _registered;

        void Register()
        {
            if (_parentPage != null && !_registered)
            {
                _parentPage.Disappearing += OnDisappearing;

                _registered = true;
            }
        }

        void Unregister()
        {
            if (_parentPage != null && _registered)
            {
                _parentPage.Disappearing -= OnDisappearing;

                _registered = false;
                _parentPage = null;
            }
        }

        private void OnDisappearing(object sender, EventArgs e)
        {
            OnDisappearing();
        }

        protected virtual void OnDisappearing()
        {
            try
            {
                MessagingCenter.Unsubscribe<object, string>("CustomAirPressure", "BluetoothDataCommand");
                MessagingCenter.Unsubscribe<object, enCustomBluetoothToolbar>("CustomAirPressure", "BluetoothConnectionStausChanged");
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void checkBluetoothConnected()
        {
            txtAirPressure.IsDisabled = IsBluetoothConnected;

            if (IsBluetoothConnected && !IsBluetoothWait) // beklemeye alındı ise disable rengi olcak
                txtAirPressure.EntryBorderColor = Color.FromHex("#C4E4C7");
            else
                txtAirPressure.IsDisabled = txtAirPressure.IsDisabled;

        }

        private void txtAirPressure_EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (AirPressureTextChanged != null)
                    AirPressureTextChanged(sender, e);

                AirPressureDataInputType = enDataInputType.ManuelMobile;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void txtAirPressure_EntryFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (IsBluetoothConnected)
                    txtAirPressure.EntryBorderColor = Color.FromHex("#C4E4C7");
                else
                    txtAirPressure.IsDisabled = txtAirPressure.IsDisabled;

                if (AirPressureFocused != null)
                    AirPressureFocused(sender, e);
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        public ICommand ConnectionStausChangedCommand => new Command(async () =>
        {
            if (!IsBluetoothNotUsed) // ble kullanılmıcak denmis ise IsBluetoothConnected false set etmek yeterli 
                IsBluetoothConnected = GlobalSetting.Instance.isBluetoothConnected;
            else
                IsBluetoothConnected = false;
        });

        private void swtAirPressure_SwitchToggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (IsHotTireSwitchToggled != null)
                    IsHotTireSwitchToggled(sender, e);
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        public Command BluetoothDataCommand => new Command<string>(async (s) =>
        {
            try
            {
                // IsBluetoothWait veri alma islemini beklemeye almıs demektir
                if (!IsBluetoothConnected || IsBluetoothWait) return;

                if (!GlobalSetting.Instance.BluetoothGetDataRunning && string.IsNullOrWhiteSpace(s))
                {
                    GlobalSetting.Instance.isBluetoothConnected = false;

                    MessagingCenter.Send<object, enCustomBluetoothToolbar>("CustomAirPressure", "BluetoothConnectionStausChanged", enCustomBluetoothToolbar.NotConnectedToDevice);
                    return;
                }

                bool Setdata = false;

                if (!string.IsNullOrWhiteSpace(s))
                {
                    if (s.Contains("P"))
                    {
                        string AirPressure = TOOLS.convertAirPressureForView(TOOLS.ToDecimalNull(TOOLS.convertBluetoothAirPressure(s))).ToString();

                        txtAirPressure.Text = AirPressure.ToString();

                        DependencyService.Get<IAudio>().PlayWavFile();

                        Setdata = true;

                        AirPressureDataInputType = enDataInputType.BluetoothProbe;
                    }
                    else if (s.Contains("T"))
                        Setdata = true;
                }

                if (!string.IsNullOrWhiteSpace(s) && !GlobalSetting.Instance.BluetoothGetDataRunning) // BluetoothGetDataRunning bunu bilerek kontrol ettim aynı anda birden fazla partial control'de ble veri cekme islemi yapılıyor olabilir (hava basıncı / dis derinligi)
                {
                    GlobalSetting.Instance.BluetoothGetDataRunning = true; // her data geldiğinde tekrardan 
                    DependencyService.Get<IBluetoothService>().GetDeviceData();
                }
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }); 
    }
}