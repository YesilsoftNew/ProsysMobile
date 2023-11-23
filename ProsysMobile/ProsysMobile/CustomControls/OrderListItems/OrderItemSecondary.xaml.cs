using System;
using System.Windows.Input;
using ProsysMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.OrderListItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderItemSecondary
    {
        public static readonly BindableProperty PriceTextProperty = BindableProperty.Create(nameof(PriceText),
            typeof(string),
            typeof(OrderItemSecondary),
            default(string),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty NameTextProperty = BindableProperty.Create(nameof(NameText),
            typeof(string),
            typeof(OrderItemSecondary),
            default(string),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty PiecesTextProperty = BindableProperty.Create(nameof(PiecesText),
            typeof(string),
            typeof(OrderItemSecondary),
            default(string),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource),
            typeof(string),
            typeof(OrderItemSecondary),
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
        
        public static readonly BindableProperty IsAddedBasketProperty = BindableProperty.Create(nameof(IsAddedBasket),
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
        
        public bool IsAddedBasket
        {
            get => (bool)GetValue(IsAddedBasketProperty);
            set => SetValue(IsAddedBasketProperty, value);
        }
        
        public OrderItemSecondary()
        {
            InitializeComponent();
            
            ItemPrice.Text = PriceText;
            ItemName.Text = NameText;
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
            else if (propertyName == PiecesTextProperty.PropertyName)
            {
                ItemPieces.Text = PiecesText;
            }
            else if (propertyName == ImageSourceProperty.PropertyName)
            {
                ItemImage.Source = ImageSource;
            }
            else if (propertyName == IsFavoriteProperty.PropertyName)
            {
                ItemImageButton.Source = IsFavorite ? Constants.SelectedFavoriteImageSource : Constants.UnSelectedFavoriteImageSource;
            }
            else if (propertyName == IsAddedBasketProperty.PropertyName)
            {
                IsAddedBasketImage.IsVisible = IsAddedBasket;
            }
        }

        private void ItemImageButton_OnClicked(object sender, EventArgs e)
        {
            FavoriteCommand?.Execute(FavoriteCommandParameter);
        }
    }
}