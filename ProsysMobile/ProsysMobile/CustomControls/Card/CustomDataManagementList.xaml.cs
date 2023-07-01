using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Card
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomDataManagementList : PancakeView
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
        /// The Info property.
        /// </summary>
        public static readonly BindableProperty InfoProperty = BindableProperty.Create(
            nameof(Info),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The TireCount property.
        /// </summary>
        public static readonly BindableProperty TireCountProperty = BindableProperty.Create(
            nameof(TireCount),
            typeof(string),
            typeof(CustomTireActionNewBuy),
            default(string),
            BindingMode.TwoWay);

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
        /// Info value
        /// </summary>
        public string Info
        {
            get
            {
                return (string)GetValue(InfoProperty);
            }

            set
            {
                SetValue(InfoProperty, value);
            }
        }

        /// <summary>
        /// TireCount value
        /// </summary>
        public string TireCount
        {
            get
            {
                return (string)GetValue(TireCountProperty);
            }

            set
            {
                SetValue(TireCountProperty, value);
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
            else if (propertyName == InfoProperty.PropertyName)
            {
                lblInfo.Text = Info;
            }
            else if (propertyName == TireCountProperty.PropertyName)
            {
                lblTireCount.Text = TireCount;
            }
        }

        public CustomDataManagementList()
        {
            InitializeComponent();
        }
    }
}