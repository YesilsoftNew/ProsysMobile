using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WiseDynamicMobile.Helper;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.CustomModel;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.Services.Bluetooth;
using ProsysMobile.Services.Navigation;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Base;
using ProsysMobile.ViewModels.Pages.System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTireChannelLayout : ContentView
    {
        protected readonly INavigationService NavigationService;
        ITireActionSQLiteService _tireActionSQLiteService;

        public CustomTireChannelLayout()
        {
            try
            {
                InitializeComponent();

                NavigationService = ViewModelLocator.Resolve<INavigationService>();
                _tireActionSQLiteService = ViewModelLocator.Resolve<ITireActionSQLiteService>();

                MessagingCenter.Subscribe<object, string>("CustomTireChannelLayout", "BluetoothDataCommand", async (sender, arg) =>
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

                MessagingCenter.Subscribe<object, enCustomBluetoothToolbar>("CustomTireChannelLayout", "BluetoothConnectionStausChanged", async (sender, arg) =>
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


        #region XamlElement
        StackLayout stcTreadDepths = new StackLayout { VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand, WidthRequest = 360, HeightRequest = 175, BackgroundColor = Color.White };

        Grid grdTreadDepth = new Grid
        {
            VerticalOptions = LayoutOptions.FillAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            RowSpacing = 0,
            HeightRequest = 175,
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(5, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(2, GridUnitType.Star) }
            }
        };

        StackLayout stcTreadDepthUpside = new StackLayout { BackgroundColor = Color.Transparent, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand }; //grid.row
        StackLayout stcTreadDepthUnderside = new StackLayout { BackgroundColor = Color.Transparent, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

        AbsoluteLayout absTreadDepthUpside = new AbsoluteLayout { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
        AbsoluteLayout absTreadDepthUnderside = new AbsoluteLayout { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

        Image ImgArrowDown1 = new Image { Source = "ArrowDown" };
        Image ImgArrowDown2 = new Image { Source = "ArrowDown" };
        Image ImgArrowDown3 = new Image { Source = "ArrowDown" };
        Image ImgArrowDown4 = new Image { Source = "ArrowDown" };

        BoxView bxChannel1 = new BoxView { BackgroundColor = Color.Black, CornerRadius = new CornerRadius(4, 4, 0, 0) };
        BoxView bxChannel2 = new BoxView { BackgroundColor = Color.Black, CornerRadius = new CornerRadius(4, 4, 0, 0) };
        BoxView bxChannel3 = new BoxView { BackgroundColor = Color.Black, CornerRadius = new CornerRadius(4, 4, 0, 0) };
        BoxView bxChannel4 = new BoxView { BackgroundColor = Color.Black, CornerRadius = new CornerRadius(4, 4, 0, 0) };
        BoxView bxChannel5 = new BoxView { BackgroundColor = Color.Black, CornerRadius = new CornerRadius(4, 4, 0, 0) };

        BoxView bxChannelNo1 = new BoxView { BackgroundColor = Color.White, CornerRadius = 40 };
        BoxView bxChannelNo2 = new BoxView { BackgroundColor = Color.White, CornerRadius = 40 };
        BoxView bxChannelNo3 = new BoxView { BackgroundColor = Color.White, CornerRadius = 40 };
        BoxView bxChannelNo4 = new BoxView { BackgroundColor = Color.White, CornerRadius = 40 };

        Label lblChannelNo1 = new Label { Text = "1", VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Blue };
        Label lblChannelNo2 = new Label { Text = "2", VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Blue };
        Label lblChannelNo3 = new Label { Text = "3", VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Blue };
        Label lblChannelNo4 = new Label { Text = "4", VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.Blue };

        Label lblLastChannel1 = new Label { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, BackgroundColor = Color.Transparent, FontSize = 12, Text = "-", TextColor = Color.Black };
        Label lblLastChannel2 = new Label { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, BackgroundColor = Color.Transparent, FontSize = 12, Text = "-", TextColor = Color.Black };
        Label lblLastChannel3 = new Label { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, BackgroundColor = Color.Transparent, FontSize = 12, Text = "-", TextColor = Color.Black };
        Label lblLastChannel4 = new Label { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, BackgroundColor = Color.Transparent, FontSize = 12, Text = "-", TextColor = Color.Black };

        Label lblChannel1 = new Label { BackgroundColor = Color.Transparent, Text = "", FontSize = 12, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
        Label lblChannel2 = new Label { BackgroundColor = Color.Transparent, Text = "", FontSize = 12, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
        Label lblChannel3 = new Label { BackgroundColor = Color.Transparent, Text = "", FontSize = 12, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
        Label lblChannel4 = new Label { BackgroundColor = Color.Transparent, Text = "", FontSize = 12, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };


        Image ImgUnderTire1 = new Image { Source = "UnderTire1" };
        Image ImgUnderTire2 = new Image { Source = "UnderTire2" };
        Image ImgUnderTire3 = new Image { Source = "UnderTire3" };
        Image ImgUnderTire4 = new Image { Source = "UnderTire4" };

        Image ImgFender1R = new Image { Source = "FenderRight1" };
        Image ImgFender2R = new Image { Source = "FenderRight2" };
        Image ImgFender3R = new Image { Source = "FenderRight3" };
        Image ImgFender4R = new Image { Source = "FenderRight4" };
        #endregion

        /// <summary>
        /// IsBluetoothNotUsedProperty
        /// </summary>
        public static readonly BindableProperty IsBluetoothNotUsedProperty = BindableProperty.Create(
            nameof(IsBluetoothNotUsed),
            typeof(bool),
            typeof(CustomTireChannelLayout),
            false,
            BindingMode.TwoWay);

        /// <summary>
        /// The Tire Channel Layout Data Property.
        /// </summary>
        public static readonly BindableProperty TireChannelLayoutDataProperty = BindableProperty.Create(
            nameof(TireChannelLayoutData),
            typeof(CustomTireChannelLayoutData),
            typeof(CustomTireChannelLayout),
            default(CustomTireChannelLayoutData),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// is bluetooth connected
        /// </summary>
        static readonly BindableProperty IsBluetoothConnectedProperty = BindableProperty.Create(
            nameof(IsBluetoothConnected),
            typeof(bool),
            typeof(CustomTireChannelLayout),
            default(bool),
            BindingMode.OneWay);

        ///// <summary>
        ///// TreadDeptDataInputTypeProperty
        ///// </summary>
        //public static readonly BindableProperty TreadDeptDataInputTypeProperty = BindableProperty.Create(
        //    nameof(TreadDeptDataInputType),
        //    typeof(enDataInputType),
        //    typeof(CustomTireChannelLayout),
        //    enDataInputType.ManuelMobile,
        //    BindingMode.TwoWay);

        ///// <summary>
        ///// AirPressureDataInputTypeProperty
        ///// </summary>
        //public static readonly BindableProperty AirPressureDataInputTypeProperty = BindableProperty.Create(
        //    nameof(AirPressureDataInputType),
        //    typeof(enDataInputType),
        //    typeof(CustomTireChannelLayout),
        //    enDataInputType.ManuelMobile,
        //    BindingMode.TwoWay);

        ///// <summary>
        ///// TreadDeptDataInputType 
        ///// </summary>
        //public enDataInputType TreadDeptDataInputType
        //{
        //    get
        //    {
        //        return (enDataInputType)GetValue(TreadDeptDataInputTypeProperty);
        //    }

        //    set
        //    {
        //        SetValue(TreadDeptDataInputTypeProperty, value);
        //    }
        //}

        ///// <summary>
        ///// AirPressureDataInputType 
        ///// </summary>
        //public enDataInputType AirPressureDataInputType
        //{
        //    get
        //    {
        //        return (enDataInputType)GetValue(AirPressureDataInputTypeProperty);
        //    }

        //    set
        //    {
        //        SetValue(AirPressureDataInputTypeProperty, value);
        //    }
        //}

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

        public CustomTireChannelLayoutData TireChannelLayoutData
        {
            get
            {
                return (CustomTireChannelLayoutData)GetValue(TireChannelLayoutDataProperty);
            }
            set
            {
                SetValue(TireChannelLayoutDataProperty, value);
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

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            try
            {
                if (propertyName == TireChannelLayoutDataProperty.PropertyName)
                {
                    var tireChannelLayoutData = TireChannelLayoutData;
                    tireChannelLayoutData.FocusedChannelCommand = FocusedChannelCommand;
                    tireChannelLayoutData.CreateCustomTireChannelCommand = CreateCustomTireChannelCommand;
                    tireChannelLayoutData.UpdateCustomTireChannelCommand = UpdateCustomTireChannelCommand;
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
                MessagingCenter.Unsubscribe<object, string>("CustomTireChannelLayout", "BluetoothDataCommand");
                MessagingCenter.Unsubscribe<object, enCustomBluetoothToolbar>("CustomTireChannelLayout", "BluetoothConnectionStausChanged");
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }


        public ICommand FocusedChannelCommand => new Command(async () =>
        {
            ArrowPrepare();
        });

        public ICommand CreateCustomTireChannelCommand => new Command(async () =>
        {
            FillTireLayout();
        });

        public ICommand UpdateCustomTireChannelCommand => new Command(async () =>
        {
            try
            {
                if (TireChannelLayoutData.tireAction is null)
                    TireChannelLayoutData.tireAction = new TireAction();

                lblChannelNo1.Text = "1";
                lblChannelNo2.Text = "2";
                lblChannelNo3.Text = "3";
                lblChannelNo4.Text = "4";

                //if (TireChannelLayoutData.treadDepthInputDirection == enTreadDepthInputDirection.DistanIce)
                //{
                //    lblChannelNo1.Text = "1";
                //    lblChannelNo2.Text = "2";
                //    lblChannelNo3.Text = "3";
                //    lblChannelNo4.Text = "4";
                //}
                //else
                //{
                //    if (TireChannelLayoutData.ChannelCount == 4)
                //    {
                //        lblChannelNo1.Text = "4";
                //        lblChannelNo2.Text = "3";
                //        lblChannelNo3.Text = "2";
                //        lblChannelNo4.Text = "1";
                //    }
                //    else if (TireChannelLayoutData.ChannelCount == 3)
                //    {
                //        lblChannelNo1.Text = "3";
                //        lblChannelNo2.Text = "2";
                //        lblChannelNo3.Text = "1";
                //    }
                //    else if (TireChannelLayoutData.ChannelCount == 2)
                //    {
                //        lblChannelNo1.Text = "2";
                //        lblChannelNo2.Text = "1";
                //    }
                //    else if (TireChannelLayoutData.ChannelCount == 1)
                //        lblChannelNo4.Text = "1";
                //}

                if (TireChannelLayoutData.treadDepthInputDirection == enTreadDepthInputDirection.DistanIce)
                {
                    lblChannel1.Text = TireChannelLayoutData.tireAction.channel1 is null ? "" : TireChannelLayoutData.tireAction.channel1.ToString();
                    lblChannel2.Text = TireChannelLayoutData.tireAction.channel2 is null ? "" : TireChannelLayoutData.tireAction.channel2.ToString();
                    lblChannel3.Text = TireChannelLayoutData.tireAction.channel3 is null ? "" : TireChannelLayoutData.tireAction.channel3.ToString();
                    lblChannel4.Text = TireChannelLayoutData.tireAction.channel4 is null ? "" : TireChannelLayoutData.tireAction.channel4.ToString();

                    lblLastChannel1.Text = TireChannelLayoutData.lastTireAction.channel1 is null ? "-" : TireChannelLayoutData.lastTireAction.channel1.ToString();
                    lblLastChannel2.Text = TireChannelLayoutData.lastTireAction.channel2 is null ? "-" : TireChannelLayoutData.lastTireAction.channel2.ToString();
                    lblLastChannel3.Text = TireChannelLayoutData.lastTireAction.channel3 is null ? "-" : TireChannelLayoutData.lastTireAction.channel3.ToString();
                    lblLastChannel4.Text = TireChannelLayoutData.lastTireAction.channel4 is null ? "-" : TireChannelLayoutData.lastTireAction.channel4.ToString();

                    Channel1Prepare(TireChannelLayoutData.tireAction.channel1, TireChannelLayoutData.lastTireAction.channel1);
                    Channel2Prepare(TireChannelLayoutData.tireAction.channel2, TireChannelLayoutData.lastTireAction.channel2);
                    Channel3Prepare(TireChannelLayoutData.tireAction.channel3, TireChannelLayoutData.lastTireAction.channel3);
                    Channel4Prepare(TireChannelLayoutData.tireAction.channel4, TireChannelLayoutData.lastTireAction.channel4);
                }
                else
                {
                    lblChannel1.Text = TireChannelLayoutData.tireAction.channel4 is null ? "" : TireChannelLayoutData.tireAction.channel4.ToString();
                    lblChannel2.Text = TireChannelLayoutData.tireAction.channel3 is null ? "" : TireChannelLayoutData.tireAction.channel3.ToString();
                    lblChannel3.Text = TireChannelLayoutData.tireAction.channel2 is null ? "" : TireChannelLayoutData.tireAction.channel2.ToString();
                    lblChannel4.Text = TireChannelLayoutData.tireAction.channel1 is null ? "" : TireChannelLayoutData.tireAction.channel1.ToString();

                    lblLastChannel1.Text = TireChannelLayoutData.lastTireAction.channel4 is null ? "-" : TireChannelLayoutData.lastTireAction.channel4.ToString();
                    lblLastChannel2.Text = TireChannelLayoutData.lastTireAction.channel3 is null ? "-" : TireChannelLayoutData.lastTireAction.channel3.ToString();
                    lblLastChannel3.Text = TireChannelLayoutData.lastTireAction.channel2 is null ? "-" : TireChannelLayoutData.lastTireAction.channel2.ToString();
                    lblLastChannel4.Text = TireChannelLayoutData.lastTireAction.channel1 is null ? "-" : TireChannelLayoutData.lastTireAction.channel1.ToString();

                    Channel1Prepare(TireChannelLayoutData.tireAction.channel4, TireChannelLayoutData.lastTireAction.channel4);
                    Channel2Prepare(TireChannelLayoutData.tireAction.channel3, TireChannelLayoutData.lastTireAction.channel3);
                    Channel3Prepare(TireChannelLayoutData.tireAction.channel2, TireChannelLayoutData.lastTireAction.channel2);
                    Channel4Prepare(TireChannelLayoutData.tireAction.channel1, TireChannelLayoutData.lastTireAction.channel1);
                }

                ArrowPrepare();
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        });

        async Task FillTireLayout()
        {
            try
            {
                Content = null;

                stcTreadDepths.Children.Clear();
                grdTreadDepth.Children.Clear();
                stcTreadDepthUpside.Children.Clear();
                stcTreadDepthUnderside.Children.Clear();
                absTreadDepthUpside.Children.Clear();
                absTreadDepthUnderside.Children.Clear();

                lblChannelNo1.GestureRecognizers.Add(new TapGestureRecognizer(ChannelNo1Click));
                lblChannelNo2.GestureRecognizers.Add(new TapGestureRecognizer(ChannelNo2Click));
                lblChannelNo3.GestureRecognizers.Add(new TapGestureRecognizer(ChannelNo3Click));
                lblChannelNo4.GestureRecognizers.Add(new TapGestureRecognizer(ChannelNo4Click));

                stcTreadDepths.Children.Add(grdTreadDepth);
                grdTreadDepth.Children.Add(stcTreadDepthUpside, 0, 0);
                grdTreadDepth.Children.Add(stcTreadDepthUnderside, 0, 1);
                stcTreadDepthUpside.Children.Add(absTreadDepthUpside);
                stcTreadDepthUnderside.Children.Add(absTreadDepthUnderside);

                switch (TireChannelLayoutData.ChannelCount)
                {
                    case 1:
                        absTreadDepthUpside.Children.Add(ImgFender1R, new Rectangle(.5, 1, 1, 1), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(ImgUnderTire1, new Rectangle(.5, 1, 1, 1), AbsoluteLayoutFlags.All);

                        absTreadDepthUpside.Children.Add(ImgArrowDown1, new Rectangle(.5, .2, .15, .18), AbsoluteLayoutFlags.All);

                        absTreadDepthUpside.Children.Add(bxChannel1, new Rectangle(.39, .6, .11, 0.1), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(bxChannel2, new Rectangle(.611, .6, .11, 0.1), AbsoluteLayoutFlags.All);

                        absTreadDepthUpside.Children.Add(bxChannelNo1, new Rectangle(.5, .79, 25, 25), AbsoluteLayoutFlags.PositionProportional);

                        absTreadDepthUpside.Children.Add(lblChannelNo1, new Rectangle(.500, .84, 40, 40), AbsoluteLayoutFlags.PositionProportional);

                        lblChannelNo1.Text = "1";

                        absTreadDepthUnderside.Children.Add(lblLastChannel1, new Rectangle(.5, .9, .15, .5), AbsoluteLayoutFlags.All);

                        absTreadDepthUnderside.Children.Add(lblChannel1, new Rectangle(.5, -.8, .15, .8), AbsoluteLayoutFlags.All);

                        break;

                    case 2:
                        absTreadDepthUpside.Children.Add(ImgFender2R, new Rectangle(.5, 1, 1, 1), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(ImgUnderTire2, new Rectangle(.5, 1, 1, 1), AbsoluteLayoutFlags.All);

                        absTreadDepthUpside.Children.Add(ImgArrowDown1, new Rectangle(.38, .2, .15, .18), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(ImgArrowDown2, new Rectangle(.61, .2, .15, .18), AbsoluteLayoutFlags.All);

                        absTreadDepthUpside.Children.Add(bxChannel1, new Rectangle(.285, .6, .12, 0.1), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(bxChannel2, new Rectangle(.5, .6, .12, 0.1), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(bxChannel3, new Rectangle(.712, .6, .12, 0.1), AbsoluteLayoutFlags.All);

                        absTreadDepthUpside.Children.Add(bxChannelNo1, new Rectangle(.4, .79, 25, 25), AbsoluteLayoutFlags.PositionProportional);
                        absTreadDepthUpside.Children.Add(bxChannelNo2, new Rectangle(.6, .79, 25, 25), AbsoluteLayoutFlags.PositionProportional);

                        absTreadDepthUpside.Children.Add(lblChannelNo1, new Rectangle(.393, .84, 40, 40), AbsoluteLayoutFlags.PositionProportional);
                        absTreadDepthUpside.Children.Add(lblChannelNo2, new Rectangle(.605, .84, 40, 40), AbsoluteLayoutFlags.PositionProportional);

                        if (TireChannelLayoutData.treadDepthInputDirection == enTreadDepthInputDirection.IctenDisa)
                        {
                            lblChannelNo1.Text = "2";
                            lblChannelNo2.Text = "1";
                        }
                        else
                        {
                            lblChannelNo1.Text = "1";
                            lblChannelNo2.Text = "2";
                        }

                        absTreadDepthUnderside.Children.Add(lblLastChannel1, new Rectangle(.39, .9, .15, .5), AbsoluteLayoutFlags.All);
                        absTreadDepthUnderside.Children.Add(lblLastChannel2, new Rectangle(.6, .9, .15, .5), AbsoluteLayoutFlags.All);

                        absTreadDepthUnderside.Children.Add(lblChannel1, new Rectangle(.39, -.8, .15, .8), AbsoluteLayoutFlags.All);
                        absTreadDepthUnderside.Children.Add(lblChannel2, new Rectangle(.6, -.8, .15, .8), AbsoluteLayoutFlags.All);

                        break;

                    case 3:
                        absTreadDepthUpside.Children.Add(ImgFender3R, new Rectangle(.5, 1, 1, 1), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(ImgUnderTire3, new Rectangle(.5, 1, 1, 1), AbsoluteLayoutFlags.All);

                        absTreadDepthUpside.Children.Add(ImgArrowDown1, new Rectangle(.275, .2, .15, .18), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(ImgArrowDown2, new Rectangle(.5, .2, .15, .18), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(ImgArrowDown3, new Rectangle(.72, .2, .15, .18), AbsoluteLayoutFlags.All);

                        absTreadDepthUpside.Children.Add(bxChannel1, new Rectangle(.181, .6, .125, 0.1), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(bxChannel2, new Rectangle(.393, .6, .125, 0.1), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(bxChannel3, new Rectangle(.608, .6, .125, 0.1), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(bxChannel4, new Rectangle(.817, .6, .125, 0.1), AbsoluteLayoutFlags.All);

                        absTreadDepthUpside.Children.Add(bxChannelNo1, new Rectangle(.3, .79, 25, 25), AbsoluteLayoutFlags.PositionProportional);
                        absTreadDepthUpside.Children.Add(bxChannelNo2, new Rectangle(.5, .79, 25, 25), AbsoluteLayoutFlags.PositionProportional);
                        absTreadDepthUpside.Children.Add(bxChannelNo3, new Rectangle(.7, .79, 25, 25), AbsoluteLayoutFlags.PositionProportional);

                        absTreadDepthUpside.Children.Add(lblChannelNo1, new Rectangle(.292, .84, 40, 40), AbsoluteLayoutFlags.PositionProportional);
                        absTreadDepthUpside.Children.Add(lblChannelNo2, new Rectangle(.497, .84, 40, 40), AbsoluteLayoutFlags.PositionProportional);
                        absTreadDepthUpside.Children.Add(lblChannelNo3, new Rectangle(.707, .84, 40, 40), AbsoluteLayoutFlags.PositionProportional);


                        if (TireChannelLayoutData.treadDepthInputDirection == enTreadDepthInputDirection.IctenDisa)
                        {
                            lblChannelNo1.Text = "3";
                            lblChannelNo2.Text = "2";
                            lblChannelNo3.Text = "1";
                        }
                        else
                        {
                            lblChannelNo1.Text = "1";
                            lblChannelNo2.Text = "2";
                            lblChannelNo3.Text = "3";
                        }

                        absTreadDepthUnderside.Children.Add(lblLastChannel1, new Rectangle(.275, .9, .15, .5), AbsoluteLayoutFlags.All);
                        absTreadDepthUnderside.Children.Add(lblLastChannel2, new Rectangle(.5, .9, .15, .5), AbsoluteLayoutFlags.All);
                        absTreadDepthUnderside.Children.Add(lblLastChannel3, new Rectangle(.72, .9, .15, .5), AbsoluteLayoutFlags.All);

                        absTreadDepthUnderside.Children.Add(lblChannel1, new Rectangle(.275, -.8, .15, .8), AbsoluteLayoutFlags.All);
                        absTreadDepthUnderside.Children.Add(lblChannel2, new Rectangle(.5, -.8, .15, .8), AbsoluteLayoutFlags.All);
                        absTreadDepthUnderside.Children.Add(lblChannel3, new Rectangle(.72, -.8, .15, .8), AbsoluteLayoutFlags.All);

                        break;

                    case 4:
                        absTreadDepthUpside.Children.Add(ImgFender4R, new Rectangle(.5, 1, 1, 1), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(ImgUnderTire4, new Rectangle(.5, 1, 1, 1), AbsoluteLayoutFlags.All);

                        absTreadDepthUpside.Children.Add(ImgArrowDown1, new Rectangle(.21, .2, .12, .15), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(ImgArrowDown2, new Rectangle(.4, .2, .12, .15), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(ImgArrowDown3, new Rectangle(.595, .2, .12, .15), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(ImgArrowDown4, new Rectangle(.79, .2, .12, .15), AbsoluteLayoutFlags.All);

                        absTreadDepthUpside.Children.Add(bxChannel1, new Rectangle(.125, .6, .11, 0.1), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(bxChannel2, new Rectangle(.3125, .6, .11, 0.1), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(bxChannel3, new Rectangle(.5, .6, .11, 0.1), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(bxChannel4, new Rectangle(.6875, .6, .11, 0.1), AbsoluteLayoutFlags.All);
                        absTreadDepthUpside.Children.Add(bxChannel5, new Rectangle(.875, .6, .11, 0.1), AbsoluteLayoutFlags.All);

                        absTreadDepthUpside.Children.Add(bxChannelNo1, new Rectangle(.23, .79, 25, 25), AbsoluteLayoutFlags.PositionProportional);
                        absTreadDepthUpside.Children.Add(bxChannelNo2, new Rectangle(.41, .79, 25, 25), AbsoluteLayoutFlags.PositionProportional);
                        absTreadDepthUpside.Children.Add(bxChannelNo3, new Rectangle(.59, .79, 25, 25), AbsoluteLayoutFlags.PositionProportional);
                        absTreadDepthUpside.Children.Add(bxChannelNo4, new Rectangle(.77, .79, 25, 25), AbsoluteLayoutFlags.PositionProportional);

                        absTreadDepthUpside.Children.Add(lblChannelNo1, new Rectangle(.218, .84, 40, 40), AbsoluteLayoutFlags.PositionProportional);
                        absTreadDepthUpside.Children.Add(lblChannelNo2, new Rectangle(.408, .84, 40, 40), AbsoluteLayoutFlags.PositionProportional);
                        absTreadDepthUpside.Children.Add(lblChannelNo3, new Rectangle(.592, .84, 40, 40), AbsoluteLayoutFlags.PositionProportional);
                        absTreadDepthUpside.Children.Add(lblChannelNo4, new Rectangle(.778, .84, 40, 40), AbsoluteLayoutFlags.PositionProportional);

                        if (TireChannelLayoutData.treadDepthInputDirection == enTreadDepthInputDirection.IctenDisa)
                        {
                            lblChannelNo1.Text = "4";
                            lblChannelNo2.Text = "3";
                            lblChannelNo3.Text = "2";
                            lblChannelNo4.Text = "1";
                        }
                        else
                        {
                            lblChannelNo1.Text = "1";
                            lblChannelNo2.Text = "2";
                            lblChannelNo3.Text = "3";
                            lblChannelNo4.Text = "4";
                        }

                        absTreadDepthUnderside.Children.Add(lblLastChannel1, new Rectangle(.21, .9, .15, .5), AbsoluteLayoutFlags.All);
                        absTreadDepthUnderside.Children.Add(lblLastChannel2, new Rectangle(.4, .9, .15, .5), AbsoluteLayoutFlags.All);
                        absTreadDepthUnderside.Children.Add(lblLastChannel3, new Rectangle(.595, .9, .15, .5), AbsoluteLayoutFlags.All);
                        absTreadDepthUnderside.Children.Add(lblLastChannel4, new Rectangle(.79, .9, .15, .5), AbsoluteLayoutFlags.All);

                        absTreadDepthUnderside.Children.Add(lblChannel1, new Rectangle(.21, -.8, .15, .8), AbsoluteLayoutFlags.All);
                        absTreadDepthUnderside.Children.Add(lblChannel2, new Rectangle(.4, -.8, .15, .8), AbsoluteLayoutFlags.All);
                        absTreadDepthUnderside.Children.Add(lblChannel3, new Rectangle(.595, -.8, .15, .8), AbsoluteLayoutFlags.All);
                        absTreadDepthUnderside.Children.Add(lblChannel4, new Rectangle(.79, -.8, .15, .8), AbsoluteLayoutFlags.All);

                        break;
                }

                //absTreadDepthUpside.Children.Add(ImgFender4R, new Rectangle(.5, 1, 1, 1), AbsoluteLayoutFlags.All);
                //absTreadDepthUpside.Children.Add(ImgUnderTire4, new Rectangle(.5, 1, 1, 1), AbsoluteLayoutFlags.All);

                //absTreadDepthUpside.Children.Add(ImgArrowDown1, new Rectangle(.21, .2, .12, .15), AbsoluteLayoutFlags.All);
                //absTreadDepthUpside.Children.Add(ImgArrowDown2, new Rectangle(.4, .2, .12, .15), AbsoluteLayoutFlags.All);
                //absTreadDepthUpside.Children.Add(ImgArrowDown3, new Rectangle(.595, .2, .12, .15), AbsoluteLayoutFlags.All);
                //absTreadDepthUpside.Children.Add(ImgArrowDown4, new Rectangle(.79, .2, .12, .15), AbsoluteLayoutFlags.All);

                //absTreadDepthUpside.Children.Add(bxChannel1, new Rectangle(.125, .6, .11, 0.1), AbsoluteLayoutFlags.All);
                //absTreadDepthUpside.Children.Add(bxChannel2, new Rectangle(.3125, .6, .11, 0.1), AbsoluteLayoutFlags.All);
                //absTreadDepthUpside.Children.Add(bxChannel3, new Rectangle(.5, .6, .11, 0.1), AbsoluteLayoutFlags.All);
                //absTreadDepthUpside.Children.Add(bxChannel4, new Rectangle(.6875, .6, .11, 0.1), AbsoluteLayoutFlags.All);
                //absTreadDepthUpside.Children.Add(bxChannel5, new Rectangle(.875, .6, .11, 0.1), AbsoluteLayoutFlags.All);

                //absTreadDepthUpside.Children.Add(bxChannelNo1, new Rectangle(.23, .79, 25, 25), AbsoluteLayoutFlags.PositionProportional);
                //absTreadDepthUpside.Children.Add(bxChannelNo2, new Rectangle(.41, .79, 25, 25), AbsoluteLayoutFlags.PositionProportional);
                //absTreadDepthUpside.Children.Add(bxChannelNo3, new Rectangle(.59, .79, 25, 25), AbsoluteLayoutFlags.PositionProportional);
                //absTreadDepthUpside.Children.Add(bxChannelNo4, new Rectangle(.77, .79, 25, 25), AbsoluteLayoutFlags.PositionProportional);

                //absTreadDepthUpside.Children.Add(lblChannelNo1, new Rectangle(.218, .84, 40, 40), AbsoluteLayoutFlags.PositionProportional);
                //absTreadDepthUpside.Children.Add(lblChannelNo2, new Rectangle(.408, .84, 40, 40), AbsoluteLayoutFlags.PositionProportional);
                //absTreadDepthUpside.Children.Add(lblChannelNo3, new Rectangle(.592, .84, 40, 40), AbsoluteLayoutFlags.PositionProportional);
                //absTreadDepthUpside.Children.Add(lblChannelNo4, new Rectangle(.778, .84, 40, 40), AbsoluteLayoutFlags.PositionProportional);

                //if (TireChannelLayoutData.treadDepthInputDirection == enTreadDepthInputDirection.DistanIce)
                //{
                //    lblChannelNo1.Text = "1";
                //    lblChannelNo2.Text = "2";
                //    lblChannelNo3.Text = "3";
                //    lblChannelNo4.Text = "4";
                //}

                //absTreadDepthUnderside.Children.Add(lblLastChannel1, new Rectangle(.21, .9, .15, .5), AbsoluteLayoutFlags.All);
                //absTreadDepthUnderside.Children.Add(lblLastChannel2, new Rectangle(.4, .9, .15, .5), AbsoluteLayoutFlags.All);
                //absTreadDepthUnderside.Children.Add(lblLastChannel3, new Rectangle(.595, .9, .15, .5), AbsoluteLayoutFlags.All);
                //absTreadDepthUnderside.Children.Add(lblLastChannel4, new Rectangle(.79, .9, .15, .5), AbsoluteLayoutFlags.All);

                //absTreadDepthUnderside.Children.Add(lblChannel1, new Rectangle(.21, -.8, .15, .8), AbsoluteLayoutFlags.All);
                //absTreadDepthUnderside.Children.Add(lblChannel2, new Rectangle(.4, -.8, .15, .8), AbsoluteLayoutFlags.All);
                //absTreadDepthUnderside.Children.Add(lblChannel3, new Rectangle(.595, -.8, .15, .8), AbsoluteLayoutFlags.All);
                //absTreadDepthUnderside.Children.Add(lblChannel4, new Rectangle(.79, -.8, .15, .8), AbsoluteLayoutFlags.All);

                Content = stcTreadDepths;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        async Task Channel1Prepare(decimal? channel, decimal? oldChannel)
        {
            try
            {
                var _channel = channel is null ? oldChannel : channel;

                //if (_channel != null)
                //{
                //    AbsoluteLayout.SetLayoutBounds(bxChannel1, new Rectangle(.125, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                //    AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.3125, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                //}
                //else
                //{
                //    AbsoluteLayout.SetLayoutBounds(bxChannel1, new Rectangle(.125, .6, .11, 0.1));
                //    AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.3125, .6, .11, 0.1));
                //}

                if (_channel != null)
                {
                    switch (TireChannelLayoutData.ChannelCount)
                    {
                        case 1:
                            AbsoluteLayout.SetLayoutBounds(bxChannel1, new Rectangle(.39, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.611, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            break;
                        case 2:
                            AbsoluteLayout.SetLayoutBounds(bxChannel1, new Rectangle(.285, .6, .12, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.5, .6, .12, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            break;
                        case 3:
                            AbsoluteLayout.SetLayoutBounds(bxChannel1, new Rectangle(.181, .6, .125, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.393, .6, .125, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            break;
                        case 4:
                            AbsoluteLayout.SetLayoutBounds(bxChannel1, new Rectangle(.125, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.3125, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            break;
                    }
                }
                else
                {
                    switch (TireChannelLayoutData.ChannelCount)
                    {
                        case 1:
                            AbsoluteLayout.SetLayoutBounds(bxChannel1, new Rectangle(.39, .6, .11, +0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.611, .6, .11, +0.1));
                            break;
                        case 2:
                            AbsoluteLayout.SetLayoutBounds(bxChannel1, new Rectangle(.285, .6, .12, +0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.5, .6, .12, +0.1));
                            break;
                        case 3:
                            AbsoluteLayout.SetLayoutBounds(bxChannel1, new Rectangle(.181, .6, .125, +0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.393, .6, .125, +0.1));
                            break;
                        case 4:
                            AbsoluteLayout.SetLayoutBounds(bxChannel1, new Rectangle(.125, .6, .11, +0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.3125, .6, .11, +0.1));
                            break;
                    }
                }

                bxChannelNo1.BackgroundColor = channel is null ? Color.White : Color.FromHex("#FF681C");
                lblChannelNo1.TextColor = channel is null ? Color.Black : Color.FromHex("#FFFFFF");
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        async Task Channel2Prepare(decimal? channel, decimal? oldChannel)
        {
            try
            {
                var _channel = channel is null ? oldChannel : channel;

                //if (_channel != null)
                //{
                //    AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.3125, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                //    AbsoluteLayout.SetLayoutBounds(bxChannel3, new Rectangle(.5, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                //}
                //else
                //{
                //    AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.3125, .6, .11, 0.1));
                //    AbsoluteLayout.SetLayoutBounds(bxChannel3, new Rectangle(.5, .6, .11, 0.1));
                //}

                if (_channel != null)
                {
                    switch (TireChannelLayoutData.ChannelCount)
                    {
                        case 2:
                            AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.5, .6, .12, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel3, new Rectangle(.712, .6, .12, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            break;
                        case 3:
                            AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.393, .6, .125, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel3, new Rectangle(.608, .6, .125, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            break;
                        case 4:
                            AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.3125, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel3, new Rectangle(.5, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            break;
                    }
                }
                else
                {
                    switch (TireChannelLayoutData.ChannelCount)
                    {
                        case 2:
                            AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.5, .6, .12, 0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel3, new Rectangle(.712, .6, .12, 0.1));
                            break;
                        case 3:
                            AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.393, .6, .125, 0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel3, new Rectangle(.608, .6, .125, 0.1));
                            break;
                        case 4:
                            AbsoluteLayout.SetLayoutBounds(bxChannel2, new Rectangle(.3125, .6, .11, 0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel3, new Rectangle(.5, .6, .11, 0.1));
                            break;
                    }
                }

                bxChannelNo2.BackgroundColor = channel is null ? Color.White : Color.FromHex("#FF681C");
                lblChannelNo2.TextColor = channel is null ? Color.Black : Color.FromHex("#FFFFFF");
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        async Task Channel3Prepare(decimal? channel, decimal? oldChannel)
        {
            try
            {
                var _channel = channel is null ? oldChannel : channel;

                //if (_channel != null)
                //{
                //    AbsoluteLayout.SetLayoutBounds(bxChannel3, new Rectangle(.5, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                //    AbsoluteLayout.SetLayoutBounds(bxChannel4, new Rectangle(.6875, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                //}
                //else
                //{
                //    AbsoluteLayout.SetLayoutBounds(bxChannel3, new Rectangle(.5, .6, .11, 0.1));
                //    AbsoluteLayout.SetLayoutBounds(bxChannel4, new Rectangle(.6875, .6, .11, 0.1));
                //}

                if (_channel != null)
                {
                    switch (TireChannelLayoutData.ChannelCount)
                    {
                        case 3:
                            AbsoluteLayout.SetLayoutBounds(bxChannel3, new Rectangle(.608, .6, .125, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel4, new Rectangle(.817, .6, .125, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            break;
                        case 4:
                            AbsoluteLayout.SetLayoutBounds(bxChannel3, new Rectangle(.5, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel4, new Rectangle(.6875, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));
                            break;
                    }
                }
                else
                {
                    switch (TireChannelLayoutData.ChannelCount)
                    {
                        case 3:
                            AbsoluteLayout.SetLayoutBounds(bxChannel3, new Rectangle(.608, .6, .125, 0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel4, new Rectangle(.817, .6, .125, 0.1));
                            break;
                        case 4:
                            AbsoluteLayout.SetLayoutBounds(bxChannel3, new Rectangle(.5, .6, .11, 0.1));
                            AbsoluteLayout.SetLayoutBounds(bxChannel4, new Rectangle(.6875, .6, .11, 0.1));
                            break;
                    }
                }

                bxChannelNo3.BackgroundColor = channel is null ? Color.White : Color.FromHex("#FF681C");
                lblChannelNo3.TextColor = channel is null ? Color.Black : Color.FromHex("#FFFFFF");
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        async Task Channel4Prepare(decimal? channel, decimal? oldChannel)
        {
            try
            {
                var _channel = channel is null ? oldChannel : channel;

                if (_channel != null)
                {
                    AbsoluteLayout.SetLayoutBounds(bxChannel4, new Rectangle(.6875, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));  // ABSOLUTELAYOUT da oran 0.1-0.4 aralığında olmalı-bu sebeple girilen diş derinliğini orjinal diş derinliğine 0.3 üzerinden oranlayıp 0.1 ekliyorum
                    AbsoluteLayout.SetLayoutBounds(bxChannel5, new Rectangle(.875, .6, .11, (Convert.ToDouble(_channel.Value) * 0.3 / Convert.ToDouble(TireChannelLayoutData.OriginalTreadDepth)) + 0.1));  // ABSOLUTELAYOUT da oran 0.1-0.4 aralığında olmalı-bu sebeple girilen diş derinliğini orjinal diş derinliğine 0.3 üzerinden oranlayıp 0.1 ekliyorum
                }
                else
                {
                    AbsoluteLayout.SetLayoutBounds(bxChannel4, new Rectangle(.6875, .6, .11, 0.1));  // ABSOLUTELAYOUT da oran 0.1-0.4 aralığında olmalı-bu sebeple girilen diş derinliğini orjinal diş derinliğine 0.3 üzerinden oranlayıp 0.1 ekliyorum
                    AbsoluteLayout.SetLayoutBounds(bxChannel5, new Rectangle(.875, .6, .11, 0.1));  // ABSOLUTELAYOUT da oran 0.1-0.4 aralığında olmalı-bu sebeple girilen diş derinliğini orjinal diş derinliğine 0.3 üzerinden oranlayıp 0.1 ekliyorum
                }

                bxChannelNo4.BackgroundColor = channel is null ? Color.White : Color.FromHex("#FF681C");
                lblChannelNo4.TextColor = channel is null ? Color.Black : Color.FromHex("#FFFFFF");
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        async Task ArrowPrepare()
        {
            try
            {
                ImgArrowDown1.IsVisible = false;
                ImgArrowDown2.IsVisible = false;
                ImgArrowDown3.IsVisible = false;
                ImgArrowDown4.IsVisible = false;

                if (TireChannelLayoutData.focusedChannelIndex == 1)
                    ImgArrowDown1.IsVisible = true;
                else if (TireChannelLayoutData.focusedChannelIndex == 2)
                    ImgArrowDown2.IsVisible = true;
                else if (TireChannelLayoutData.focusedChannelIndex == 3)
                    ImgArrowDown3.IsVisible = true;
                else if (TireChannelLayoutData.focusedChannelIndex == 4)
                    ImgArrowDown4.IsVisible = true;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        async void ChannelNo1Click(View arg1, object arg2)
        {
            try
            {
                TireChannelLayoutData.focusedChannelIndex = 1;

                ArrowPrepare();

                TireChannelLayoutData.ArrowPositionChangedCommand.Execute(null);
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
                return;
            }
        }

        async void ChannelNo2Click(View arg1, object arg2)
        {
            try
            {
                TireChannelLayoutData.focusedChannelIndex = 2;

                ArrowPrepare();

                TireChannelLayoutData.ArrowPositionChangedCommand.Execute(null);
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
                return;
            }
        }

        async void ChannelNo3Click(View arg1, object arg2)
        {
            try
            {
                TireChannelLayoutData.focusedChannelIndex = 3;

                ArrowPrepare();

                TireChannelLayoutData.ArrowPositionChangedCommand.Execute(null);
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
                return;
            }
        }

        async void ChannelNo4Click(View arg1, object arg2)
        {
            try
            {
                TireChannelLayoutData.focusedChannelIndex = 4;

                ArrowPrepare();

                TireChannelLayoutData.ArrowPositionChangedCommand.Execute(null);
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
                return;
            }
        }

        public ICommand ConnectionStausChangedCommand => new Command(async () =>
        {
            IsBluetoothConnected = GlobalSetting.Instance.isBluetoothConnected;
        });

        public Command BluetoothDataCommand => new Command<string>(async (s) =>
        {
            try
            {
                if (!IsBluetoothConnected) return;

                if (!GlobalSetting.Instance.BluetoothGetDataRunning && string.IsNullOrWhiteSpace(s))
                {
                    GlobalSetting.Instance.isBluetoothConnected = false;

                    MessagingCenter.Send<object, enCustomBluetoothToolbar>("CustomTireChannelLayout", "BluetoothConnectionStausChanged", enCustomBluetoothToolbar.NotConnectedToDevice);
                    return;
                }

                bool Setdata = false;

                if (!string.IsNullOrWhiteSpace(s))
                {
                    if (s.Contains("T"))
                    {
                        string Channel = TOOLS.convertTreadDepthUnitForView(TOOLS.ToDecimalNull(TOOLS.convertBluetoothChannel(s))).ToString();

                        if (TireChannelLayoutData.focusedChannelIndex == 1)
                        {
                            lblChannel1.Text = Channel;

                            if (TireChannelLayoutData.treadDepthInputDirection == enTreadDepthInputDirection.DistanIce)
                            {
                                TireChannelLayoutData.tireAction.channel1 = TOOLS.ToDecimal(Channel);
                                Channel1Prepare(TireChannelLayoutData.tireAction.channel1, TireChannelLayoutData.lastTireAction.channel1);
                            }
                            else
                            {
                                TireChannelLayoutData.tireAction.channel4 = TOOLS.ToDecimal(Channel);
                                Channel1Prepare(TireChannelLayoutData.tireAction.channel4, TireChannelLayoutData.lastTireAction.channel4);
                            }

                            #region nextChannel
                            TireChannelLayoutData.focusedChannelIndex = 2;
                            ArrowPrepare();
                            TireChannelLayoutData.ArrowPositionChangedCommand.Execute(null);
                            #endregion

                            Setdata = true;

                            Channel1DataInputType = enDataInputType.BluetoothProbe;
                        }
                        else if (TireChannelLayoutData.focusedChannelIndex == 2)
                        {
                            lblChannel2.Text = Channel;

                            if (TireChannelLayoutData.treadDepthInputDirection == enTreadDepthInputDirection.DistanIce)
                            {
                                TireChannelLayoutData.tireAction.channel2 = TOOLS.ToDecimal(Channel);
                                Channel2Prepare(TireChannelLayoutData.tireAction.channel2, TireChannelLayoutData.lastTireAction.channel2);
                            }
                            else
                            {
                                TireChannelLayoutData.tireAction.channel3 = TOOLS.ToDecimal(Channel);
                                Channel2Prepare(TireChannelLayoutData.tireAction.channel3, TireChannelLayoutData.lastTireAction.channel3);
                            }

                            #region nextChannel
                            TireChannelLayoutData.focusedChannelIndex = 3;
                            ArrowPrepare();
                            TireChannelLayoutData.ArrowPositionChangedCommand.Execute(null);
                            #endregion

                            Setdata = true;

                            Channel2DataInputType = enDataInputType.BluetoothProbe;
                        }
                        else if (TireChannelLayoutData.focusedChannelIndex == 3)
                        {
                            lblChannel3.Text = Channel;

                            if (TireChannelLayoutData.treadDepthInputDirection == enTreadDepthInputDirection.DistanIce)
                            {
                                TireChannelLayoutData.tireAction.channel3 = TOOLS.ToDecimal(Channel);
                                Channel3Prepare(TireChannelLayoutData.tireAction.channel3, TireChannelLayoutData.lastTireAction.channel3);
                            }
                            else
                            {
                                TireChannelLayoutData.tireAction.channel2 = TOOLS.ToDecimal(Channel);
                                Channel3Prepare(TireChannelLayoutData.tireAction.channel2, TireChannelLayoutData.lastTireAction.channel2);
                            }

                            #region nextChannel
                            TireChannelLayoutData.focusedChannelIndex = 4;
                            ArrowPrepare();
                            TireChannelLayoutData.ArrowPositionChangedCommand.Execute(null);
                            #endregion

                            Setdata = true;

                            Channel3DataInputType = enDataInputType.BluetoothProbe;
                        }
                        else if (TireChannelLayoutData.focusedChannelIndex == 4)
                        {
                            lblChannel4.Text = Channel;

                            if (TireChannelLayoutData.treadDepthInputDirection == enTreadDepthInputDirection.DistanIce)
                            {
                                TireChannelLayoutData.tireAction.channel4 = TOOLS.ToDecimal(Channel);
                                Channel4Prepare(TireChannelLayoutData.tireAction.channel4, TireChannelLayoutData.lastTireAction.channel4);
                            }
                            else
                            {
                                TireChannelLayoutData.tireAction.channel1 = TOOLS.ToDecimal(Channel);
                                Channel4Prepare(TireChannelLayoutData.tireAction.channel1, TireChannelLayoutData.lastTireAction.channel1);
                            }

                            Setdata = true;

                            Channel4DataInputType = enDataInputType.BluetoothProbe;
                        }

                        if (Setdata)
                        {
                            if (Setdata)
                                DependencyService.Get<IAudio>().PlayWavFile();

                            TireChannelLayoutData.tireAction.ChannelAVG = TOOLS.getChannelAvg(
                                TireChannelLayoutData.tireAction.channel1,
                                TireChannelLayoutData.tireAction.channel2,
                                TireChannelLayoutData.tireAction.channel3,
                                TireChannelLayoutData.tireAction.channel4);

                            TireChannelLayoutData.tireAction = _tireActionSQLiteService.PrepareClassifications(TireChannelLayoutData.tireAction, tireProduct: TireChannelLayoutData.tireProduct);

                            TireChannelLayoutData.ValueUpdatedForBluetooth.Execute((int)enBluetoothInputType.TreadDepth);

                            NavigationModel<ShowBluetoothValuePageViewParamModel> navigationModel = new NavigationModel<ShowBluetoothValuePageViewParamModel>
                            {
                                Model = new ShowBluetoothValuePageViewParamModel
                                {
                                    TreadDepthValue = $"{Channel} MM"
                                },
                            };

                            await NavigationService.NavigateToPopupAsync<ShowBluetoothValuePageViewModel>(navigationModel);
                        }
                    }
                    else if (s.Contains("P"))
                    {
                        string AirPressure = TOOLS.convertAirPressureForView(TOOLS.ToDecimalNull(TOOLS.convertBluetoothAirPressure(s))).ToString();

                        TireChannelLayoutData.tireAction.airPressureMeasured = TOOLS.ToDecimal(AirPressure);
                        TireChannelLayoutData.tireAction = _tireActionSQLiteService.PrepareClassifications(TireChannelLayoutData.tireAction, tireProduct: TireChannelLayoutData.tireProduct);

                        Setdata = true;

                        TireChannelLayoutData.tireAction.AirPressureEntryType = (int)enDataInputType.BluetoothProbe;

                        DependencyService.Get<IAudio>().PlayWavFile();

                        TireChannelLayoutData.ValueUpdatedForBluetooth.Execute((int)enBluetoothInputType.AirPressure);

                        NavigationModel<ShowBluetoothValuePageViewParamModel> navigationModel = new NavigationModel<ShowBluetoothValuePageViewParamModel>
                        {
                            Model = new ShowBluetoothValuePageViewParamModel
                            {
                                TreadDepthValue = $"{AirPressure} PSI"
                            },
                        };

                        await NavigationService.NavigateToPopupAsync<ShowBluetoothValuePageViewModel>(navigationModel);
                    }
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
                if (!string.IsNullOrWhiteSpace(lblChannel1.Text) && Channel1DataInputType == enDataInputType.ManuelMobile)
                {
                    TireChannelLayoutData.tireAction.ChannelEntryType = (int)enDataInputType.ManuelMobile;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(lblChannel2.Text) && Channel2DataInputType == enDataInputType.ManuelMobile)
                {
                    TireChannelLayoutData.tireAction.ChannelEntryType = (int)enDataInputType.ManuelMobile;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(lblChannel3.Text) && Channel3DataInputType == enDataInputType.ManuelMobile)
                {
                    TireChannelLayoutData.tireAction.ChannelEntryType = (int)enDataInputType.ManuelMobile;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(lblChannel4.Text) && Channel4DataInputType == enDataInputType.ManuelMobile)
                {
                    TireChannelLayoutData.tireAction.ChannelEntryType = (int)enDataInputType.ManuelMobile;
                    return;
                }

                TireChannelLayoutData.tireAction.ChannelEntryType = (int)enDataInputType.BluetoothProbe;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }
        #endregion
    }
}