using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomSwitch : Grid
    {

        /// <summary>
        /// Text Property
        /// Entry text
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomSwitch), default(string), BindingMode.TwoWay);

        /// <summary>
        /// IsToggled
        /// Switch IsToggled
        /// </summary>
        public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(CustomSwitch), default(bool), Xamarin.Forms.BindingMode.TwoWay);

        /// <summary>
        /// Text
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
        /// IsToggled
        /// </summary>
        public bool IsToggled
        {
            get
            {
                return (bool)GetValue(IsToggledProperty);
            }

            set
            {
                SetValue(IsToggledProperty, value);
            }
        }

        /// <summary>
        /// Toggled Event
        /// </summary>
        public event EventHandler<ToggledEventArgs> SwitchToggled;

        public CustomSwitch()
        {
            InitializeComponent();

            ItemSwitch.IsToggled = IsToggled;
            ItemLabel.Text = Text;
        }

        /// <summary>
        /// Property Changed
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsToggledProperty.PropertyName)
            {
                ItemSwitch.IsToggled = IsToggled;
            }
            else if (propertyName == TextProperty.PropertyName)
            {
                ItemLabel.Text = Text;
            }
        }

        /// <summary>
        /// Switch change function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            Application.Current.Resources.TryGetValue("Ink.Base", out var unToggleColor);
            Application.Current.Resources.TryGetValue("Primary.Base", out var toggleColor);

            var target = sender as Switch;
            target.ThumbColor = e.Value ? (Color)toggleColor : (Color)unToggleColor;
            IsToggled = e.Value;

            if (SwitchToggled != null)
            {
                SwitchToggled(sender,e);
            }
        }
    }
}