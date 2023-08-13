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
        
        public static readonly BindableProperty ButtonTextProperty = BindableProperty.Create(
            nameof(ButtonText),
            typeof(string),
            typeof(CustomPageBarPrimary),
            string.Empty,
            BindingMode.TwoWay);

        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }
        
        public static readonly BindableProperty ButtonIsVisibleProperty = BindableProperty.Create(
            nameof(ButtonIsVisible),
            typeof(bool),
            typeof(CustomPageBarPrimary),
            false,
            BindingMode.TwoWay);
        
        public bool ButtonIsVisible
        {
            get => (bool)GetValue(ButtonIsVisibleProperty);
            set => SetValue(ButtonIsVisibleProperty, value);
        }
        
        public static readonly BindableProperty ButtonCommandProperty = BindableProperty.Create(
            nameof(ButtonCommand),
            typeof(ICommand),
            typeof(CustomPageBarPrimary),
            default(ICommand),
            BindingMode.TwoWay);
        
        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }
        
        public CustomPageBarPrimary()
        {
            InitializeComponent();

            ItemTitle.Text = TitleText;
            ItemButton.Text = ButtonText;
            ItemButton.IsVisible = ButtonIsVisible;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            
            if (propertyName == TitleTextProperty.PropertyName)
                ItemTitle.Text = TitleText;
            else if (propertyName == ButtonTextProperty.PropertyName)
                ItemButton.Text = ButtonText;
            else if (propertyName == ButtonIsVisibleProperty.PropertyName)
                ItemButton.IsVisible = ButtonIsVisible;
            else if (propertyName == ButtonCommandProperty.PropertyName)
                ItemButton.Command = ButtonCommand;
        }
    }
}