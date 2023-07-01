using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WiseDynamicMobile.Helper;
using WiseMobile.Helper;
using WiseMobile.Models.CommonModels.CustomModel;
using WiseMobile.Models.CommonModels.Enums;
using WiseMobile.Models.CommonModels.SQLiteModels;
using WiseMobile.Themes.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static WiseDynamicMobile.Helper.TOOLS;

namespace WiseMobile.CustomControls.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomVehicleLayout : Grid
    {
        int axle = 1;
        string position = "L1";
        Style Partials_TextFields_TextLabel;
        public CustomVehicleLayout()
        {
            InitializeComponent();

            Application.Current.Resources.TryGetValue("Partials.TextFields.TextLabel", out var retval);

            Partials_TextFields_TextLabel = (Style)retval;
        }

        /// <summary>
        /// The Vehicle Layout Data Property.
        /// </summary>
        public static readonly BindableProperty VehicleLayoutDataProperty = BindableProperty.Create(
            nameof(VehicleLayoutData),
            typeof(CustomVehicleLayoutData),
            typeof(CustomVehicleLayout),
            default(CustomVehicleLayoutData),
            Xamarin.Forms.BindingMode.OneWay);

        public CustomVehicleLayoutData VehicleLayoutData
        {
            get
            {
                return (CustomVehicleLayoutData)GetValue(VehicleLayoutDataProperty);
            }
            set
            {
                SetValue(VehicleLayoutDataProperty, value);
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            try
            {
                if (propertyName == VehicleLayoutDataProperty.PropertyName)
                {
                    var vehicleLayoutData = VehicleLayoutData;
                    vehicleLayoutData.CreateVehicleLayoutCommand = FillVehicleLayoutCommand;
                    vehicleLayoutData.PositionChangeResponseCommand = PositionChangeResponseCommand;
                    vehicleLayoutData.ChangeTireTireWarningInfoCommand = ChangeTireTireWarningInfoCommand;
                    vehicleLayoutData.ChangeTireSelectedInfoCommand = ChangeTireSelectedInfoCommand;
                    vehicleLayoutData.ChangeTireAirPressureInfoCommand = ChangeTireAirPressureInfoCommand;
                    vehicleLayoutData.ChangeTireTreadDepthInfoCommand = ChangeTireTreadDepthInfoCommand;
                    vehicleLayoutData.ChangeTireTireImageInfoCommand = ChangeTireTireImageInfoCommand;
                    vehicleLayoutData.ChangeTireWarningInfoCommand = ChangeTireWarningInfoCommand;
                }
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        public ICommand FillVehicleLayoutCommand => new Command(async () =>
        {
            FillVehicleLayout();
        });

        public ICommand PositionChangeResponseCommand => new Command(async (O) =>
        {
            //1. item position, 2. item result true/false
            if (O != null && O is List<object> retval && retval.Count == 2)
            {
                if (retval[1] is bool result && result) // true donduyse bi sonraki pozisyona geciyorum
                    await SelectedTire(TOOLS.ToString(retval[0]));
            };
        });

        public ICommand ChangeTireTireWarningInfoCommand => new Command(async (O) =>
        {
            //int axle, string position, bool isTireWarning
            if (O != null && O is Dictionary<string, object> retval)
            {
                ChangeTireInfo(TOOLS.ToInt(retval["axle"]), TOOLS.ToString(retval["position"]), isSelected: null, isTireWarning: Convert.ToBoolean(retval["isTireWarning"]));
            }
        });

        public ICommand ChangeTireSelectedInfoCommand => new Command(async (O) =>
        {
            //int axle, string position, bool isSelected
            if (O != null && O is Dictionary<string, object> retval)
            {
                if (Convert.ToBoolean(retval["isSelected"]))
                {
                    axle = TOOLS.ToInt(retval["axle"]);
                    position = TOOLS.ToString(retval["position"]);
                }

                ChangeTireInfo(TOOLS.ToInt(retval["axle"]), TOOLS.ToString(retval["position"]), isSelected: Convert.ToBoolean(retval["isSelected"]), isTireWarning: null);
            }
        });

        public ICommand ChangeTireWarningInfoCommand => new Command(async (O) =>
        {
            // int axle, string position, bool isTireWarning
            if (O != null && O is Dictionary<string, object> retval)
            {
                ChangeTireInfo(TOOLS.ToInt(retval["axle"]), TOOLS.ToString(retval["position"]), isSelected: null, isTireWarning: (bool)retval["isTireWarning"]);
            }
        });

        public ICommand ChangeTireAirPressureInfoCommand => new Command(async (O) =>
        {
            // int axle, string position, decimal? airPressure, enAirPressureImportances airPressureImportances
            if (O != null && O is Dictionary<string, object> retval)
            {
                ChangeTireInfo(TOOLS.ToInt(retval["axle"]), TOOLS.ToString(retval["position"]), isSelected: null, isTireWarning: null, setAirPressure: true,
                    airPressure: TOOLS.ToDecimal(retval["airPressure"]), airPressureImportances: (enAirPressureImportances?)TOOLS.ToIntNull(retval["airPressureImportances"]));
            }
        });

        public ICommand ChangeTireTreadDepthInfoCommand => new Command(async (O) =>
        {
            // int axle, string position, decimal? treadDepth, enTreadDepthImportances treadDepthImportances
            if (O != null && O is Dictionary<string, object> retval)
            {
                ChangeTireInfo(TOOLS.ToInt(retval["axle"]), TOOLS.ToString(retval["position"]), isSelected: null, isTireWarning: null, setTreadDepth: true,
                    treadDepth: TOOLS.ToDecimal(retval["treadDepth"]), treadDepthImportances: (enTreadDepthImportances?)TOOLS.ToIntNull(retval["treadDepthImportances"]));
            }
        });

        public ICommand ChangeTireTireImageInfoCommand => new Command(async (O) =>
        {
            // int axle, string position, decimal? treadDepth, enTreadDepthImportances treadDepthImportances
            if (O != null && O is Dictionary<string, object> retval)
            {
                ChangeTireInfo(TOOLS.ToInt(retval["axle"]), TOOLS.ToString(retval["position"]), isSelected: null, isTireWarning: null, setTireImage: true);
            }
        });


        async Task FillVehicleLayout()
        {
            try
            {
                bool NotInSpare = true;

                if (VehicleLayoutData.vehicle.SpareTireCount)
                    NotInSpare = false;

                if (VehicleLayoutData.vehicleTires == null) VehicleLayoutData.vehicleTires = new List<VehicleTire>();

                VehicleLayoutData.vehicleTires.RemoveAll(t => string.IsNullOrWhiteSpace(t.TireProductGuid));

                CreateVehicleLayoutGrid();
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        async Task CreateVehicleLayoutGrid()
        {
            try
            {
                AbsoluteLayout Tirelayout;

                axle = 1;
                position = "L1";

                grdVehicleLayout.ColumnDefinitions.Clear();
                grdVehicleLayout.RowDefinitions.Clear();
                grdVehicleLayout.Children.Clear();

                grdVehicleLayout.HorizontalOptions = LayoutOptions.StartAndExpand;
                grdVehicleLayout.VerticalOptions = LayoutOptions.CenterAndExpand;
                grdVehicleLayout.ColumnSpacing = 0;
                grdVehicleLayout.RowSpacing = VehicleLayoutData.IsRotationVehicleLayout ? 10 : 20; // axle ust ve altındaki size ve hava basıncı bilgisini yazmadıgımız icin rotasyondaki satır aralıgının bu kadar buyuk olmasına gerek olmadıgı ıcın duzenledim

                grdVehicleLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) });
                grdVehicleLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) });
                grdVehicleLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
                grdVehicleLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) });
                grdVehicleLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) });

                for (int ix = 0; ix < VehicleLayoutData.vehicleTypeDetails.Count; ix++)
                {
                    if (!VehicleLayoutData.vehicle.SpareTireCount && VehicleLayoutData.vehicleTypeDetails[ix].IsSpare) continue;

                    grdVehicleLayout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                    VehicleVehicleTypeDetail xVehicleVehicleTypeDetail = VehicleLayoutData.vehicleVehicleTypeDetails.Where(x => x.VehicleTypeDetailGuid == VehicleLayoutData.vehicleTypeDetails[ix].Guid).FirstOrDefault();

                    #region CreateAxle
                    AbsoluteLayout midlineLayout = new AbsoluteLayout();

                    midlineLayout.Children.Add(new Image
                    {
                        Aspect = Aspect.AspectFit,
                        Source = (VehicleLayoutData.vehicleTypeDetails[ix].WheelPositionType == (int)enWheelPositionType.Single) ? "AxleSingle" : "AxleDual"
                    }, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);


                    StackLayout stcAxleNfo = new StackLayout();
                    stcAxleNfo.Children.Add(new Image()
                    {
                        Aspect = Aspect.AspectFit,
                        Source = "AxleNfo",
                        WidthRequest = 46,
                        HeightRequest = 46,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    });

                    midlineLayout.Children.Add(stcAxleNfo, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

                    if (!VehicleLayoutData.IsRotationVehicleLayout && VehicleLayoutData.fleetBranchVehicleType != null) // Rotasyon için olusturulan layout ise aks uzerindeki size ve tavsiye edilen hava basıncı bilgisini yazmıyorum
                    {
                        string maxAirPressure = "";
                        string tireSize = "";

                        if (VehicleLayoutData.vehicleTypeDetails[ix].IsSteer)
                        {
                            tireSize = TOOLS.ToString(VehicleLayoutData.fleetBranchVehicleType.SteerRecommendedTireSize);
                            maxAirPressure = TOOLS.ToString(VehicleLayoutData.fleetBranchVehicleType.SteerDefaultPressure);
                        }
                        else if (VehicleLayoutData.vehicleTypeDetails[ix].IsDrive)
                        {
                            tireSize = TOOLS.ToString(VehicleLayoutData.fleetBranchVehicleType.DriveRecommendedTireSize);
                            maxAirPressure = TOOLS.ToString(VehicleLayoutData.fleetBranchVehicleType.DriveDefaultPressure);
                        }
                        else if (VehicleLayoutData.vehicleTypeDetails[ix].IsFreeRolling)
                        {
                            tireSize = TOOLS.ToString(VehicleLayoutData.fleetBranchVehicleType.FreeRollingRecommendedTireSize);
                            maxAirPressure = TOOLS.ToString(VehicleLayoutData.fleetBranchVehicleType.FreeRollingDefaultPressure);
                        }
                        else if (VehicleLayoutData.vehicleTypeDetails[ix].IsSpare)
                        {
                            tireSize = TOOLS.ToString(VehicleLayoutData.fleetBranchVehicleType.SpareRecommendedTireSize);
                            maxAirPressure = TOOLS.ToString(VehicleLayoutData.fleetBranchVehicleType.SpareTireDefaultPressure);
                        }

                        StackLayout stcAxleAirPressureInfo = new StackLayout();
                        stcAxleAirPressureInfo.Children.Add(new Label()
                        {
                            Text = maxAirPressure,
                            VerticalOptions = LayoutOptions.StartAndExpand,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,

                            Style = Partials_TextFields_TextLabel,
                        });

                        midlineLayout.Children.Add(stcAxleAirPressureInfo, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);


                        StackLayout stcAxleTireSizeInfo = new StackLayout();
                        stcAxleTireSizeInfo.Children.Add(new Label()
                        {
                            Text = tireSize,
                            VerticalOptions = LayoutOptions.EndAndExpand,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,

                            Style = Partials_TextFields_TextLabel,
                        });

                        midlineLayout.Children.Add(stcAxleTireSizeInfo, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
                    }

                    StackLayout stcAxleNumber = new StackLayout();
                    stcAxleNumber.Children.Add(new Label()
                    {
                        Text = (ix + 1).ToString(),
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        TextColor = Color.FromHex("#425466"),
                        FontSize = 20,
                        Style = Partials_TextFields_TextLabel,
                    });

                    midlineLayout.Children.Add(stcAxleNumber, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
                    #endregion

                    int ColumnIndex = 0;

                    string L1 = "L1";
                    string R1 = "R1";

                    if (VehicleLayoutData.vehicleTypeDetails[ix].IsSpare)
                    {
                        L1 = "S1";
                        R1 = "S2";
                    }

                    Tirelayout = VehicleTireInfLayout((ix + 1), L1, VehicleLayoutData.vehicleTypeDetails[ix].IsSpare, xVehicleVehicleTypeDetail.Guid,
                        (ix + 1).ToString() + VehicleLayoutData.Positions.FirstOrDefault(t => t.Name == L1 && t.IsDual == (VehicleLayoutData.vehicleTypeDetails[ix].WheelPositionType == (int)enWheelPositionType.Dual ? true : false)).NameVal);

                    grdVehicleLayout.Children.Add(Tirelayout, ColumnIndex, ix);

                    if (VehicleLayoutData.vehicleTypeDetails[ix].IsSpare)
                    {
                        Grid.SetColumnSpan(Tirelayout, 2);
                        ColumnIndex = ColumnIndex + 2;
                    }
                    else
                    {
                        ColumnIndex = ColumnIndex + 1;
                    }

                    if (VehicleLayoutData.vehicleTypeDetails[ix].WheelPositionType == (int)enWheelPositionType.Dual)
                    {
                        Tirelayout = VehicleTireInfLayout((ix + 1), "L2", false, xVehicleVehicleTypeDetail.Guid,
                            (ix + 1).ToString() + VehicleLayoutData.Positions.FirstOrDefault(t => t.Name == "L2" && t.IsDual == (VehicleLayoutData.vehicleTypeDetails[ix].WheelPositionType == (int)enWheelPositionType.Dual ? true : false)).NameVal);
                        grdVehicleLayout.Children.Add(Tirelayout, ColumnIndex, ix);
                        ColumnIndex = ColumnIndex + 1;

                        grdVehicleLayout.Children.Add(midlineLayout, ColumnIndex, ix);
                        //midlineImage.Margin = middleLineThck;
                        ColumnIndex = ColumnIndex + 1;

                        Tirelayout = VehicleTireInfLayout((ix + 1), "R2", false, xVehicleVehicleTypeDetail.Guid,
                            (ix + 1).ToString() + VehicleLayoutData.Positions.FirstOrDefault(t => t.Name == "R2" && t.IsDual == (VehicleLayoutData.vehicleTypeDetails[ix].WheelPositionType == (int)enWheelPositionType.Dual ? true : false)).NameVal);
                        grdVehicleLayout.Children.Add(Tirelayout, ColumnIndex, ix);
                        ColumnIndex = ColumnIndex + 1;
                    }
                    else
                    {
                        if (VehicleLayoutData.vehicleTypeDetails[ix].IsSpare)
                        {
                            //midlineImage.Margin = tireSteeringThck;
                            ColumnIndex = ColumnIndex + 1;
                        }
                        else
                        {
                            grdVehicleLayout.Children.Add(midlineLayout, ColumnIndex, ix);
                            //midlineImage.Margin = middleLineThck;
                            Grid.SetColumnSpan(midlineLayout, 3);
                            ColumnIndex = ColumnIndex + 3;
                        }
                    }

                    Tirelayout = VehicleTireInfLayout((ix + 1), R1, false, xVehicleVehicleTypeDetail.Guid,
                        (ix + 1).ToString() + VehicleLayoutData.Positions.FirstOrDefault(t => t.Name == R1 && t.IsDual == (VehicleLayoutData.vehicleTypeDetails[ix].WheelPositionType == (int)enWheelPositionType.Dual ? true : false)).NameVal);
                    grdVehicleLayout.Children.Add(Tirelayout, ColumnIndex, ix);

                    //if (vehicleTypeDetails[ix].IsSteer == true || vehicleTypeDetails[ix].IsSpare)
                    //    Grid.SetColumnSpan(Tirelayout, 2);

                    if (VehicleLayoutData.vehicleTypeDetails[ix].IsSpare)
                        Grid.SetColumnSpan(Tirelayout, 2);
                }
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }

            AbsoluteLayout VehicleTireInfLayout(int sAxle, string sPosition, bool IsSpare, string vehicleVehicleTypeDetailGuID, string positionAxleLng)
            {
                try
                {
                    bool isTireWarning = false;

                    if (VehicleLayoutData.warningPositions != null && VehicleLayoutData.warningPositions.Any(t => t.Equals($"{sAxle}|{sPosition}")))
                        isTireWarning = true;

                    // islem yapılan position'daki product'u alıyorum
                    VehicleTire vehicleTire = VehicleLayoutData.vehicleTires.FirstOrDefault(t => t.AxleNumber == sAxle && t.Position == sPosition && !string.IsNullOrWhiteSpace(t.TireProductGuid) && t.IsDeleted == false);

                    string tireImageSource = "";

                    if (IsSpare == true)
                        tireImageSource = "STire";
                    else
                        tireImageSource = sPosition.Substring(0, 1) + "Tire";

                    // layout jantta dondur modunda acıldıysa ve rotatedOnRimPositions buraya eklendiyse lastik resmini degistiriyoruz
                    if (VehicleLayoutData.IsRotateOnRim && VehicleLayoutData.rotatedOnRimPositions.Exists(t => t.Equals(sAxle + "|" + sPosition)))
                        tireImageSource += "RimRotate";


                    string treadDept = "";
                    string airPressure = "";

                    Xamarin.Forms.Color treadDeptImportancesColor = Xamarin.Forms.Color.Transparent;
                    Xamarin.Forms.Color airPressureImportancesColor = Xamarin.Forms.Color.Transparent;

                    if (vehicleTire == null)
                        tireImageSource += VehicleLayoutData.IsRotationVehicleLayout ? "AddEmpty" : "Empty";
                    else
                    {
                        var tireActionTreadDepth = VehicleLayoutData.tireActionTreadDepths.FirstOrDefault(t => t.TireProductGuid == vehicleTire.TireProductGuid);

                        if (tireActionTreadDepth != null)
                        {
                            treadDept = TOOLS.getChannelAvg(tireActionTreadDepth).Value.ToString();
                            treadDeptImportancesColor = GetTreadDepthImportances((enTreadDepthImportances?)tireActionTreadDepth.TreadDepthImportances);
                        }

                        var tireActionAirPressure = VehicleLayoutData.tireActionAirPressures.FirstOrDefault(t => t.TireProductGuid == vehicleTire.TireProductGuid);

                        if (tireActionAirPressure != null)
                        {
                            airPressure = tireActionAirPressure.airPressureCompleted is null ? tireActionAirPressure.airPressureMeasured.ToString() : tireActionAirPressure.airPressureCompleted.ToString();
                            airPressureImportancesColor = GetAirPressureImportanceColor((enAirPressureImportances?)tireActionAirPressure.AirPressureImportances);
                        }

                        // TODO: Is Emri olayını en son halledicez
                        //WorkOrderProcessService workOrderProcessViewModel = new WorkOrderProcessService();

                        //var retval = workOrderProcessViewModel.IsThereWorkOrderProcess(vehicleTire.TireProductGuid);

                        //if (retval)
                        //    vehicleLayoutPosition = VehicleLayoutPosition.Warning;
                    }

                    AbsoluteLayout layout = new AbsoluteLayout();

                    double layoutWith = 1;

                    if (sPosition.Substring(0, 1).Equals("R"))
                    {
                        layoutWith = 0.89;
                    }

                    #region TireImage
                    StackLayout stcTire = new StackLayout();
                    stcTire.ClassId = "MainStc";
                    stcTire.Children.Add(new Image
                    {
                        HorizontalOptions = (sPosition.Substring(0, 1).Equals("R")) ? LayoutOptions.StartAndExpand : LayoutOptions.EndAndExpand,
                        Source = tireImageSource,
                        Aspect = Aspect.AspectFit,
                        ClassId = "TireImage"
                    });
                    layout.Children.Add(stcTire, new Rectangle(0, 0, layoutWith, 1), AbsoluteLayoutFlags.All);

                    StackLayout stcTireSelect = new StackLayout();
                    stcTireSelect.ClassId = "MainStc";
                    stcTireSelect.Children.Add(new Image
                    {
                        HorizontalOptions = (sPosition.Substring(0, 1).Equals("R")) ? LayoutOptions.StartAndExpand : LayoutOptions.EndAndExpand,
                        Source = "",
                        Aspect = Aspect.AspectFit,
                        ClassId = "TireImageSelect"
                    });
                    layout.Children.Add(stcTireSelect, new Rectangle(0, 0, layoutWith, 1), AbsoluteLayoutFlags.All);
                    #endregion

                    if (!VehicleLayoutData.IsRotationVehicleLayout)
                    {
                        #region Top
                        Thickness tchTop = new Thickness(9.5, 4, 0, 0);
                        switch (sPosition.Substring(0, 1).ToString())
                        {
                            case "L":
                                tchTop = new Thickness(9.5, 4, 0, 0);
                                break;
                            case "R":
                                tchTop = new Thickness(-2.5, 4, 0, 0);
                                break;
                            case "S":
                                tchTop = new Thickness(18, 35, 0, 0);
                                break;
                        }

                        StackLayout stcTopBackcolorInf = new StackLayout();
                        stcTopBackcolorInf.ClassId = "MainStc";
                        stcTopBackcolorInf.Children.Add(new BoxView
                        {
                            BackgroundColor = treadDeptImportancesColor,
                            WidthRequest = 40,
                            HeightRequest = 20,
                            CornerRadius = 6,
                            Margin = tchTop,
                            HorizontalOptions = (sPosition.Substring(0, 1).Equals("S")) ? LayoutOptions.Start : LayoutOptions.Center,
                            ClassId = "bxTopBackcolorInf",
                            IsVisible = vehicleTire is null ? false : true
                        });
                        layout.Children.Add(stcTopBackcolorInf, new Rectangle(0, 0, layoutWith, 1), AbsoluteLayoutFlags.All);

                        StackLayout stcTopLabelInf = new StackLayout();
                        stcTopLabelInf.ClassId = "MainStc";
                        stcTopLabelInf.Children.Add(new Label
                        {
                            WidthRequest = 40,
                            HeightRequest = 20,
                            Margin = tchTop,
                            HorizontalOptions = (sPosition.Substring(0, 1).Equals("S")) ? LayoutOptions.Start : LayoutOptions.Center,
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center,
                            TextColor = treadDeptImportancesColor == Color.FromHex("#F2F4F5") ? Color.Black : Color.FromHex("#FFFFFF"),
                            Text = string.IsNullOrWhiteSpace(treadDept) ? "-" : treadDept,
                            ClassId = "lblTopLabelInf",
                            Style = Partials_TextFields_TextLabel,
                            IsVisible = vehicleTire is null ? false : true
                        });
                        layout.Children.Add(stcTopLabelInf, new Rectangle(0, 0, layoutWith, 1), AbsoluteLayoutFlags.All);
                        #endregion

                        #region Center
                        if (!sPosition.Substring(0, 1).Equals("S"))
                        {
                            Thickness thcCenterBackcolorInf;

                            if (sPosition.Substring(0, 1).Equals("L"))
                                thcCenterBackcolorInf = new Thickness(11, 32, 0, 0);
                            else
                                thcCenterBackcolorInf = new Thickness(0, 32, 0, 0);

                            StackLayout stcCenterBackcolorInf = new StackLayout();
                            stcCenterBackcolorInf.ClassId = "MainStc";
                            stcCenterBackcolorInf.Children.Add(new BoxView
                            {
                                BackgroundColor = isTireWarning ? Color.FromHex("#F16063") : Color.FromHex("#FFFFFF"),
                                WidthRequest = 28,
                                HeightRequest = 28,
                                CornerRadius = (Device.RuntimePlatform == Device.Android) ? 28 : 14,
                                Margin = thcCenterBackcolorInf,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center,
                                ClassId = "bxCenterBackcolorInf"
                            });
                            layout.Children.Add(stcCenterBackcolorInf, new Rectangle(0, 0, layoutWith, 1), AbsoluteLayoutFlags.All);

                            StackLayout stcCenterLabelInf = new StackLayout();
                            stcCenterLabelInf.ClassId = "MainStc";
                            stcCenterLabelInf.Children.Add(new Label
                            {
                                WidthRequest = 28,
                                HeightRequest = 28,
                                Margin = thcCenterBackcolorInf,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalTextAlignment = TextAlignment.Center,
                                VerticalTextAlignment = TextAlignment.Center,
                                ClassId = "lblCenterLabelInf",
                                TextColor = isTireWarning ? Color.FromHex("#FFFFFF") : Color.FromHex("#27272E"),
                                Style = Partials_TextFields_TextLabel,

                                Text = sPosition,
                            });
                            layout.Children.Add(stcCenterLabelInf, new Rectangle(0, 0, layoutWith, 1), AbsoluteLayoutFlags.All);
                        }
                        #endregion

                        #region Bottom
                        Thickness tchBottom = new Thickness(9.5, 65, 0, 0);
                        switch (sPosition.Substring(0, 1).ToString())
                        {
                            case "L":
                                tchBottom = new Thickness(9.5, 65, 0, 0);
                                break;
                            case "R":
                                tchBottom = new Thickness(-2.5, 65, 0, 0);
                                break;
                            case "S":
                                tchBottom = new Thickness(58, 35, 0, 0);
                                break;
                        }

                        StackLayout stcBottomBackcolorInf = new StackLayout();
                        stcBottomBackcolorInf.ClassId = "MainStc";
                        stcBottomBackcolorInf.Children.Add(new BoxView
                        {
                            BackgroundColor = airPressureImportancesColor,
                            WidthRequest = 40,
                            HeightRequest = 20,
                            CornerRadius = 6,
                            Margin = tchBottom,
                            HorizontalOptions = LayoutOptions.Center,
                            ClassId = "bxBottomBackcolorInf",
                            IsVisible = vehicleTire is null ? false : true
                        });
                        layout.Children.Add(stcBottomBackcolorInf, new Rectangle(0, 0, layoutWith, 1), AbsoluteLayoutFlags.All);

                        StackLayout stcBottomLabelInf = new StackLayout();
                        stcBottomLabelInf.ClassId = "MainStc";
                        stcBottomLabelInf.Children.Add(new Label
                        {
                            WidthRequest = 40,
                            HeightRequest = 20,
                            Margin = tchBottom,
                            HorizontalOptions = LayoutOptions.Center,
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center,
                            TextColor = airPressureImportancesColor == Color.FromHex("#F2F4F5") ? Color.Black : Color.FromHex("#FFFFFF"),
                            Text = string.IsNullOrWhiteSpace(airPressure) ? "-" : airPressure,
                            ClassId = "lblBottomLabelInf",
                            Style = Partials_TextFields_TextLabel,
                            IsVisible = vehicleTire is null ? false : true
                        });
                        layout.Children.Add(stcBottomLabelInf, new Rectangle(0, 0, layoutWith, 1), AbsoluteLayoutFlags.All);
                        #endregion
                    }

                    BoxView boxView = new BoxView();
                    boxView.BackgroundColor = Color.Transparent;
                    boxView.ClassId = sAxle + "|" + sPosition + "|" + vehicleVehicleTypeDetailGuID + "|" + TOOLS.BoolToInt(isTireWarning) + "|" + positionAxleLng;

                    boxView.GestureRecognizers.Add(new TapGestureRecognizer(SelectedTire));


                    layout.ClassId = sAxle + "|" + sPosition + "|" + vehicleVehicleTypeDetailGuID + "|" + TOOLS.BoolToInt(isTireWarning) + "|" + positionAxleLng;
                    layout.Children.Add(boxView, new Rectangle(0, 0, layoutWith, 1), AbsoluteLayoutFlags.All);

                    return layout;
                }
                catch (Exception ex)
                {
                    WiseLogger.Instance.CrashLog(ex);

                    return null;
                }
            }
        }

        //public void ChangeTireTireWarningInfo(int axle, string position, bool isTireWarning)
        //{
        //    ChangeTireInfo(axle, position, isSelected: null, isTireWarning: isTireWarning);
        //}

        //public void ChangeTireSelectedInfo(int axle, string position, bool isSelected)
        //{
        //    ChangeTireInfo(axle, position, isSelected: isSelected, isTireWarning: null);
        //}

        //public void ChangeTireAirPressureInfo(int axle, string position, decimal? airPressure, enAirPressureImportances airPressureImportances)
        //{
        //    ChangeTireInfo(axle, position, isSelected: null, isTireWarning: null, setAirPressure: true, airPressure: airPressure, airPressureImportances: airPressureImportances);
        //}

        //public void ChangeTireTreadDepthInfo(int axle, string position, decimal? treadDepth, enTreadDepthImportances treadDepthImportances)
        //{
        //    ChangeTireInfo(axle, position, isSelected: null, isTireWarning: null, setTreadDepth: true, treadDepth: treadDepth, treadDepthImportances: treadDepthImportances);
        //}

        async Task ChangeTireInfo(int axle, string position, bool? isSelected, bool? isTireWarning,
            bool setAirPressure = false, decimal? airPressure = null, enAirPressureImportances? airPressureImportances = null,
            bool setTreadDepth = false, decimal? treadDepth = null, enTreadDepthImportances? treadDepthImportances = null,
            bool setTireImage = false)
        {
            try
            {
                string focusedPostionInfo = "";

                // pozisyon boş ise true dolu ise false bilgisi veriyor
                var PositionIsEmpty = !VehicleLayoutData.vehicleTires.Exists(t => t.AxleNumber == axle && t.Position == position && !string.IsNullOrWhiteSpace(t.TireProductGuid) && t.IsDeleted == false);

                foreach (View child in grdVehicleLayout.Children)
                {
                    if (TOOLS.ToString(child.ClassId).IndexOf(axle + "|" + position) < 0) continue;

                    focusedPostionInfo = child.ClassId;

                    string[] xAxlePositions = child.ClassId.Split('|');

                    string SelectImageType = "Select";
                    if (isTireWarning != null && isTireWarning.Value)
                        SelectImageType = "SelectWarning";

                    AbsoluteLayout layout = (AbsoluteLayout)child;

                    foreach (View Layoutchild in layout.Children)
                    {
                        if (!Layoutchild.ClassId.Equals("MainStc"))
                            continue;

                        StackLayout stcMainStc = (StackLayout)Layoutchild;

                        foreach (View mainStcDetail in stcMainStc.Children)
                        {
                            if (mainStcDetail.ClassId == "TireImageSelect")
                            {
                                if (isSelected is null) continue;

                                Image xImage = (Image)mainStcDetail;

                                if (isSelected == false)
                                {
                                    xImage.Source = "";
                                }
                                else
                                {
                                    switch (position.Substring(0, 1).ToString())
                                    {
                                        case "R":
                                            xImage.Source = "RTire" + ((isSelected.Value) ? SelectImageType : "");
                                            break;

                                        case "L":
                                            xImage.Source = "LTire" + ((isSelected.Value) ? SelectImageType : "");
                                            break;

                                        case "S":
                                            xImage.Source = "STire" + ((isSelected.Value) ? SelectImageType : "");
                                            break;
                                    }
                                }
                            }
                            else if (mainStcDetail.ClassId == "bxTopBackcolorInf")
                            {
                                BoxView boxView = (BoxView)mainStcDetail;
                                boxView.IsVisible = PositionIsEmpty ? false : true;

                                if (!setTreadDepth) continue;

                                // pozisyon bos ise kutucugu gorunmez yapıyorum
                                if (PositionIsEmpty)
                                {
                                    boxView.BackgroundColor = Color.Transparent;
                                    continue;
                                }
                                else if (treadDepth is null)
                                {
                                    boxView.BackgroundColor = Color.FromHex("#E2E8F0");
                                    continue;
                                }

                                boxView.BackgroundColor = GetTreadDepthImportances(treadDepthImportances);
                            }
                            else if (mainStcDetail.ClassId == "lblTopLabelInf")
                            {
                                Label label = (Label)mainStcDetail;
                                label.IsVisible = PositionIsEmpty ? false : true;

                                if (!setTreadDepth) continue;

                                label.TextColor = treadDepthImportances is null ? Color.Black : Color.FromHex("#FFFFFF");
                                label.Text = (treadDepth is null) ? (PositionIsEmpty ? "" : "-") : treadDepth.ToString();
                            }
                            else if (mainStcDetail.ClassId == "bxCenterBackcolorInf")
                            {
                                if (isTireWarning is null) continue;

                                BoxView boxView = (BoxView)mainStcDetail;
                                boxView.BackgroundColor = (isTireWarning.Value) ? Color.FromHex("#F16063") : Color.FromHex("#FFFFFF");
                            }
                            else if (mainStcDetail.ClassId == "lblCenterLabelInf")
                            {
                                if (isTireWarning is null) continue;

                                Label label = (Label)mainStcDetail;
                                label.TextColor = isTireWarning.Value ? Color.FromHex("#FFFFFF") : Color.FromHex("#27272E");
                            }
                            else if (mainStcDetail.ClassId == "bxBottomBackcolorInf")
                            {
                                BoxView boxView = (BoxView)mainStcDetail;
                                boxView.IsVisible = PositionIsEmpty ? false : true;

                                if (!setAirPressure) continue;

                                // pozisyon bos ise kutucugu gorunmez yapıyorum
                                if (PositionIsEmpty)
                                {
                                    boxView.BackgroundColor = Color.Transparent;
                                    continue;
                                }
                                else if (airPressure is null)
                                {
                                    boxView.BackgroundColor = Color.FromHex("#E2E8F0");
                                    continue;
                                }

                                boxView.BackgroundColor = GetAirPressureImportanceColor(airPressureImportances);
                            }
                            else if (mainStcDetail.ClassId == "lblBottomLabelInf")
                            {
                                Label label = (Label)mainStcDetail;
                                label.IsVisible = PositionIsEmpty ? false : true;

                                if (!setAirPressure) continue;

                                label.TextColor = airPressureImportances is null ? Color.Black : Color.FromHex("#FFFFFF");
                                label.Text = (airPressure is null) ? (PositionIsEmpty ? "" : "-") : airPressure.ToString();
                            }
                            else if (mainStcDetail.ClassId == "TireImage")
                            {
                                if (!setTireImage) continue;

                                Image image = (Image)mainStcDetail;

                                var tireImageSource = position.Substring(0, 1) + "Tire";

                                if (VehicleLayoutData.IsRotateOnRim && VehicleLayoutData.rotatedOnRimPositions.Exists(t => t.Equals(axle + "|" + position)))
                                    tireImageSource += "RimRotate";

                                if (!VehicleLayoutData.vehicleTires.Exists(t => t.AxleNumber == axle && t.Position == position && !string.IsNullOrWhiteSpace(t.TireProductGuid) && t.IsDeleted == false))
                                    tireImageSource += VehicleLayoutData.IsRotationVehicleLayout ? "AddEmpty" : "Empty";

                                image.Source = tireImageSource;
                            }
                        }
                    }
                }

                VehicleLayoutData.FocusedPostionInfo = focusedPostionInfo;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        Color GetTreadDepthImportances(enTreadDepthImportances? treadDepthImportances)
        {
            Color color;

            switch (treadDepthImportances)
            {
                case enTreadDepthImportances.PullPoint:
                    color = TreadDepthImportancesColor.PullPoint;
                    break;
                case enTreadDepthImportances.NearPullPoint:
                    color = TreadDepthImportancesColor.NearPullPoint;
                    break;
                case enTreadDepthImportances.AbovePullPoint:
                    color = TreadDepthImportancesColor.AbovePullPoint;
                    break;
                default:
                    color = AirPressureImportancesColor.Empty;
                    break;
            }

            return color;
        }

        Color GetAirPressureImportanceColor(enAirPressureImportances? airPressureImportances)
        {
            Color color;

            switch (airPressureImportances)
            {
                case enAirPressureImportances.VeryLow:
                    color = AirPressureImportancesColor.VeryLow;
                    break;
                case enAirPressureImportances.Low:
                    color = AirPressureImportancesColor.Low;
                    break;
                case enAirPressureImportances.Normal:
                    color = AirPressureImportancesColor.Normal;
                    break;
                case enAirPressureImportances.High:
                    color = AirPressureImportancesColor.High;
                    break;
                case enAirPressureImportances.VeryHigh:
                    color = AirPressureImportancesColor.VeryHigh;
                    break;
                default:
                    color = AirPressureImportancesColor.Empty;
                    break;
            }

            return color;
        }

        async void SelectedTire(View arg1, object arg2)
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                if (!VehicleLayoutData.IsPositionChangeApproval)
                    await SelectedTire(axlePosition: arg1.ClassId);
                else // pozisyon degiligi onay gerektiriyorsa onay alıp o sekilde degisiklik yapıyoruz
                    VehicleLayoutData.PositionChangeRequestCommand.Execute(arg1.ClassId);

                DoubleTapping.ResumeTap();
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);

                DoubleTapping.ResumeTap();
                return;
            }
        }

        async Task SelectedTire(string axlePosition)
        {
            try
            {
                string[] xAxlePositions = axlePosition.Split('|');

                VehicleLayoutData.FocusedPostionInfo = axlePosition;

                if (!VehicleLayoutData.IsCollectivePositionSelection) // birden fazla pozisyon seciyor ise bir onceki pozisyondan unfocus olmuyoruz
                    ChangeTireInfo(axle, position, isSelected: false, isTireWarning: null);

                axle = Convert.ToInt32(xAxlePositions[0]);
                position = xAxlePositions[1].ToString();

                bool _isSelected = true;

                // toplu secimlerde secilen pozisyonları bir list icerisinde topluyoruz aynı pozisyonu tekrar secerse sacede o pozisyonu unfocus yapıyoruz                
                if (VehicleLayoutData.IsCollectivePositionSelection)
                {
                    if (!VehicleLayoutData.vehicleTires.Exists(t => t.AxleNumber == axle && t.Position == position && t.IsDeleted == false && !string.IsNullOrWhiteSpace(t.TireProductGuid)))
                    {
                        DoubleTapping.ResumeTap();
                        return;
                    }

                    if (VehicleLayoutData.selectedPositions is null)
                        VehicleLayoutData.selectedPositions = new List<string>();

                    var selectedPosition = VehicleLayoutData.selectedPositions.FirstOrDefault(t => t.Equals(axle + "|" + position));

                    if (selectedPosition != null)
                    {
                        VehicleLayoutData.selectedPositions.Remove(selectedPosition);
                        _isSelected = false;
                    }
                    else
                        VehicleLayoutData.selectedPositions.Add(axle + "|" + position);
                }

                VehicleLayoutData.PositionChangedCommand.Execute(axlePosition);

                ChangeTireInfo(axle, position, isSelected: _isSelected, isTireWarning: null);

                DoubleTapping.ResumeTap();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}