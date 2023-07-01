using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using WiseMobile.CustomControls.Button;
using WiseMobile.Models.CommonModels.Enums;
using WiseMobile.Models.CommonModels.SQLiteModels;
using WiseMobile.Themes.Views;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace WiseMobile.CustomControls.Card
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomVehicleEntryOdometer : PancakeView
    {
        #region properties
        /// <summary>
        /// The percent property.
        /// </summary>
        public static readonly BindableProperty PercentProperty = BindableProperty.Create(
            nameof(Percent),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The Date property.
        /// </summary>
        public static readonly BindableProperty DateProperty = BindableProperty.Create(
            nameof(Date),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The AdditionCount property.
        /// </summary>
        public static readonly BindableProperty AdditionCountProperty = BindableProperty.Create(
            nameof(AdditionCount),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The Plate property.
        /// </summary>
        public static readonly BindableProperty PlateProperty = BindableProperty.Create(
            nameof(Plate),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The Fleet property.
        /// </summary>
        public static readonly BindableProperty FleetProperty = BindableProperty.Create(
            nameof(Fleet),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The FleetBranch property.
        /// </summary>
        public static readonly BindableProperty FleetBranchProperty = BindableProperty.Create(
            nameof(FleetBranch),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The VehicleType property.
        /// </summary>
        public static readonly BindableProperty VehicleTypeProperty = BindableProperty.Create(
            nameof(VehicleType),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The AxleType property.
        /// </summary>
        public static readonly BindableProperty AxleTypeProperty = BindableProperty.Create(
            nameof(AxleType),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The Odometer property.
        /// </summary>
        public static readonly BindableProperty OdometerProperty = BindableProperty.Create(
            nameof(Odometer),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The SendStatus property.
        /// </summary>
        public static readonly BindableProperty SendStatusProperty = BindableProperty.Create(
            nameof(SendStatus),
            typeof(CustomBadges),
            typeof(CustomTireActionNewBuy),
            default(CustomBadges),
            BindingMode.TwoWay);

        /// <summary>
        /// The ReasonForNotSending property.
        /// </summary>
        public static readonly BindableProperty ReasonForNotSendingProperty = BindableProperty.Create(
            nameof(ReasonForNotSending),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// IsDataManagement Property
        /// </summary>s
        public static readonly BindableProperty IsDataManagementProperty = BindableProperty.Create(nameof(IsDataManagement),
            typeof(bool),
            typeof(CustomTireActionNewBuy),
            default(bool),
            Xamarin.Forms.BindingMode.OneWay);
        #endregion

        #region definitions
        /// <summary>
        /// ProgressBar value
        /// </summary>
        public string Percent
        {
            get
            {
                return (string)GetValue(PercentProperty);
            }

            set
            {
                SetValue(PercentProperty, value);
            }
        }

        /// <summary>
        /// Date value
        /// </summary>
        public string Date
        {
            get
            {
                return (string)GetValue(DateProperty);
            }

            set
            {
                SetValue(DateProperty, value);
            }
        }

        /// <summary>
        /// AdditionCount value
        /// </summary>
        public string AdditionCount
        {
            get
            {
                return (string)GetValue(AdditionCountProperty);
            }

            set
            {
                SetValue(AdditionCountProperty, value);
            }
        }

        /// <summary>
        /// Plate value
        /// </summary>
        public string Plate
        {
            get
            {
                return (string)GetValue(PlateProperty);
            }

            set
            {
                SetValue(PlateProperty, value);
            }
        }

        /// <summary>
        /// Fleet value
        /// </summary>
        public string Fleet
        {
            get
            {
                return (string)GetValue(FleetProperty);
            }

            set
            {
                SetValue(FleetProperty, value);
            }
        }

        /// <summary>
        /// FleetBranch value
        /// </summary>
        public string FleetBranch
        {
            get
            {
                return (string)GetValue(FleetBranchProperty);
            }

            set
            {
                SetValue(FleetBranchProperty, value);
            }
        }

        /// <summary>
        /// VehicleType value
        /// </summary>
        public string VehicleType
        {
            get
            {
                return (string)GetValue(VehicleTypeProperty);
            }

            set
            {
                SetValue(VehicleTypeProperty, value);
            }
        }

        /// <summary>
        /// AxleType value
        /// </summary>
        public string AxleType
        {
            get
            {
                return (string)GetValue(AxleTypeProperty);
            }

            set
            {
                SetValue(AxleTypeProperty, value);
            }
        }

        /// <summary>
        /// Odometer value
        /// </summary>
        public string Odometer
        {
            get
            {
                return (string)GetValue(OdometerProperty);
            }

            set
            {
                SetValue(OdometerProperty, value);
            }
        }

        /// <summary>
        /// SendStatus value
        /// </summary>
        public CustomBadges SendStatus
        {
            get
            {
                return (CustomBadges)GetValue(SendStatusProperty);
            }

            set
            {
                SetValue(SendStatusProperty, value);
            }
        }

        /// <summary>
        /// ReasonForNotSending value
        /// </summary>
        public string ReasonForNotSending
        {
            get
            {
                return (string)GetValue(ReasonForNotSendingProperty);
            }

            set
            {
                SetValue(ReasonForNotSendingProperty, value);
            }
        }

        /// <summary>
        /// ServiceNo value
        /// </summary>
        public bool IsDataManagement
        {
            get
            {
                return (bool)GetValue(IsDataManagementProperty);
            }

            set
            {
                SetValue(IsDataManagementProperty, value);
            }
        }
        #endregion

        /// <summary>
        /// Change Property Function
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == PercentProperty.PropertyName)
            {
                lblPercent.Text = Percent;
                prgPercent.Progress = Convert.ToDouble(Percent) / 100;

                if (Percent != null)
                {
                    Application.Current.Resources.TryGetValue("Primary.Base", out var Orange);
                    Application.Current.Resources.TryGetValue("Green.Base", out var Green);
                    if (Convert.ToDouble(Percent) > 50)
                    {
                        lblPercent.TextColor = (Color)Green;
                        lblPercentSign.TextColor = (Color)Green;
                        prgPercent.RingProgressColor = (Color)Green;
                    }
                    else
                    {
                        lblPercent.TextColor = (Color)Orange;
                        lblPercentSign.TextColor = (Color)Orange;
                        prgPercent.RingProgressColor = (Color)Orange;
                    }
                }
            }
            else if (propertyName == DateProperty.PropertyName)
            {
                lblDate.Text = Date;
            }
            else if (propertyName == AdditionCountProperty.PropertyName)
            {
                lblAdditionCount.Text = AdditionCount;
            }
            else if (propertyName == PlateProperty.PropertyName)
            {
                lblPlate.Text = Plate;
            }
            else if (propertyName == FleetProperty.PropertyName)
            {
                lblFleet.Text = Fleet;
            }
            else if (propertyName == AxleTypeProperty.PropertyName)
            {
                lblAxleType.Text = AxleType;
            }
            else if (propertyName == VehicleTypeProperty.PropertyName)
            {
                lblVehicleType.Text = VehicleType;
            }
            else if (propertyName == OdometerProperty.PropertyName)
            {
                btnOdometer.Text = Odometer;
            }
            else if (propertyName == FleetBranchProperty.PropertyName)
            {
                lblFleetBranch.Text = FleetBranch;
            }
            else if (propertyName == SendStatusProperty.PropertyName)
            {
                bdgSendStatus = SendStatus;
            }
            else if (propertyName == ReasonForNotSendingProperty.PropertyName)
            {
                lblReasonForNotSending.Text = ReasonForNotSending;
            }
            else if (propertyName == IsDataManagementProperty.PropertyName)
            {
                grdDataManagement.IsVisible = IsDataManagement;
            }
        }

        public CustomVehicleEntryOdometer()
        {
            InitializeComponent();
        }
    }
}