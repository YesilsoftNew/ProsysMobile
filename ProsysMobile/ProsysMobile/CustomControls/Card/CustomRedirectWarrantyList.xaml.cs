using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Themes.Views;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Card
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomRedirectWarrantyList : PancakeView
    {
        #region properties

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
        /// The State property.
        /// </summary>
        public static readonly BindableProperty StateProperty = BindableProperty.Create(
            nameof(State),
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
        /// The RedirectServiceProvider property.
        /// </summary>
        public static readonly BindableProperty RedirectServiceProviderProperty = BindableProperty.Create(
            nameof(RedirectServiceProvider),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The RedirectStockCenter property.
        /// </summary>
        public static readonly BindableProperty RedirectStockCenterProperty = BindableProperty.Create(
            nameof(RedirectStockCenter),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The WarrantyReason property.
        /// </summary>
        public static readonly BindableProperty WarrantyReasonProperty = BindableProperty.Create(
            nameof(WarrantyReason),
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
        /// State value
        /// </summary>
        public string State
        {
            get
            {
                return (string)GetValue(StateProperty);
            }

            set
            {
                SetValue(StateProperty, value);
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
        /// RedirectServiceProvider value
        /// </summary>
        public string RedirectServiceProvider
        {
            get
            {
                return (string)GetValue(RedirectServiceProviderProperty);
            }

            set
            {
                SetValue(RedirectServiceProviderProperty, value);
            }
        }

        /// <summary>
        /// RedirectStockCenter value
        /// </summary>
        public string RedirectStockCenter
        {
            get
            {
                return (string)GetValue(RedirectStockCenterProperty);
            }

            set
            {
                SetValue(RedirectStockCenterProperty, value);
            }
        }

        /// <summary>
        /// WarrantyReason value
        /// </summary>
        public string WarrantyReason
        {
            get
            {
                return (string)GetValue(WarrantyReasonProperty);
            }

            set
            {
                SetValue(WarrantyReasonProperty, value);
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

            if (propertyName == DateProperty.PropertyName)
            {
                lblDate.Text = Date;
            }
            else if (propertyName == StateProperty.PropertyName)
            {
                lblState.Text = State;
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
            else if (propertyName == RedirectServiceProviderProperty.PropertyName)
            {
                lblRedirectServiceProvider.Text = RedirectServiceProvider;
            }
            else if (propertyName == RedirectStockCenterProperty.PropertyName)
            {
                lblRedirectStockCenter.Text = RedirectStockCenter;
            }
            else if (propertyName == WarrantyReasonProperty.PropertyName)
            {
                lblWarrantyReason.Text = WarrantyReason;
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

        public CustomRedirectWarrantyList()
        {
            InitializeComponent();
        }
    }
}