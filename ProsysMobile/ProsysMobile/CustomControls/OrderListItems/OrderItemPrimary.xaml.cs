using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.OrderListItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderItemPrimary
    {
        
        public static readonly BindableProperty PriceTextProperty = BindableProperty.Create(nameof(PriceText),
            typeof(string),
            typeof(OrderItemPrimary),
            default(string),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty NameTextProperty = BindableProperty.Create(nameof(NameText),
            typeof(string),
            typeof(OrderItemPrimary),
            default(string),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty PiecesTextProperty = BindableProperty.Create(nameof(PiecesText),
            typeof(string),
            typeof(OrderItemPrimary),
            default(string),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource),
            typeof(string),
            typeof(OrderItemPrimary),
            default(string),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public string PriceText
        {
            get => (string)GetValue(PriceTextProperty);
            set => SetValue(PriceTextProperty, value);
        }
        
        public string NameText
        {
            get => (string)GetValue(NameTextProperty);
            set => SetValue(NameTextProperty, value);
        }
        
        public string PiecesText
        {
            get => (string)GetValue(PiecesTextProperty);
            set => SetValue(PiecesTextProperty, value);
        }
        
        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
        
        public OrderItemPrimary()
        {
            InitializeComponent();
            
            ItemPrice.Text = PriceText;
            ItemName.Text = NameText;
            ItemPieces.Text = PiecesText;
            ItemImage.Source = ImageSource;
        }
        
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == PriceTextProperty.PropertyName)
            {
                ItemPrice.Text = PriceText;
            } 
            else if (propertyName == NameTextProperty.PropertyName)
            {
                ItemName.Text = NameText;
            }
            else if (propertyName == PiecesTextProperty.PropertyName)
            {
                ItemPieces.Text = PiecesText;
            }
            else if (propertyName == ImageSourceProperty.PropertyName)
            {
                ItemImage.Source = ImageSource;
            }
        }
    }
}