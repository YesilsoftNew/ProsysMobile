using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.Services.Bluetooth;
using ProsysMobile.Services.Dialog;
using ProsysMobile.Services.Navigation;
using ProsysMobile.ViewModels.Base;
using ProsysMobile.ViewModels.Pages.Main.Settings;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Bar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomBluetoothToolbar : PancakeView
    {
        /// <summary>
        /// The type property.
        /// </summary>
        public static readonly BindableProperty TypeProperty = BindableProperty.Create(
            nameof(Type),
            typeof(Models.CommonModels.Enums.enCustomBluetoothToolbar),
            typeof(CustomBluetoothToolbar),
            Models.CommonModels.Enums.enCustomBluetoothToolbar.ConnectedToDevice,
            BindingMode.TwoWay);

        /// <summary>
        /// The cell's Type
        /// </summary>
        public Models.CommonModels.Enums.enCustomBluetoothToolbar Type
        {
            get
            {
                return (Models.CommonModels.Enums.enCustomBluetoothToolbar)GetValue(TypeProperty);
            }
            set
            {
                SetValue(TypeProperty, value);
            }
        }


        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        public CustomBluetoothToolbar()
        {
            InitializeComponent();

            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();

            if (GlobalSetting.Instance.isBluetoothConnected && !GlobalSetting.Instance.BluetoothGetDataRunning)
            {
                GlobalSetting.Instance.BluetoothGetDataRunning = true;
                DependencyService.Get<IBluetoothService>().GetDeviceData();
            }

            MessagingCenter.Subscribe<object, enCustomBluetoothToolbar>("CustomBluetoothToolbar", "BluetoothConnectionStausChanged", async (sender, arg) =>
            {
                try
                {
                    ConnectionStausChangedCommand.Execute(arg);
                }
                catch (Exception ex)
                {
                    WiseLogger.Instance.CrashLog(ex);
                }
            });

            if (GlobalSetting.Instance.isBluetoothConnected)
                Type = enCustomBluetoothToolbar.ConnectedToDevice;
            else
                Type = enCustomBluetoothToolbar.NotConnectedToDevice;

            ItemBorder.GestureRecognizers.Add(new TapGestureRecognizer(ItemBorder_Clicked));
        }

        /// <summary>
        /// Change Property Function
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TypeProperty.PropertyName)
                changeDesign();
        }

        /// <summary>
        /// Change design
        /// </summary>
        void changeDesign()
        {
            switch (Type)
            {
                case Models.CommonModels.Enums.enCustomBluetoothToolbar.ConnectedToDevice:
                    Application.Current.Resources.TryGetValue("Views.Bluetooth.Connect", out var connectStyle);
                    Application.Current.Resources.TryGetValue("Views.Bluetooth.ConnectLabel", out var connectLabelStyle);

                    changeControlProperties(connectStyle as Style, connectLabelStyle as Style, "Cihaza Bağlı", "BluetoothGreen", null, false);

                    break;
                case Models.CommonModels.Enums.enCustomBluetoothToolbar.Connecting:
                    Application.Current.Resources.TryGetValue("Views.Bluetooth.Connecting", out var connectingStyle);
                    Application.Current.Resources.TryGetValue("Views.Bluetooth.ConnectingLabel", out var connectingLabelStyle);

                    changeControlProperties(connectingStyle as Style, connectingLabelStyle as Style, "Bağlanıyor, Tekrar Deneyin", "Loader", null, false);

                    break;
                case Models.CommonModels.Enums.enCustomBluetoothToolbar.ConnectionFailed:
                    Application.Current.Resources.TryGetValue("Views.Bluetooth.ConnectionFailed", out var connectionFailedStyle);
                    Application.Current.Resources.TryGetValue("Views.Bluetooth.ConnectionFailedLabel", out var connectionFailedLabelStyle);

                    changeControlProperties(connectionFailedStyle as Style, connectionFailedLabelStyle as Style, "Cihaz Bağlanılamadı, Tekrar Deneyin", "AlertTriangle", "BluetoothChevronRightRed", true);

                    break;
                case Models.CommonModels.Enums.enCustomBluetoothToolbar.ConnectionSuccessful:
                    Application.Current.Resources.TryGetValue("Views.Bluetooth.ConnectionSuccessful", out var connectionSuccessfulStyle);
                    Application.Current.Resources.TryGetValue("Views.Bluetooth.ConnectionSuccessfulLabel", out var connectionSuccessfulLabelStyle);

                    changeControlProperties(connectionSuccessfulStyle as Style, connectionSuccessfulLabelStyle as Style, "Bağlantı Başarılı", "CheckCircleGreen", null, false);

                    break;
                case Models.CommonModels.Enums.enCustomBluetoothToolbar.NotConnectedToDevice:
                    Application.Current.Resources.TryGetValue("Views.Bluetooth.NotConnect", out var notConnectStyle);
                    Application.Current.Resources.TryGetValue("Views.Bluetooth.NotConnectLabel", out var notConnectLabelStyle);

                    changeControlProperties(notConnectStyle as Style, notConnectLabelStyle as Style, "Cihaza Bağlı Değil", "BluetoothRed", "BluetoothChevronRightBlack", true);

                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Change Control Properties
        /// </summary>
        /// <param name="mainStyle"></param>
        /// <param name="labelStyle"></param>
        /// <param name="labelText"></param>
        /// <param name="itemImageSource"></param>
        /// <param name="itemImage2Source"></param>
        /// <param name="itemImage2IsVisibility"></param>
        void changeControlProperties(Style mainStyle, Style labelStyle, string labelText, string itemImageSource, string itemImage2Source, bool itemImage2IsVisibility)
        {
            ItemBorder.Style = mainStyle;
            ItemLabel.Style = labelStyle;
            ItemLabel.Text = labelText;
            ItemImage2.IsVisible = itemImage2IsVisibility;
            ItemImage.Source = itemImageSource;
            if (!string.IsNullOrEmpty(itemImage2Source)) ItemImage2.Source = itemImage2Source;
        }

        public ICommand ConnectionStausChangedCommand => new Command(async (arg) =>
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (arg != null && arg is enCustomBluetoothToolbar customBluetoothToolbar)
                    Type = customBluetoothToolbar;
            });
        });

        async void ItemBorder_Clicked(View arg1, object arg2)
        {
            try
            {
                ClickConnectBle();
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        async Task ClickConnectBle()
        {
            try
            {
                // burda ble baglantısını yapıyorum yada disconnect ediyorum
                if (GlobalSetting.Instance.isBluetoothConnected)
                    DependencyService.Get<IBluetoothService>().DisconnectDevice();
                else
                {
                    // TODO: baglı cihaz yoksa bs ac sectir
                    if (GlobalSetting.Instance.bluetoothDevice is null)
                    {
                        DialogService.WarningToastMessage("Kayıtlı bluetooth cihazı bulunamadı! Lütfen bluetooth cihaz ayarlarını yapınız.");
                        Type = enCustomBluetoothToolbar.NotConnectedToDevice;

                        NavigationModel<BluetoothDevicesPageViewParamModel> navigationModel = new NavigationModel<BluetoothDevicesPageViewParamModel>
                        {
                            Model = new BluetoothDevicesPageViewParamModel(),

                            ClosedPageEventCommand = BluetoothDevicesPage_ClosedPageEventCommand
                        };

                        NavigationService.NavigateToBackdropAsync<BluetoothDevicesPageViewModel>(navigationModel);
                        return;
                    }

                    if (DependencyService.Get<IBluetoothService>().ConnectDevice())
                    {
                        GlobalSetting.Instance.BluetoothGetDataRunning = true;
                        DependencyService.Get<IBluetoothService>().GetDeviceData();
                    }
                    else
                        DialogService.WarningToastMessage(GlobalSetting.translateExtension.GetTranstlateValue("pgInspectionDetails_cs_MSJ22"));
                }

                if (GlobalSetting.Instance.isBluetoothConnected)
                    Type = enCustomBluetoothToolbar.ConnectedToDevice;
                else
                    Type = enCustomBluetoothToolbar.NotConnectedToDevice;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        public ICommand BluetoothDevicesPage_ClosedPageEventCommand => new Command(async (O) =>
        {
            try
            {

                if (DependencyService.Get<IBluetoothService>().ConnectDevice())
                {
                    GlobalSetting.Instance.BluetoothGetDataRunning = true;
                    DependencyService.Get<IBluetoothService>().GetDeviceData();
                }
                else
                    DialogService.WarningToastMessage(GlobalSetting.translateExtension.GetTranstlateValue("pgInspectionDetails_cs_MSJ22"));
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        });
    }
}