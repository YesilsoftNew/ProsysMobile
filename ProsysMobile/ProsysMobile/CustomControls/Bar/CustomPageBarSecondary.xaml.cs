using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Bar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomPageBarSecondary
    {
        public static readonly BindableProperty IsPreviousButtonVisibleProperty = BindableProperty.Create(nameof(IsPreviousButtonVisible),
            typeof(bool),
            typeof(CustomPageBarSecondary),
            true,
            BindingMode.TwoWay);

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
            typeof(string),
            typeof(CustomPageBarSecondary),
            default(string),
            BindingMode.TwoWay);

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource),
            typeof(string),
            typeof(CustomPageBarSecondary),
            "ChevronLeftBlack",
            BindingMode.TwoWay);
        
        public static readonly BindableProperty PreviousClickCommandProperty = BindableProperty.Create(nameof(PreviousClickCommand),
            typeof(ICommand),
            typeof(CustomPageBarSecondary),
            default(ICommand),
            BindingMode.TwoWay);

        public bool IsPreviousButtonVisible
        {
            get => (bool)GetValue(IsPreviousButtonVisibleProperty);
            set => SetValue(IsPreviousButtonVisibleProperty, value);
        }
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
        
        public ICommand PreviousClickCommand
        {
            get => (Command)GetValue(PreviousClickCommandProperty);
            set => SetValue(PreviousClickCommandProperty, value);
        }
        
        public CustomPageBarSecondary()
        {
            InitializeComponent();
            ItemTitle.Text = Title;
            ItemButtonPrevious.Source = ImageSource;
        }
        
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
                ItemTitle.Text = Title;
            else if (propertyName == ImageSourceProperty.PropertyName)
                ItemButtonPrevious.Source = ImageSource;
            else if (propertyName == IsPreviousButtonVisibleProperty.PropertyName)
                ItemButtonPrevious.IsVisible = IsPreviousButtonVisible;
        }

        private void ItemButtonPrevious_Clicked(object sender, EventArgs e)
        {
            PreviousClickCommand?.Execute(null);
        }
    }
}