using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Bar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomPageBarPrimary
    {
        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
            nameof(TitleText),
            typeof(string),
            typeof(CustomPageBarPrimary),
            string.Empty,
            BindingMode.TwoWay);

        public string TitleText
        {
            get => (string)GetValue(TitleTextProperty);
            set => SetValue(TitleTextProperty, value);
        }
        
        public static readonly BindableProperty SecondaryTextProperty = BindableProperty.Create(
            nameof(SecondaryText),
            typeof(string),
            typeof(CustomPageBarPrimary),
            string.Empty,
            BindingMode.TwoWay);

        public string SecondaryText
        {
            get => (string)GetValue(SecondaryTextProperty);
            set => SetValue(SecondaryTextProperty, value);
        }
        
        public CustomPageBarPrimary()
        {
            InitializeComponent();

            ItemTitle.Text = TitleText;
            ItemTitle2.Text = SecondaryText;
            ItemTitle2.IsVisible = !string.IsNullOrWhiteSpace(SecondaryText);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            
            if (propertyName == TitleTextProperty.PropertyName)
                ItemTitle.Text = TitleText;
            else if (propertyName == SecondaryTextProperty.PropertyName)
            {
                ItemTitle2.Text = SecondaryText;
                ItemTitle2.IsVisible = !string.IsNullOrWhiteSpace(SecondaryText);
            }
        }
    }
}