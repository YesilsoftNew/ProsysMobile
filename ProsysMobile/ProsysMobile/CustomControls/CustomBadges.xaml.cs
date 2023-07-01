using ProsysMobile.Models.CommonModels.Enums;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomBadges : PancakeView
    {

        /// <summary>
        /// The type property.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(CustomBadges),
            default(string),
            BindingMode.TwoWay);

        /// <summary>
        /// The type property.
        /// </summary>
        public static readonly BindableProperty TypeProperty = BindableProperty.Create(
            nameof(Type),
            typeof(enCustomBadgesType),
            typeof(CustomBadges),
            null,
            BindingMode.TwoWay);

        /// <summary>
        /// The cell's Type
        /// </summary>
        public enCustomBadgesType Type
        {
            get
            {
                return (enCustomBadgesType)GetValue(TypeProperty);
            }

            set
            {
                SetValue(TypeProperty, value);
            }
        }

        /// <summary>
        /// The cell's Type
        /// </summary>
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }

        /// <summary>
        /// Change Property Function
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TypeProperty.PropertyName)
            {
                changeDesign();
            }
            else if (propertyName == TextProperty.PropertyName)
            {
                if (Text == "false")
                    ItemBorder.IsVisible = false;
                else
                {
                    ItemBorder.IsVisible = true;
                    ItemLabel.Text = Text;
                }
            }
        }

        /// <summary>
        /// Change design
        /// </summary>
        void changeDesign()
        {
            switch (Type)
            {
                case enCustomBadgesType.Success:
                    Application.Current.Resources.TryGetValue("Views.Badges.Success", out var successStyle);
                    Application.Current.Resources.TryGetValue("Views.Badges.SuccessLabel", out var successLabelStype);

                    changeControlProperties(successStyle as Style, successLabelStype as Style);

                    break;
                case enCustomBadgesType.Alert:
                    Application.Current.Resources.TryGetValue("Views.Badges.Alert", out var alertStyle);
                    Application.Current.Resources.TryGetValue("Views.Badges.AlertLabel", out var alertLabelStyle);

                    changeControlProperties(alertStyle as Style, alertLabelStyle as Style);

                    break;
                case enCustomBadgesType.Warning:
                    Application.Current.Resources.TryGetValue("Views.Badges.Warning", out var warningStyle);
                    Application.Current.Resources.TryGetValue("Views.Badges.WarningLabel", out var warningLabelStyle);

                    changeControlProperties(warningStyle as Style, warningLabelStyle as Style);

                    break;
                case enCustomBadgesType.Info:
                    Application.Current.Resources.TryGetValue("Views.Badges.Info", out var infoStyle);
                    Application.Current.Resources.TryGetValue("Views.Badges.InfoLabel", out var infoLabelStyle);

                    changeControlProperties(infoStyle as Style, infoLabelStyle as Style);

                    break;
                case enCustomBadgesType.DarkInfo:
                    Application.Current.Resources.TryGetValue("Views.Badges.DarkInfo", out var DarkinfoStyle);
                    Application.Current.Resources.TryGetValue("Views.Badges.DarkInfoLabel", out var DarkinfoLabelStyle);

                    changeControlProperties(DarkinfoStyle as Style, DarkinfoLabelStyle as Style);

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
        void changeControlProperties(Style mainStyle, Style labelStyle)
        {
            ItemBorder.Style = mainStyle;
            ItemLabel.Style = labelStyle;
        }

        public CustomBadges()
        {
            InitializeComponent();
        }
    }
}