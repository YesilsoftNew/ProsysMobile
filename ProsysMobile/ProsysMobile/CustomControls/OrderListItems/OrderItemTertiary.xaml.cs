using System;
using System.Windows.Input;
using ProsysMobile.Helper;
using ProsysMobile.Resources.Language;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.OrderListItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderItemTertiary
    {
        public static readonly BindableProperty PriceTextProperty = BindableProperty.Create(nameof(PriceText),
            typeof(string),
            typeof(OrderItemTertiary),
            default(string),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty NameTextProperty = BindableProperty.Create(nameof(NameText),
            typeof(string),
            typeof(OrderItemTertiary),
            default(string),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty PiecesTextProperty = BindableProperty.Create(nameof(PiecesText),
            typeof(string),
            typeof(OrderItemTertiary),
            default(string),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource),
            typeof(string),
            typeof(OrderItemTertiary),
            default(string),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty IsFavoriteProperty = BindableProperty.Create(nameof(IsFavorite),
            typeof(bool),
            typeof(OrderItemSecondary),
            default(bool),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty FavoriteCommandProperty = BindableProperty.Create(nameof(FavoriteCommand),
            typeof(ICommand),
            typeof(OrderItemSecondary),
            default(ICommand),
            BindingMode.TwoWay);
        
        public static BindableProperty FavoriteCommandParameterProperty = BindableProperty.CreateAttached(
            nameof(FavoriteCommandParameter),
            typeof(object),
            typeof(OrderItemSecondary),
            null,
            BindingMode.TwoWay);
        
        public static readonly BindableProperty Tag1TextProperty = BindableProperty.Create(nameof(Tag1Text),
            typeof(string),
            typeof(OrderItemTertiary),
            default(string),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty Tag2TextProperty = BindableProperty.Create(nameof(Tag2Text),
            typeof(string),
            typeof(OrderItemTertiary),
            default(string),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty Tag3TextProperty = BindableProperty.Create(nameof(Tag3Text),
            typeof(string),
            typeof(OrderItemTertiary),
            default(string),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty Tag4TextProperty = BindableProperty.Create(nameof(Tag4Text),
            typeof(string),
            typeof(OrderItemTertiary),
            default(string),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty Tag1ColorProperty = BindableProperty.Create(nameof(Tag1Color),
            typeof(Color),
            typeof(OrderItemTertiary),
            default(Color),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty Tag2ColorProperty = BindableProperty.Create(nameof(Tag2Color),
            typeof(Color),
            typeof(OrderItemTertiary),
            default(Color),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty Tag3ColorProperty = BindableProperty.Create(nameof(Tag3Color),
            typeof(Color),
            typeof(OrderItemTertiary),
            default(Color),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty Tag4ColorProperty = BindableProperty.Create(nameof(Tag4Color),
            typeof(Color),
            typeof(OrderItemTertiary),
            default(Color),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty IsAddedBasketProperty = BindableProperty.Create(nameof(IsAddedBasket),
            typeof(bool),
            typeof(OrderItemTertiary),
            false,
            BindingMode.TwoWay);
        
        public static readonly BindableProperty UnitPriceTextProperty = BindableProperty.Create(nameof(UnitPriceText),
            typeof(string),
            typeof(OrderItemTertiary),
            string.Empty,
            BindingMode.TwoWay);
        
        public static readonly BindableProperty IsStockFinishedProperty = BindableProperty.Create(nameof(IsStockFinished),
            typeof(bool),
            typeof(OrderItemTertiary),
            false,
            BindingMode.TwoWay);
        
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
        
        public bool IsFavorite
        {
            get => (bool)GetValue(IsFavoriteProperty);
            set => SetValue(IsFavoriteProperty, value);
        }
        
        public ICommand FavoriteCommand
        {
            get => (Command)GetValue(FavoriteCommandProperty);
            set => SetValue(FavoriteCommandProperty, value);
        }
        
        public object FavoriteCommandParameter
        {
            get => GetValue(FavoriteCommandParameterProperty);
            set => SetValue(FavoriteCommandParameterProperty, value);
        }
        
        public string Tag1Text
        {
            get => (string)GetValue(Tag1TextProperty);
            set => SetValue(Tag1TextProperty, value);
        }
        
        public string Tag2Text
        {
            get => (string)GetValue(Tag2TextProperty);
            set => SetValue(Tag2TextProperty, value);
        }
        
        public string Tag3Text
        {
            get => (string)GetValue(Tag3TextProperty);
            set => SetValue(Tag3TextProperty, value);
        }
        
        public string Tag4Text
        {
            get => (string)GetValue(Tag4TextProperty);
            set => SetValue(Tag4TextProperty, value);
        }
        
                
        public Color Tag1Color
        {
            get => (Color)GetValue(Tag1ColorProperty);
            set => SetValue(Tag1ColorProperty, value);
        }
        
        public Color Tag2Color
        {
            get => (Color)GetValue(Tag2ColorProperty);
            set => SetValue(Tag2ColorProperty, value);
        }
        
        public Color Tag3Color
        {
            get => (Color)GetValue(Tag3ColorProperty);
            set => SetValue(Tag3ColorProperty, value);
        }
        
        public Color Tag4Color
        {
            get => (Color)GetValue(Tag4ColorProperty);
            set => SetValue(Tag4ColorProperty, value);
        }
        
        public bool IsAddedBasket
        {
            get => (bool)GetValue(IsAddedBasketProperty);
            set => SetValue(IsAddedBasketProperty, value);
        }
        
        public string UnitPriceText
        {
            get => (string)GetValue(UnitPriceTextProperty);
            set => SetValue(UnitPriceTextProperty, value);
        }
        
        public bool IsStockFinished
        {
            get => (bool)GetValue(IsStockFinishedProperty);
            set => SetValue(IsStockFinishedProperty, value);
        }
        
        public OrderItemTertiary()
        {
            InitializeComponent();
            
            ItemPrice.Text = PriceText;
            ItemName.Text = NameText;
            ItemUnitPrice.Text = UnitPriceText;
            ItemPieces.Text = PiecesText;
            ItemImage.Source = ImageSource;
            ItemImageButton.Source = Constants.UnSelectedFavoriteImageSource;
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
            else if (propertyName == UnitPriceTextProperty.PropertyName)
            {
                ItemUnitPrice.Text = UnitPriceText;
            }
            else if (propertyName == PiecesTextProperty.PropertyName)
            {
                ItemPieces.Text = PiecesText;
                
                Application.Current.Resources.TryGetValue("Gray3", out var gray3Color);
                
                if (gray3Color == null) return;
                
                var gray3 = (Color)gray3Color;

                ItemPieces.TextColor = gray3;
                
            }
            else if (propertyName == ImageSourceProperty.PropertyName)
            {
                ItemImage.Source = ImageSource;
            }
            else if (propertyName == IsFavoriteProperty.PropertyName)
            {
                ItemImageButton.Source = IsFavorite ? Constants.SelectedFavoriteImageSource : Constants.UnSelectedFavoriteImageSource;
            }
            else if (propertyName == Tag1TextProperty.PropertyName)
            {
                Tag1.Text = Tag1Text;
            }
            else if (propertyName == Tag2TextProperty.PropertyName)
            {
                Tag2.Text = Tag2Text;
            }
            else if (propertyName == Tag3TextProperty.PropertyName)
            {
                Tag3.Text = Tag3Text;
            }
            else if (propertyName == Tag4TextProperty.PropertyName)
            {
                Tag4.Text = Tag4Text;
            }
            else if (propertyName == Tag1ColorProperty.PropertyName)
            {
                Tag1.BackgroundColor = Tag1Color;
            }
            else if (propertyName == Tag2ColorProperty.PropertyName)
            {
                Tag2.BackgroundColor = Tag2Color;
            }
            else if (propertyName == Tag3ColorProperty.PropertyName)
            {
                Tag3.BackgroundColor = Tag3Color;
            }
            else if (propertyName == Tag4ColorProperty.PropertyName)
            {
                Tag4.BackgroundColor = Tag4Color;
            }
            else if (propertyName == IsAddedBasketProperty.PropertyName)
            {
                IsAddedBasketImage.IsVisible = IsAddedBasket;
            }
            else if (propertyName == IsStockFinishedProperty.PropertyName)
            {
                Fade.IsVisible = IsStockFinished;
                SoldOut.IsVisible = IsStockFinished;
            }
        }
        
        private void ItemImageButton_OnClicked(object sender, EventArgs e)
        {
            FavoriteCommand?.Execute(FavoriteCommandParameter);
        }
    }
}