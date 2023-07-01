using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using WiseMobile.Models.CommonModels.Enums;
using WiseMobile.Models.CommonModels.SQLiteModels;
using WiseMobile.Themes.Views;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace WiseMobile.CustomControls.Card
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTireActionVehicleTransfer : PancakeView
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
        /// The ExitBranch property.
        /// </summary>
        public static readonly BindableProperty ExitBranchProperty = BindableProperty.Create(
            nameof(ExitBranch),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The ExitLocation property.
        /// </summary>
        public static readonly BindableProperty ExitLocationProperty = BindableProperty.Create(
            nameof(ExitLocation),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The ArrivalBranch property.
        /// </summary>
        public static readonly BindableProperty ArrivalBranchProperty = BindableProperty.Create(
            nameof(ArrivalBranch),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The ArrivalLocation property.
        /// </summary>
        public static readonly BindableProperty ArrivalLocationProperty = BindableProperty.Create(
            nameof(ArrivalLocation),
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
        /// ExitBranch value
        /// </summary>
        public string ExitBranch
        {
            get
            {
                return (string)GetValue(ExitBranchProperty);
            }

            set
            {
                SetValue(ExitBranchProperty, value);
            }
        }

        /// <summary>
        /// ExitLocation value
        /// </summary>
        public string ExitLocation
        {
            get
            {
                return (string)GetValue(ExitLocationProperty);
            }

            set
            {
                SetValue(ExitLocationProperty, value);
            }
        }

        /// <summary>
        /// ArrivalBranch value
        /// </summary>
        public string ArrivalBranch
        {
            get
            {
                return (string)GetValue(ArrivalBranchProperty);
            }

            set
            {
                SetValue(ArrivalBranchProperty, value);
            }
        }

        /// <summary>
        /// ArrivalLocation value
        /// </summary>
        public string ArrivalLocation
        {
            get
            {
                return (string)GetValue(ArrivalLocationProperty);
            }

            set
            {
                SetValue(ArrivalLocationProperty, value);
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
            else if (propertyName == ExitBranchProperty.PropertyName)
            {
                lblExitBranch.Text = ExitBranch;
            }
            else if (propertyName == ExitLocationProperty.PropertyName)
            {
                lblExitLocation.Text = ExitLocation;
            }
            else if (propertyName == ArrivalBranchProperty.PropertyName)
            {
                lblArrivalBranch.Text = ArrivalBranch;
            }
            else if (propertyName == ArrivalLocationProperty.PropertyName)
            {
                lblArrivalLocation.Text = ArrivalLocation;
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

        public CustomTireActionVehicleTransfer()
        {
            InitializeComponent();
        }
    }
}