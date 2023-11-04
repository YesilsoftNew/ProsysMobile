using System;
using System.Windows.Input;
using ProsysMobile.Helper;
using Xamarin.Forms;

namespace ProsysMobile.CustomControls.Entry
{
	public partial class CustomSmartSearchEntry
    {

        #region BindablePropertys

        /// <summary>
        /// Keyboard Property
        /// </summary>
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(CustomSmartSearchEntry), Keyboard.Default,
            coerceValue: (o, v) => (Keyboard)v ?? Keyboard.Default);

        /// <summary>
        /// Title Property
        /// Label text
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string),
            typeof(CustomSmartSearchEntry),
            default(string),
            Xamarin.Forms.BindingMode.TwoWay);


        /// <summary>
        /// PlaceholderProperty Property
        /// Entry PlaceholderProperty
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(CustomSmartSearchEntry),
            "Arama Yapın",
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// ImageProperty
        /// Entry PlaceholderProperty
        /// </summary>
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource),
            typeof(string),
            typeof(CustomSmartSearchEntry),
            "SearchBlack",
            Xamarin.Forms.BindingMode.OneWay);


        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(
            nameof(SearchCommand),
            typeof(ICommand),
            typeof(CustomSmartSearchEntry),
            null);

        #endregion

        #region Commands

        public ICommand SearchCommand { get => (ICommand)GetValue(SearchCommandProperty); set => SetValue(SearchCommandProperty, value); }

        public ICommand UserStoppedTypingCommand
        {
            get
            {
                return new Command<string>((searchString) =>
                {
                    SearchCommand?.Execute(searchString);
                });
            }
        }

        #endregion

        #region Propertys

        /// <summary>
        /// Title
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);

            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Placeholder
        /// </summary>
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);

            set => SetValue(PlaceholderProperty, value);
        }

        /// <summary>
        /// Placeholder
        /// </summary>
        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);

            set => SetValue(ImageSourceProperty, value);
        }

        /// <summary>
        /// Keyboard
        /// </summary>
        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        #endregion

        public CustomSmartSearchEntry()
        {
            InitializeComponent();
            BindingContext = this;

            ItemSearch.Text = Text;
            ItemSearch.Placeholder = Placeholder;
            ItemImage.Source = ImageSource;
            ItemSearch.Keyboard = Keyboard;
        }

        #region Events

        /// <summary>
        /// Entry Text Change
        /// </summary>
        public event EventHandler<TextChangedEventArgs> EntryTextChanged;

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
            EntryTextChanged?.Invoke(sender, e);
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
                ProsysLogger.Instance.CrashLog(ex);

                DoubleTapping.ResumeTap();
                throw;
            }
        }

        #endregion
    }
}