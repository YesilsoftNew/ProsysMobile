using System;
using ProsysMobile.Helper;
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
            BindingMode.TwoWay);
        
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color),
            typeof(Color),
            typeof(CustomSubCategoryGrid),
            default(Color),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty IsClickableProperty = BindableProperty.Create(nameof(IsClickable),
            typeof(bool),
            typeof(CustomSubCategoryGrid),
            default(bool),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected),
            typeof(bool),
            typeof(CustomSubCategoryGrid),
            default(bool),
            BindingMode.TwoWay);
        
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
        
        public bool IsClickable
        {
            get => (bool)GetValue(IsClickableProperty);
            set => SetValue(IsClickableProperty, value);
        }
        
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
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
            else if (propertyName == IsSelectedProperty.PropertyName)
            {
                if (IsClickable)
                {
                    if (IsSelected)
                    {
                        Application.Current.Resources.TryGetValue("Color1", out var color1Color);
                        var color1 = (Color?)color1Color;
                        
                        Application.Current.Resources.TryGetValue("White1", out var whiteColor);
                        var white = (Color?)whiteColor;

                        if (color1 == null) return;
                        if (white == null) return;
                        
                        ItemMain.Border = new Border { Thickness = 1, Color = (Color)color1 };
                        ItemMain.BackgroundColor = (Color)color1;
                        ItemLabel.BackgroundColor = (Color)color1;
                        ItemLabel.TextColor = (Color)white;
                    }
                    else
                    {
                        Application.Current.Resources.TryGetValue("White1", out var whiteColor);
                        var white = (Color?)whiteColor;
                        
                        if (white == null) return;
                        
                        ItemMain.BackgroundColor = (Color)white;
                        ItemLabel.BackgroundColor = (Color)white;
                        ItemLabel.TextColor = Color;
                        ItemMain.Border = new Border { Thickness = 1, Color = Color };
                    }
                }
            }
        }

    }
}