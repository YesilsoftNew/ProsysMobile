using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomFloatActionItem : Grid
    {

        /// <summary>
        /// Text Property
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(CustomFloatActionItem),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Description Property
        /// </summary>
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            nameof(Source),
            typeof(string),
            typeof(CustomFloatActionItem),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

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
        /// Source
        /// </summary>
        public string Source
        {
            get
            {
                return (string)GetValue(SourceProperty);
            }

            set
            {
                SetValue(SourceProperty, value);
            }
        }

        public event EventHandler<EventArgs> Clicked;

        public CustomFloatActionItem()
        {
            InitializeComponent();

            ItemLabel.Text = Text;
            ItemImage.Source = Source;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                ItemLabel.Text = Text;
            }
            else if (propertyName == SourceProperty.PropertyName)
            {
                ItemImage.Source = Source;
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await ItemGrid.ScaleTo(.8, 100, Easing.CubicIn);

            if (Clicked != null)
            {
                Clicked(sender, e);
            }

            await ItemGrid.ScaleTo(1, 100, Easing.CubicOut);
        }
    }
}