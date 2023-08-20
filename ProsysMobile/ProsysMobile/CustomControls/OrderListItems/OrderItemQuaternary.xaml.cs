using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.OrderListItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderItemQuaternary
    {
       public static readonly BindableProperty PriceTextProperty = BindableProperty.Create(nameof(PriceText),
            typeof(string),
            typeof(OrderItemQuaternary),
            default(string),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty NameTextProperty = BindableProperty.Create(nameof(NameText),
            typeof(string),
            typeof(OrderItemQuaternary),
            default(string),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty PiecesTextProperty = BindableProperty.Create(nameof(PiecesText),
            typeof(string),
            typeof(OrderItemQuaternary),
            default(string),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty IsVisibleMarketImageProperty = BindableProperty.Create(nameof(IsVisibleMarketImage),
            typeof(bool),
            typeof(OrderItemQuaternary),
            true,
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty UnitPriceTextProperty = BindableProperty.Create(nameof(UnitPriceText),
            typeof(string),
            typeof(OrderItemQuaternary),
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
        
        public bool IsVisibleMarketImage
        {
            get => (bool)GetValue(IsVisibleMarketImageProperty);
            set => SetValue(IsVisibleMarketImageProperty, value);
        }
        
        public string UnitPriceText
        {
            get => (string)GetValue(UnitPriceTextProperty);
            set => SetValue(UnitPriceTextProperty, value);
        }
        
        public OrderItemQuaternary()
        {
            InitializeComponent();
            
            ItemPrice.Text = PriceText;
            ItemName.Text = NameText;
            ItemPieces.Text = PiecesText;
            ItemImageButton.IsVisible = IsVisibleMarketImage;
            ItemUnitPrice.Text = UnitPriceText;
            ItemUnitPrice.IsVisible = false;
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
            else if (propertyName == IsVisibleMarketImageProperty.PropertyName)
            {
                ItemImageButton.IsVisible = IsVisibleMarketImage;
            }
            else if (propertyName == UnitPriceTextProperty.PropertyName)
            {
                ItemUnitPrice.IsVisible = !string.IsNullOrWhiteSpace(UnitPriceText);
                ItemUnitPrice.Text = UnitPriceText;
            }
        }
    }
}