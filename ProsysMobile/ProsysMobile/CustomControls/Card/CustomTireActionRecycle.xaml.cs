using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Themes.Views;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Card
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTireActionRecycle : PancakeView
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
        /// The TireInformation property.
        /// </summary>
        public static readonly BindableProperty TireInformationProperty = BindableProperty.Create(
            nameof(TireInformation),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The Barcode property.
        /// </summary>
        public static readonly BindableProperty BarcodeProperty = BindableProperty.Create(
            nameof(Barcode),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The TireType property.
        /// </summary>
        public static readonly BindableProperty TireTypeProperty = BindableProperty.Create(
            nameof(TireType),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The StockCenter property.
        /// </summary>
        public static readonly BindableProperty StockCenterProperty = BindableProperty.Create(
            nameof(StockCenter),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The TreadDepth property.
        /// </summary>
        public static readonly BindableProperty TreadDepthProperty = BindableProperty.Create(
            nameof(TreadDepth),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The DamageReason property.
        /// </summary>
        public static readonly BindableProperty DamageReasonProperty = BindableProperty.Create(
            nameof(DamageReason),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The ServiceProvider property.
        /// </summary>
        public static readonly BindableProperty ServiceProviderProperty = BindableProperty.Create(
            nameof(ServiceProvider),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The ServiceNo property.
        /// </summary>
        public static readonly BindableProperty ServiceNoProperty = BindableProperty.Create(
            nameof(ServiceNo),
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
        /// TireInformation value
        /// </summary>
        public string TireInformation
        {
            get
            {
                return (string)GetValue(TireInformationProperty);
            }

            set
            {
                SetValue(TireInformationProperty, value);
            }
        }

        /// <summary>
        /// Barcode value
        /// </summary>
        public string Barcode
        {
            get
            {
                return (string)GetValue(BarcodeProperty);
            }

            set
            {
                SetValue(BarcodeProperty, value);
            }
        }

        /// <summary>
        /// TireType value
        /// </summary>
        public string TireType
        {
            get
            {
                return (string)GetValue(TireTypeProperty);
            }

            set
            {
                SetValue(TireTypeProperty, value);
            }
        }

        /// <summary>
        /// StockCenter value
        /// </summary>
        public string StockCenter
        {
            get
            {
                return (string)GetValue(StockCenterProperty);
            }

            set
            {
                SetValue(StockCenterProperty, value);
            }
        }

        /// <summary>
        /// TreadDepth value
        /// </summary>
        public string TreadDepth
        {
            get
            {
                return (string)GetValue(TreadDepthProperty);
            }

            set
            {
                SetValue(TreadDepthProperty, value);
            }
        }

        /// <summary>
        /// DamageReason value
        /// </summary>
        public string DamageReason
        {
            get
            {
                return (string)GetValue(DamageReasonProperty);
            }

            set
            {
                SetValue(DamageReasonProperty, value);
            }
        }

        /// <summary>
        /// ServiceProvider value
        /// </summary>
        public string ServiceProvider
        {
            get
            {
                return (string)GetValue(ServiceProviderProperty);
            }

            set
            {
                SetValue(ServiceProviderProperty, value);
            }
        }

        /// <summary>
        /// ServiceNo value
        /// </summary>
        public string ServiceNo
        {
            get
            {
                return (string)GetValue(ServiceNoProperty);
            }

            set
            {
                SetValue(ServiceNoProperty, value);
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
            try
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
                else if (propertyName == TireInformationProperty.PropertyName)
                {
                    lblTireInformation.Text = TireInformation;
                }
                else if (propertyName == BarcodeProperty.PropertyName)
                {
                    lblBarcode.Text = Barcode;
                }
                else if (propertyName == TireTypeProperty.PropertyName)
                {
                    lblTireType.Text = TireType;
                }
                else if (propertyName == StockCenterProperty.PropertyName)
                {
                    lblStockCenter.Text = StockCenter;
                }
                else if (propertyName == TreadDepthProperty.PropertyName)
                {
                    lblTreadDepth.Text = TreadDepth;
                }
                else if (propertyName == DamageReasonProperty.PropertyName)
                {
                    lblDamageReason.Text = DamageReason;
                }
                else if (propertyName == ServiceProviderProperty.PropertyName)
                {
                    lblServiceProvider.Text = ServiceProvider;
                }
                else if (propertyName == ServiceNoProperty.PropertyName)
                {
                    lblServiceNo.Text = ServiceNo;
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
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);

            }

        }

        public CustomTireActionRecycle()
        {
            InitializeComponent();
        }
    }
}