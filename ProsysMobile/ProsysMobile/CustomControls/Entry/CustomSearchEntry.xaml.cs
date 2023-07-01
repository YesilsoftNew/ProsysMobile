using System;
using ProsysMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Entry
{
    // BindingMode
    // https://learn.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/data-binding/binding-mode
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomSearchEntry : PancakeView 
    {
        /// <summary>
        /// Keyboard Property
        /// </summary>
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(CustomSearchEntry), Keyboard.Default,
             coerceValue: (o, v) => (Keyboard)v ?? Keyboard.Default);

        /// <summary>
        /// Title Property
        /// Label text
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string),
            typeof(CustomSearchEntry),
            default(string),
            Xamarin.Forms.BindingMode.TwoWay);


        /// <summary>
        /// PlaceholderProperty Property
        /// Entry PlaceholderProperty
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string), 
            typeof(CustomSearchEntry),
            "Arama Yapın",
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// ImageProperty
        /// Entry PlaceholderProperty
        /// </summary>
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource),
            typeof(string),
            typeof(CustomSearchEntry),
            "SearchBlack",
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Title
        /// </summary>
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }

        /// <summary>
        /// Placeholder
        /// </summary>
        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }

            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }

        /// <summary>
        /// Placeholder
        /// </summary>
        public string ImageSource
        {
            get
            {
                return (string)GetValue(ImageSourceProperty);
            }

            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }

        /// <summary>
        /// Keyboard
        /// </summary>
        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        /// <summary>
        /// Entry Text Change
        /// </summary>
        public event EventHandler<TextChangedEventArgs> EntryTextChanged;

        public CustomSearchEntry()
        {
            InitializeComponent();

            ItemSearch.Text = Text;
            ItemSearch.Placeholder = Placeholder;
            ItemImage.Source = ImageSource;
            ItemSearch.Keyboard = Keyboard;
        }

        /// <summary>
        /// On Property Changed
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                ItemSearch.Text = Text;
            }
            else if (propertyName == PlaceholderProperty.PropertyName)
            {
                ItemSearch.Placeholder = Placeholder;
            }
            else if (propertyName == ImageSourceProperty.PropertyName)
            {
                ItemImage.Source = ImageSource;
            }
            else if (propertyName == KeyboardProperty.PropertyName)
            {
                ItemSearch.Keyboard = Keyboard;
            }
        }

        /// <summary>
        /// Search Box Text Change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
            ItemBtnCancelSearch.IsVisible = string.IsNullOrWhiteSpace(e.NewTextValue) ? false : true;

            if (EntryTextChanged != null)
            {
                EntryTextChanged(sender, e);
            }
        }

        /// <summary>
        /// Search Image Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemImage_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                ItemSearch.Focus();

                DoubleTapping.ResumeTap();
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);

                DoubleTapping.ResumeTap();
                throw;
            }
        }
        
        /// <summary>
        /// Cancel Button Clicked
        /// </summary>
        private void ItemBtnCancelSearch_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                ItemSearch.Text = String.Empty;

                DoubleTapping.ResumeTap();
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);

                DoubleTapping.ResumeTap();
                return;
            }
        }
    }
}