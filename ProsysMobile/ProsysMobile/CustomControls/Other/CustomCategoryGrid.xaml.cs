using System;
using ProsysMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Other
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomCategoryGrid 
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string),
            typeof(CustomCategoryGrid),
            default(string),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty LabelStyleProperty = BindableProperty.Create(nameof(LabelStyle),
            typeof(Style),
            typeof(CustomCategoryGrid),
            default(Style),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource),
            typeof(string),
            typeof(CustomCategoryGrid),
            default(string),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty ImageHeightProperty = BindableProperty.Create(nameof(ImageHeight),
            typeof(int),
            typeof(CustomCategoryGrid),
            default(string),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty ImageWidthProperty = BindableProperty.Create(nameof(ImageWidth),
            typeof(int),
            typeof(CustomCategoryGrid),
            default(string),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty IsClickableProperty = BindableProperty.Create(nameof(IsClickable),
            typeof(bool),
            typeof(CustomCategoryGrid),
            default(bool),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        
        public Style LabelStyle
        {
            get => (Style)GetValue(LabelStyleProperty);
            set => SetValue(LabelStyleProperty, value);
        }
        
        public int ImageHeight
        {
            get => (int)GetValue(ImageHeightProperty);
            set => SetValue(ImageHeightProperty, value);
        }
        
        public int ImageWidth
        {
            get => (int)GetValue(ImageWidthProperty);
            set => SetValue(ImageWidthProperty, value);
        }

        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
        
        public bool IsClickable
        {
            get => (bool)GetValue(IsClickableProperty);
            set => SetValue(IsClickableProperty, value);
        }

        public CustomCategoryGrid()
        {
            InitializeComponent();

            ItemLabel.Text = Text;
            ItemLabel.Style = LabelStyle;
            ItemImage.Source = ImageSource;
            ItemImage.HeightRequest = ImageHeight;
            ItemImage.WidthRequest = ImageWidth;
        }
        
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                ItemLabel.Text = Text;
            }
            if (propertyName == LabelStyleProperty.PropertyName)
            {
                ItemLabel.Style = LabelStyle;
            }
            else if (propertyName == ImageSourceProperty.PropertyName)
            {
                ItemImage.Source = ImageSource;
            }
            else if (propertyName == ImageWidthProperty.PropertyName)
            {
                ItemImage.WidthRequest = ImageWidth;
            }
            else if (propertyName == ImageHeightProperty.PropertyName)
            {
                ItemImage.HeightRequest = ImageHeight;
            }
            else if (propertyName == IsClickableProperty.PropertyName)
            {
                if (IsClickable)
                {
                    MainGrid.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(x =>
                        {
                            try
                            {
                                if (ItemPancakeView.Border == null)
                                {
                                    Application.Current.Resources.TryGetValue("Gray5", out var grayColor);
                                    Application.Current.Resources.TryGetValue("Black3", out var blackColor);

                                    if (grayColor != null)
                                    {
                                        ItemPancakeView.Border = new Border() { Thickness = 1, Color = (Color)grayColor };
                                    }

                                    if (blackColor != null)
                                    {
                                        ItemLabel.TextColor = (Color)blackColor;
                                    }
                                }
                                else
                                {
                                    Application.Current.Resources.TryGetValue("Gray3", out var defaultColor);

                                    ItemPancakeView.Border = null;
                                    
                                    if (defaultColor != null)
                                    {
                                        ItemLabel.TextColor = (Color)defaultColor;
                                    }
                                }
                        
                            }
                            catch (Exception ex)
                            {
                                ProsysLogger.Instance.CrashLog(ex);
                            }
                        })
                    });  
                }
            }
        }

    }
}