using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Other
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomSubCategoryGrid 
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string),
            typeof(CustomSubCategoryGrid),
            default(string),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color),
            typeof(Color),
            typeof(CustomSubCategoryGrid),
            default(Color),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public CustomSubCategoryGrid()
        {
            InitializeComponent();

            ItemLabel.Text = Text;
            ItemLabel.TextColor = Color;
            ItemMain.Border.Color = Color;
        }
        
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                ItemLabel.Text = Text;
            }
            else if (propertyName == ColorProperty.PropertyName)
            {
                ItemLabel.TextColor = Color;
                ItemMain.Border.Color = Color;
            }
        }

    }
}