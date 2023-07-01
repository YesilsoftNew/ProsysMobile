using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WiseMobile.CustomControls.Button
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomStandardImageButton : ImageButton
    {
        /// <summary>
        /// ImageProperty
        /// Image Source
        /// </summary>
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource),
            typeof(string),
            typeof(CustomStandardImageButton),
            "ChevronLeftBlack",
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Placeholder
        /// </summary>
        public string ImageSource
        {
            get
            {
                return (string)GetValue(ImageSourceProperty);
            }

            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }

        /// <summary>
        /// Previous Clicked
        /// </summary>
        public event EventHandler<EventArgs> ImageButtonClicked;

        public CustomStandardImageButton()
        {
            InitializeComponent();

            ItemImageButton.Source = ImageSource;
        }

        /// <summary>
        /// Image Button Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ItemImageButton_Clicked(object sender, EventArgs e)
        {
            await ItemImageButton.ScaleTo(.7, 100, Easing.CubicIn);

            if (ImageButtonClicked != null)
            {
                ImageButtonClicked(sender,e);
            }
            await ItemImageButton.ScaleTo(1, 100, Easing.CubicOut);
        }

        /// <summary>
        /// Change Property Work Function
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ImageSourceProperty.PropertyName)
            {
                ItemImageButton.Source = ImageSource;
            }
        }
    }
}