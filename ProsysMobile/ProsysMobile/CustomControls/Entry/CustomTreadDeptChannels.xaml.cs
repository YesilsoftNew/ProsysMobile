using System;
using System.Windows.Input;
using WiseDynamicMobile.Helper;
using WiseMobile.Helper;
using WiseMobile.Models.CommonModels.Enums;
using WiseMobile.Services.Bluetooth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WiseMobile.CustomControls.Entry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTreadDeptChannels : Grid
    {
        /// <summary>
        /// TreadDeptDataInputTypeProperty
        /// </summary>
        public static readonly BindableProperty TreadDeptDataInputTypeProperty = BindableProperty.Create(
            nameof(TreadDeptDataInputType),
            typeof(enDataInputType),
            typeof(CustomTreadDeptChannels),
            enDataInputType.ManuelMobile,
            BindingMode.TwoWay);

        /// <summary>
        /// ChannelCount
        /// </summary>
        public static readonly BindableProperty ChannelCountProperty = BindableProperty.Create(
            nameof(ChannelCount),
            typeof(int),
            typeof(CustomTreadDeptChannels),
            4,
            BindingMode.OneWay);

        /// <summary>
        /// IsOldChannel1Visible
        /// </summary>
        public static readonly BindableProperty IsOldChannel1VisibleProperty = BindableProperty.Create(
            nameof(IsOldChannel1Visible),
            typeof(bool),
            typeof(CustomTreadDeptChannels),
            true,
            BindingMode.OneWay);

        /// <summary>
        /// IsOldChannel2Visible
        /// </summary>
        public static readonly BindableProperty IsOldChannel2VisibleProperty = BindableProperty.Create(
            nameof(IsOldChannel2Visible),
            typeof(bool),
            typeof(CustomTreadDeptChannels),
            true,
            BindingMode.OneWay);

        /// <summary>
        /// IsOldChannel3Visible
        /// </summary>
        public static readonly BindableProperty IsOldChannel3VisibleProperty = BindableProperty.Create(
            nameof(IsOldChannel3Visible),
            typeof(bool),
            typeof(CustomTreadDeptChannels),
            true,
            BindingMode.OneWay);

        /// <summary>
        /// IsOldChannel4Visible
        /// </summary>
        public static readonly BindableProperty IsOldChannel4VisibleProperty = BindableProperty.Create(
            nameof(IsOldChannel4Visible),
            typeof(bool),
            typeof(CustomTreadDeptChannels),
            true,
            BindingMode.OneWay);

        /// <summary>
        /// Channel4Text
        /// </summary>
        public static readonly BindableProperty Channel4TextProperty = BindableProperty.Create(
            nameof(Channel4Text),
            typeof(string),
            typeof(CustomTreadDeptChannels),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// Channel3Text
        /// </summary>
        public static readonly BindableProperty Channel3TextProperty = BindableProperty.Create(
            nameof(Channel3Text),
            typeof(string),
            typeof(CustomTreadDeptChannels),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// Channel2Text
        /// </summary>
        public static readonly BindableProperty Channel2TextProperty = BindableProperty.Create(
            nameof(Channel2Text),
            typeof(string),
            typeof(CustomTreadDeptChannels),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// Channel1Text
        /// </summary>
        public static readonly BindableProperty Channel1TextProperty = BindableProperty.Create(
            nameof(Channel1Text),
            typeof(string),
            typeof(CustomTreadDeptChannels),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// is bluetooth connected
        /// </summary>
        static readonly BindableProperty IsBluetoothConnectedProperty = BindableProperty.Create(
            nameof(IsBluetoothConnected),
            typeof(bool),
            typeof(CustomTreadDeptChannels),
            default(bool),
            BindingMode.OneWay);

        /// <summary>
        /// OriginalTreadDepth Text
        /// </summary>
        public static readonly BindableProperty OriginalTreadDepthTextProperty = BindableProperty.Create(
            nameof(OriginalTreadDepthText),
            typeof(string),
            typeof(CustomTreadDeptChannels),
            default(string),
            BindingMode.OneWay);

        /// <summary>
        /// RemovalTreadDepth Text
        /// </summary>
        public static readonly BindableProperty RemovalTreadDepthTextProperty = BindableProperty.Create(
            nameof(RemovalTreadDepthText),
            typeof(string),
            typeof(CustomTreadDeptChannels),
            default(string),
            BindingMode.OneWay);

        /// <summary>
        /// IsRmvTreadDepthVisible
        /// </summary>
        public static readonly BindableProperty IsRmvTreadDepthVisibleProperty = BindableProperty.Create(
            nameof(IsRmvTreadDepthVisible),
            typeof(bool),
            typeof(CustomTreadDeptChannels),
            true,
            BindingMode.OneWay);

        /// <summary>
        /// IsOrjTreadDepthVisible
        /// </summary>
        public static readonly BindableProperty IsOrjTreadDepthVisibleProperty = BindableProperty.Create(
            nameof(IsOrjTreadDepthVisible),
            typeof(bool),
            typeof(CustomTreadDeptChannels),
            true,
            BindingMode.OneWay);

        /// <summary>
        /// TreadDepthsVisibleProperty
        /// </summary>
        public static readonly BindableProperty TreadDepthsVisibleProperty = BindableProperty.Create(
            nameof(TreadDepthsVisible),
            typeof(bool),
            typeof(CustomTreadDeptChannels),
            true,
            BindingMode.OneWay);

        /// <summary>
        /// OldChannel4 Text
        /// </summary>
        public static readonly BindableProperty OldChannel4TextProperty = BindableProperty.Create(
            nameof(OldChannel4Text),
            typeof(string),
            typeof(CustomTreadDeptChannels),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// OldChannel3 Text
        /// </summary>
        public static readonly BindableProperty OldChannel3TextProperty = BindableProperty.Create(
            nameof(OldChannel3Text),
            typeof(string),
            typeof(CustomTreadDeptChannels),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// OldChannel2 Text
        /// </summary>
        public static readonly BindableProperty OldChannel2TextProperty = BindableProperty.Create(
            nameof(OldChannel2Text),
            typeof(string),
            typeof(CustomTreadDeptChannels),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// OldChannel1 Text
        /// </summary>
        public static readonly BindableProperty OldChannel1TextProperty = BindableProperty.Create(
            nameof(OldChannel1Text),
            typeof(string),
            typeof(CustomTreadDeptChannels),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// Title Is Visible
        /// </summary>
        public static readonly BindableProperty IsTitleVisibleProperty = BindableProperty.Create(
            nameof(IsTitleVisible),
            typeof(bool),
            typeof(CustomTreadDeptChannels),
            true,
            BindingMode.OneWay);

        /// <summary>
        /// Title Text
        /// </summary>
        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
            nameof(TitleText),
            typeof(string),
            typeof(CustomTreadDeptChannels),
            default(string),
            BindingMode.OneWay);

        /// <summary>
        /// Is Disabled
        /// </summary>
        public static readonly BindableProperty IsDisabledProperty = BindableProperty.Create(
            nameof(IsDisabled),
            typeof(bool),
            typeof(CustomTreadDeptChannels),
            false,
            BindingMode.OneWay);

        /// <summary>
        /// TreadDepthControlProperty
        /// </summary>
        public static readonly BindableProperty TreadDepthControlProperty = BindableProperty.Create(
            nameof(TreadDepthControl),
            typeof(bool),
            typeof(CustomTreadDeptChannels),
            true,
            BindingMode.OneWay);

        /// <summary>
        /// SelectChannelIndexProperty
        /// </summary>
        public static readonly BindableProperty SelectChannelIndexProperty = BindableProperty.Create(
            nameof(SelectChannelIndex),
            typeof(int),
            typeof(CustomTreadDeptChannels),
            0,
            BindingMode.TwoWay);

        /// <summary>
        /// IsBluetoothNotUsedProperty
        /// </summary>
        public static readonly BindableProperty IsBluetoothNotUsedProperty = BindableProperty.Create(
            nameof(IsBluetoothNotUsed),
            typeof(bool),
            typeof(CustomTreadDeptChannels),
            false,
            BindingMode.TwoWay);

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
        /// IsDisabled 
        /// </summary>
        public bool IsDisabled
        {
            get
            {
                return (bool)GetValue(IsDisabledProperty);
            }

            set
            {
                SetValue(IsDisabledProperty, value);
            }
        }

        /// <summary>
        /// IsOldChannel1Visible 
        /// </summary>
        public bool IsOldChannel1Visible
        {
            get
            {
                return (bool)GetValue(IsOldChannel1VisibleProperty);
            }

            set
            {
                SetValue(IsOldChannel1VisibleProperty, value);
            }
        }

        /// <summary>
        /// ChannelCount 
        /// </summary>
        public int ChannelCount
        {
            get
            {
                return (int)GetValue(ChannelCountProperty);
            }

            set
            {
                SetValue(ChannelCountProperty, value);
            }
        }

        /// <summary>
        /// TreadDeptDataInputType 
        /// </summary>
        public enDataInputType TreadDeptDataInputType
        {
            get
            {
                return (enDataInputType)GetValue(TreadDeptDataInputTypeProperty);
            }

            set
            {
                SetValue(TreadDeptDataInputTypeProperty, value);
            }
        }

        /// <summary>
        /// IsOldChannel2Visible 
        /// </summary>
        public bool IsOldChannel2Visible
        {
            get
            {
                return (bool)GetValue(IsOldChannel2VisibleProperty);
            }

            set
            {
                SetValue(IsOldChannel2VisibleProperty, value);
            }
        }

        /// <summary>
        /// IsOldChannel3Visible 
        /// </summary>
        public bool IsOldChannel3Visible
        {
            get
            {
                return (bool)GetValue(IsOldChannel3VisibleProperty);
            }

            set
            {
                SetValue(IsOldChannel3VisibleProperty, value);
            }
        }

        /// <summary>
        /// IsOldChannel4Visible 
        /// </summary>
        public bool IsOldChannel4Visible
        {
            get
            {
                return (bool)GetValue(IsOldChannel4VisibleProperty);
            }

            set
            {
                SetValue(IsOldChannel4VisibleProperty, value);
            }
        }
        /// <summary>
        /// Channel4 Text
        /// </summary>
        public string Channel4Text
        {
            get
            {
                return (string)GetValue(Channel4TextProperty);
            }

            set
            {
                SetValue(Channel4TextProperty, value);
            }
        }

        /// <summary>
        /// Channel3 Text
        /// </summary>
        public string Channel3Text
        {
            get
            {
                return (string)GetValue(Channel3TextProperty);
            }

            set
            {
                SetValue(Channel3TextProperty, value);
            }
        }

        /// <summary>
        /// Channel2 Text
        /// </summary>
        public string Channel2Text
        {
            get
            {
                return (string)GetValue(Channel2TextProperty);
            }

            set
            {
                SetValue(Channel2TextProperty, value);
            }
        }

        /// <summary>
        /// Channel1 Text
        /// </summary>
        public string Channel1Text
        {
            get
            {
                return (string)GetValue(Channel1TextProperty);
            }

            set
            {
                SetValue(Channel1TextProperty, value);
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
        /// RemovalTreadDepth Text
        /// </summary>
        public string RemovalTreadDepthText
        {
            get
            {
                return (string)GetValue(RemovalTreadDepthTextProperty);
            }

            set
            {
                SetValue(RemovalTreadDepthTextProperty, value);
            }
        }

        /// <summary>
        /// OriginalTreadDepth Text
        /// </summary>
        public string OriginalTreadDepthText
        {
            get
            {
                return (string)GetValue(OriginalTreadDepthTextProperty);
            }

            set
            {
                SetValue(OriginalTreadDepthTextProperty, value);
            }
        }

        /// <summary>
        /// IsRmvTreadDepth Visible
        /// </summary>
        public bool IsRmvTreadDepthVisible
        {
            get
            {
                return (bool)GetValue(IsRmvTreadDepthVisibleProperty);
            }

            set
            {
                SetValue(IsRmvTreadDepthVisibleProperty, value);
            }
        }

        /// <summary>
        /// IsOrjTreadDepth Visible
        /// </summary>
        public bool IsOrjTreadDepthVisible
        {
            get
            {
                return (bool)GetValue(IsOrjTreadDepthVisibleProperty);
            }

            set
            {
                SetValue(IsOrjTreadDepthVisibleProperty, value);
            }
        }

        /// <summary>
        /// IsOrjTreadDepth Visible
        /// </summary>
        public bool TreadDepthsVisible
        {
            get
            {
                return (bool)GetValue(TreadDepthsVisibleProperty);
            }

            set
            {
                SetValue(TreadDepthsVisibleProperty, value);
            }
        }

        /// <summary>
        /// OldChannel4 Text
        /// </summary>
        public string OldChannel4Text
        {
            get
            {
                return (string)GetValue(OldChannel4TextProperty);
            }

            set
            {
                SetValue(OldChannel4TextProperty, value);
            }
        }

        /// <summary>
        /// OldChannel3 Text
        /// </summary>
        public string OldChannel3Text
        {
            get
            {
                return (string)GetValue(OldChannel3TextProperty);
            }

            set
            {
                SetValue(OldChannel3TextProperty, value);
            }
        }

        /// <summary>
        /// OldChannel2 Text
        /// </summary>
        public string OldChannel2Text
        {
            get
            {
                return (string)GetValue(OldChannel2TextProperty);
            }

            set
            {
                SetValue(OldChannel2TextProperty, value);
            }
        }

        /// <summary>
        /// OldChannel1 Text
        /// </summary>
        public string OldChannel1Text
        {
            get
            {
                return (string)GetValue(OldChannel1TextProperty);
            }

            set
            {
                SetValue(OldChannel1TextProperty, value);
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
        /// IsRegroove 
        /// </summary>
        public bool TreadDepthControl
        {
            get
            {
                return (bool)GetValue(TreadDepthControlProperty);
            }

            set
            {
                SetValue(TreadDepthControlProperty, value);
            }
        }

        /// <summary>
        /// SelectChannelIndex
        /// </summary>
        public int SelectChannelIndex
        {
            get
            {
                return (int)GetValue(SelectChannelIndexProperty);
            }
            set
            {
                SetValue(SelectChannelIndexProperty, value);
            }
        }

        /// <summary>
        /// Channel1 Focused
        /// </summary>
        public event EventHandler<FocusEventArgs> Channel1Focused;

        /// <summary>
        /// Channel2 Focused
        /// </summary>
        public event EventHandler<FocusEventArgs> Channel2Focused;

        /// <summary>
        /// Channel3 Focused
        /// </summary>
        public event EventHandler<FocusEventArgs> Channel3Focused;

        /// <summary>
        /// Channel4 Focused
        /// </summary>
        public event EventHandler<FocusEventArgs> Channel4Focused;

        /// <summary>
        /// Channel1 TextChanged
        /// </summary>
        public event EventHandler<TextChangedEventArgs> Channel1TextChanged;

        /// <summary>
        /// Channel2 TextChanged
        /// </summary>
        public event EventHandler<TextChangedEventArgs> Channel2TextChanged;

        /// <summary>
        /// Channel3 TextChanged
        /// </summary>
        public event EventHandler<TextChangedEventArgs> Channel3TextChanged;

        /// <summary>
        /// Channel4 TextChanged
        /// </summary>
        public event EventHandler<TextChangedEventArgs> Channel4TextChanged;

        public CustomTreadDeptChannels()
        {
            InitializeComponent();

            try
            {
                lblTitle.Text = TitleText;
                lblTitle.IsVisible = IsTitleVisible;
                grdRmvTreadDepth.IsVisible = IsRmvTreadDepthVisible;
                grdOrjTreadDepth.IsVisible = IsOrjTreadDepthVisible;
                grdTreadDepths.IsVisible = TreadDepthsVisible;

                lblOldChannel1.Text = OldChannel1Text;
                lblOldChannel2.Text = OldChannel2Text;
                lblOldChannel3.Text = OldChannel3Text;
                lblOldChannel4.Text = OldChannel4Text;

                txtChannel1.PropertyChanged += txtChannel1_PropertyChanged;
                txtChannel2.PropertyChanged += txtChannel2_PropertyChanged;
                txtChannel3.PropertyChanged += txtChannel3_PropertyChanged;
                txtChannel4.PropertyChanged += txtChannel4_PropertyChanged;

                if (!IsBluetoothNotUsed) // ble kullanılmıcak denmis ise IsBluetoothConnected false set etmek yeterli 
                    IsBluetoothConnected = GlobalSetting.Instance.isBluetoothConnected;
                else
                    IsBluetoothConnected = false;

                MessagingCenter.Subscribe<object, string>("CustomTreadDeptChannels", "BluetoothDataCommand", async (sender, arg) =>
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

                checkBluetoothConnected();

                PrepareChannelCount();

                MessagingCenter.Subscribe<object, enCustomBluetoothToolbar>("CustomTreadDeptChannels", "BluetoothConnectionStausChanged", async (sender, arg) =>
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

                SelectChannelIndex = 1;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void txtChannel1_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e != null && e.PropertyName == "Text" && sender != null && sender is CustomDecimalEntry customDecimalEntry)
                Channel1Text = customDecimalEntry.Text;
        }

        private void txtChannel2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e != null && e.PropertyName == "Text" && sender != null && sender is CustomDecimalEntry customDecimalEntry)
                Channel2Text = customDecimalEntry.Text;
        }

        private void txtChannel3_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e != null && e.PropertyName == "Text" && sender != null && sender is CustomDecimalEntry customDecimalEntry)
                Channel3Text = customDecimalEntry.Text;
        }

        private void txtChannel4_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e != null && e.PropertyName == "Text" && sender != null && sender is CustomDecimalEntry customDecimalEntry)
                Channel4Text = customDecimalEntry.Text;
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
                else if (propertyName == IsOrjTreadDepthVisibleProperty.PropertyName)
                    grdOrjTreadDepth.IsVisible = IsOrjTreadDepthVisible;
                else if (propertyName == TreadDepthsVisibleProperty.PropertyName)
                    grdTreadDepths.IsVisible = TreadDepthsVisible;
                else if (propertyName == IsRmvTreadDepthVisibleProperty.PropertyName)
                    grdRmvTreadDepth.IsVisible = IsRmvTreadDepthVisible;
                else if (propertyName == IsBluetoothConnectedProperty.PropertyName)
                    checkBluetoothConnected();
                else if (propertyName == OldChannel1TextProperty.PropertyName)
                    lblOldChannel1.Text = OldChannel1Text;
                else if (propertyName == OldChannel2TextProperty.PropertyName)
                    lblOldChannel2.Text = OldChannel2Text;
                else if (propertyName == OldChannel3TextProperty.PropertyName)
                    lblOldChannel3.Text = OldChannel3Text;
                else if (propertyName == OldChannel4TextProperty.PropertyName)
                    lblOldChannel4.Text = OldChannel4Text;
                else if (propertyName == Channel1TextProperty.PropertyName)
                    txtChannel1.Text = Channel1Text;
                else if (propertyName == Channel2TextProperty.PropertyName)
                    txtChannel2.Text = Channel2Text;
                else if (propertyName == Channel3TextProperty.PropertyName)
                    txtChannel3.Text = Channel3Text;
                else if (propertyName == Channel4TextProperty.PropertyName)
                    txtChannel4.Text = Channel4Text;
                else if (propertyName == IsDisabledProperty.PropertyName)
                {
                    if (IsBluetoothConnected)
                        checkBluetoothConnected();
                    else // ble baglantısı yok ise hic bsy yapmadan dsabled'ı ayarlayabiliriz
                    {
                        txtChannel1.IsDisabled = IsDisabled;
                        txtChannel2.IsDisabled = IsDisabled;
                        txtChannel3.IsDisabled = IsDisabled;
                        txtChannel4.IsDisabled = IsDisabled;
                    }
                }
                else if (propertyName == ChannelCountProperty.PropertyName)
                    PrepareChannelCount();
                else if (propertyName == SelectChannelIndexProperty.PropertyName)
                {
                    if (txtChannel1.IsFocused)
                        txtChannel1.EntryUnfocus = true;

                    if (txtChannel2.IsFocused)
                        txtChannel2.EntryUnfocus = true;

                    if (txtChannel3.IsFocused)
                        txtChannel3.EntryUnfocus = true;

                    if (txtChannel4.IsFocused)
                        txtChannel4.EntryUnfocus = true;

                    ClearChannelBackColor();

                    switch (SelectChannelIndex)
                    {
                        case 1:
                            txtChannel1.EntryFocus = true;
                            break;
                        case 2:
                            txtChannel2.EntryFocus = true;
                            break;
                        case 3:
                            txtChannel3.EntryFocus = true;
                            break;
                        case 4:
                            txtChannel4.EntryFocus = true;
                            break;
                    }
                }
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
                MessagingCenter.Unsubscribe<object, string>("CustomTreadDeptChannels", "BluetoothDataCommand");
                MessagingCenter.Unsubscribe<object, enCustomBluetoothToolbar>("CustomTreadDeptChannels", "BluetoothConnectionStausChanged");
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void checkBluetoothConnected()
        {
            txtChannel1.IsDisabled = IsBluetoothConnected;
            txtChannel2.IsDisabled = IsBluetoothConnected;
            txtChannel3.IsDisabled = IsBluetoothConnected;
            txtChannel4.IsDisabled = IsBluetoothConnected;

            ClearChannelBackColor();

            if (IsBluetoothConnected)
            {
                switch (SelectChannelIndex)
                {
                    case 1:
                        txtChannel1.EntryBorderColor = Color.FromHex("#C4E4C7");
                        break;
                    case 2:
                        txtChannel2.EntryBorderColor = Color.FromHex("#C4E4C7");
                        break;
                    case 3:
                        txtChannel3.EntryBorderColor = Color.FromHex("#C4E4C7");
                        break;
                    case 4:
                        txtChannel4.EntryBorderColor = Color.FromHex("#C4E4C7");
                        break;
                }
            }
        }

        private void txtChannel1_EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(OldChannel1Text) && TreadDepthControl)
                {
                    if ((TOOLS.convertTreadDepthUnitForDb(TOOLS.ToDecimal(Channel1Text)) - TOOLS.ToDecimal(OldChannel1Text)) <= GlobalSetting.Instance.threadDepthTolerance)
                        txtChannel1.EntryTextColor = Color.FromHex("#000000");
                    else
                        txtChannel1.EntryTextColor = Color.FromHex("#FF0000");
                }

                if (Channel1TextChanged != null)
                    Channel1TextChanged(sender, e);

                Channel1DataInputType = enDataInputType.ManuelMobile;

                #region nextChannel
                if (txtChannel1.IsDisabled) return;

                if (string.IsNullOrWhiteSpace(txtChannel1.Text)) return;

                int xIndex = txtChannel1.Text.ToString().IndexOf(System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString());

                if (xIndex < 0) return;

                if (txtChannel1.Text.ToString().Substring(xIndex + 1).Length > 0)
                    SelectChannelIndex = 2;
                #endregion 
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void txtChannel2_EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(OldChannel2Text) && TreadDepthControl)
                {
                    if ((TOOLS.convertTreadDepthUnitForDb(TOOLS.ToDecimal(Channel2Text)) - TOOLS.ToDecimal(OldChannel2Text)) <= GlobalSetting.Instance.threadDepthTolerance)
                        txtChannel2.EntryTextColor = Color.FromHex("#000000");
                    else
                        txtChannel2.EntryTextColor = Color.FromHex("#FF0000");
                }

                if (Channel2TextChanged != null)
                    Channel2TextChanged(sender, e);

                Channel2DataInputType = enDataInputType.ManuelMobile;

                #region nextChannel
                if (txtChannel2.IsDisabled) return;

                if (string.IsNullOrWhiteSpace(txtChannel2.Text)) return;

                int xIndex = txtChannel2.Text.ToString().IndexOf(System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString());

                if (xIndex < 0) return;

                if (txtChannel2.Text.ToString().Substring(xIndex + 1).Length > 0)
                    SelectChannelIndex = 3;
                #endregion 
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void txtChannel3_EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(OldChannel3Text) && TreadDepthControl)
                {
                    if ((TOOLS.convertTreadDepthUnitForDb(TOOLS.ToDecimal(Channel3Text)) - TOOLS.ToDecimal(OldChannel3Text)) <= GlobalSetting.Instance.threadDepthTolerance)
                        txtChannel3.EntryTextColor = Color.FromHex("#000000");
                    else
                        txtChannel3.EntryTextColor = Color.FromHex("#FF0000");
                }

                if (Channel3TextChanged != null)
                    Channel3TextChanged(sender, e);

                Channel3DataInputType = enDataInputType.ManuelMobile;

                #region nextChannel
                if (txtChannel3.IsDisabled) return;

                if (string.IsNullOrWhiteSpace(txtChannel3.Text)) return;

                int xIndex = txtChannel3.Text.ToString().IndexOf(System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString());

                if (xIndex < 0) return;

                if (txtChannel3.Text.ToString().Substring(xIndex + 1).Length > 0)
                    SelectChannelIndex = 4;
                #endregion 
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void txtChannel4_EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(OldChannel4Text) && TreadDepthControl)
                {
                    if ((TOOLS.convertTreadDepthUnitForDb(TOOLS.ToDecimal(Channel4Text)) - TOOLS.ToDecimal(OldChannel4Text)) <= GlobalSetting.Instance.threadDepthTolerance)
                        txtChannel4.EntryTextColor = Color.FromHex("#000000");
                    else
                        txtChannel4.EntryTextColor = Color.FromHex("#FF0000");
                }

                if (Channel4TextChanged != null)
                    Channel4TextChanged(sender, e);

                Channel4DataInputType = enDataInputType.ManuelMobile;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void txtChannel1_EntryFocused(object sender, FocusEventArgs e)
        {
            try
            {
                SelectChannelIndex = 1;

                if (IsBluetoothConnected)
                {
                    ClearChannelBackColor();
                    txtChannel1.EntryBorderColor = Color.FromHex("#C4E4C7");
                }

                if (Channel1Focused != null)
                    Channel1Focused(sender, e);
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void txtChannel2_EntryFocused(object sender, FocusEventArgs e)
        {
            try
            {
                SelectChannelIndex = 2;

                if (IsBluetoothConnected)
                {
                    ClearChannelBackColor();
                    txtChannel2.EntryBorderColor = Color.FromHex("#C4E4C7");
                }

                if (Channel2Focused != null)
                {
                    Channel2Focused(sender, e);
                }
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void txtChannel3_EntryFocused(object sender, FocusEventArgs e)
        {
            try
            {
                SelectChannelIndex = 3;

                if (IsBluetoothConnected)
                {
                    ClearChannelBackColor();
                    txtChannel3.EntryBorderColor = Color.FromHex("#C4E4C7");
                }

                if (Channel3Focused != null)
                {
                    Channel3Focused(sender, e);
                }
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void txtChannel4_EntryFocused(object sender, FocusEventArgs e)
        {
            try
            {
                SelectChannelIndex = 4;

                if (IsBluetoothConnected)
                {
                    ClearChannelBackColor();
                    txtChannel4.EntryBorderColor = Color.FromHex("#C4E4C7");
                }

                if (Channel4Focused != null)
                {
                    Channel4Focused(sender, e);
                }
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void ClearChannelBackColor()
        {
            try
            {
                // color otomatik ayarlanıyor zaten 
                txtChannel1.IsDisabled = txtChannel1.IsDisabled;
                txtChannel2.IsDisabled = txtChannel2.IsDisabled;
                txtChannel3.IsDisabled = txtChannel3.IsDisabled;
                txtChannel4.IsDisabled = txtChannel4.IsDisabled;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void PrepareChannelCount()
        {
            switch (ChannelCount)
            {
                case 1:
                    lblChannelNo2.IsVisible = false;
                    lblChannelNo3.IsVisible = false;
                    lblChannelNo4.IsVisible = false;

                    txtChannel2.IsVisible = false;
                    txtChannel3.IsVisible = false;
                    txtChannel4.IsVisible = false;

                    lblOldChannel2.IsVisible = false;
                    lblOldChannel3.IsVisible = false;
                    lblOldChannel4.IsVisible = false;
                    break;

                case 2:
                    lblChannelNo2.IsVisible = true;
                    lblChannelNo3.IsVisible = false;
                    lblChannelNo4.IsVisible = false;

                    txtChannel2.IsVisible = true;
                    txtChannel3.IsVisible = false;
                    txtChannel4.IsVisible = false;

                    lblOldChannel2.IsVisible = true;
                    lblOldChannel3.IsVisible = false;
                    lblOldChannel4.IsVisible = false;
                    break;

                case 3:
                    lblChannelNo2.IsVisible = true;
                    lblChannelNo3.IsVisible = true;
                    lblChannelNo4.IsVisible = false;

                    txtChannel2.IsVisible = true;
                    txtChannel3.IsVisible = true;
                    txtChannel4.IsVisible = false;

                    lblOldChannel2.IsVisible = true;
                    lblOldChannel3.IsVisible = true;
                    lblOldChannel4.IsVisible = false;
                    break;

                case 4:
                    lblChannelNo2.IsVisible = true;
                    lblChannelNo3.IsVisible = true;
                    lblChannelNo4.IsVisible = true;

                    txtChannel2.IsVisible = true;
                    txtChannel3.IsVisible = true;
                    txtChannel4.IsVisible = true;

                    lblOldChannel2.IsVisible = true;
                    lblOldChannel3.IsVisible = true;
                    lblOldChannel4.IsVisible = true;
                    break;
            }
        }

        public ICommand ConnectionStausChangedCommand => new Command(async () =>
        {
            if (!IsBluetoothNotUsed) // ble kullanılmıcak denmis ise IsBluetoothConnected false set etmek yeterli 
                IsBluetoothConnected = GlobalSetting.Instance.isBluetoothConnected;
            else
                IsBluetoothConnected = false;
        });

        public Command BluetoothDataCommand => new Command<string>(async (s) =>
        {
            try
            {
                if (!IsBluetoothConnected) return;

                if (!GlobalSetting.Instance.BluetoothGetDataRunning && string.IsNullOrWhiteSpace(s))
                {
                    GlobalSetting.Instance.isBluetoothConnected = false;

                    MessagingCenter.Send<object, enCustomBluetoothToolbar>("CustomTreadDeptChannels", "BluetoothConnectionStausChanged", enCustomBluetoothToolbar.NotConnectedToDevice);
                    return;
                }

                bool Setdata = false;


                if (!string.IsNullOrWhiteSpace(s))
                {
                    if (s.Contains("T"))
                    {
                        string Channel = TOOLS.convertTreadDepthUnitForView(TOOLS.ToDecimalNull(TOOLS.convertBluetoothChannel(s))).ToString();

                        if (SelectChannelIndex == 1)
                        {
                            txtChannel1.Text = Channel;
                            Setdata = true;

                            Channel1DataInputType = enDataInputType.BluetoothProbe;
                        }
                        else if (SelectChannelIndex == 2)
                        {
                            txtChannel2.Text = Channel;
                            Setdata = true;

                            Channel2DataInputType = enDataInputType.BluetoothProbe;
                        }
                        else if (SelectChannelIndex == 3)
                        {
                            txtChannel3.Text = Channel;
                            Setdata = true;

                            Channel3DataInputType = enDataInputType.BluetoothProbe;
                        }
                        else if (SelectChannelIndex == 4)
                        {
                            txtChannel4.Text = Channel;
                            Setdata = true;

                            Channel4DataInputType = enDataInputType.BluetoothProbe;
                        }

                        if (Setdata)
                            DependencyService.Get<IAudio>().PlayWavFile();
                    }
                    else if (s.Contains("P"))
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

        #region DataInput
        enDataInputType _channel1DataInputType;
        enDataInputType Channel1DataInputType
        {
            get
            {
                return _channel1DataInputType;
            }
            set
            {
                _channel1DataInputType = value;
                setTreadDeptDataInputType();
            }
        }

        enDataInputType _channel2DataInputType;
        enDataInputType Channel2DataInputType
        {
            get
            {
                return _channel2DataInputType;
            }
            set
            {
                _channel2DataInputType = value;
                setTreadDeptDataInputType();
            }
        }

        enDataInputType _channel3DataInputType;
        enDataInputType Channel3DataInputType
        {
            get
            {
                return _channel3DataInputType;
            }
            set
            {
                _channel3DataInputType = value;
                setTreadDeptDataInputType();
            }
        }

        enDataInputType _channel4DataInputType;
        enDataInputType Channel4DataInputType
        {
            get
            {
                return _channel4DataInputType;
            }
            set
            {
                _channel4DataInputType = value;
                setTreadDeptDataInputType();
            }
        }

        void setTreadDeptDataInputType()
        {
            try
            {
                // dolu alanların hepsi ble'den dolmussa BluetoothProbe set ediyorum onun dısındaki diger tüm durumlar ManuelMobile
                if (!string.IsNullOrWhiteSpace(txtChannel1.Text) && Channel1DataInputType == enDataInputType.ManuelMobile)
                {
                    TreadDeptDataInputType = enDataInputType.ManuelMobile;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(txtChannel2.Text) && Channel2DataInputType == enDataInputType.ManuelMobile)
                {
                    TreadDeptDataInputType = enDataInputType.ManuelMobile;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(txtChannel3.Text) && Channel3DataInputType == enDataInputType.ManuelMobile)
                {
                    TreadDeptDataInputType = enDataInputType.ManuelMobile;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(txtChannel4.Text) && Channel4DataInputType == enDataInputType.ManuelMobile)
                {
                    TreadDeptDataInputType = enDataInputType.ManuelMobile;
                    return;
                }

                TreadDeptDataInputType = enDataInputType.BluetoothProbe;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }
        #endregion
    }
}