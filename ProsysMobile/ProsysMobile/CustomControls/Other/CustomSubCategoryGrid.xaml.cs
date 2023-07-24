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
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color),
            typeof(Color),
            typeof(CustomSubCategoryGrid),
            default(Color),
            Xamarin.Forms.BindingMode.TwoWay);
        
        public static readonly BindableProperty IsClickableProperty = BindableProperty.Create(nameof(IsClickable),
            typeof(bool),
            typeof(CustomSubCategoryGrid),
            default(bool),
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
        
        public bool IsClickable
        {
            get => (bool)GetValue(IsClickableProperty);
            set => SetValue(IsClickableProperty, value);
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
            else if (propertyName == IsClickableProperty.PropertyName)
            {
                if (IsClickable)
                {
                    // ItemMain.GestureRecognizers.Add(new TapGestureRecognizer()
                    // {
                    //     Command = new Command(x =>
                    //     {
                    //         try
                    //         {
                    //             Application.Current.Resources.TryGetValue("Black3", out var blackColor);
                    //             var clrBlackColor = (Color?)blackColor;
                    //
                    //             if (ItemLabel.TextColor == Color)
                    //             {
                    //                 if (clrBlackColor != null)
                    //                 {
                    //                     ItemMain.Border = new Border() { Thickness = 1, Color = (Color)clrBlackColor };
                    //                     ItemLabel.TextColor = (Color)clrBlackColor;
                    //                 }
                    //             }
                    //             else
                    //             {
                    //                 ItemLabel.TextColor = Color;
                    //                 ItemMain.Border = new Border() { Thickness = 1, Color = Color };
                    //             }
                    //         }
                    //         catch (Exception ex)
                    //         {
                    //             ProsysLogger.Instance.CrashLog(ex);
                    //         }
                    //     })
                    // });  
                }
            }
        }

    }
}