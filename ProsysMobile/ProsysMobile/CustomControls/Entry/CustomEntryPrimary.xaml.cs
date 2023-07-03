using System;
using System.Linq;
using ProsysMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Entry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomEntryPrimary : PancakeView
    {

        /// <summary>
        /// ReturnCommandParameter
        /// Entry ReturnCommandParameter
        /// </summary>
        public static readonly BindableProperty ReturnCommandParameterProperty = BindableProperty.Create(nameof(ReturnCommandParameter), typeof(object), typeof(CustomEntryPrimary), default(object), Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Title Property
        /// Label text
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomEntryPrimary), default(string), Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }

            set
            {
                SetValue(TitleProperty, value);
            }
        }

        /// <summary>
        /// Text Property
        /// Entry text
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomEntryPrimary), default(string), BindingMode.TwoWay);

        /// <summary>
        /// Text
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
        /// Keyboard Property
        /// </summary>
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(CustomEntryPrimary), Keyboard.Default,
             coerceValue: (o, v) => (Keyboard)v ?? Keyboard.Default);

        /// <summary>
        /// Keyboard
        /// </summary>
        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        /// <summary>
        /// PlaceholderProperty Property
        /// Entry PlaceholderProperty
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomEntryPrimary), default(string), Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Text
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
        /// HorizontalTextAlignment Property
        /// </summary>
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(CustomEntryPrimary), TextAlignment.Start, Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// HorizontalTextAlignment
        /// </summary>
        public TextAlignment HorizontalTextAlignment
        {
            get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
            set { SetValue(HorizontalTextAlignmentProperty, value); }
        }

        /// <summary>
        /// VerticalTextAlignment Property
        /// </summary>
        public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(CustomEntryPrimary), TextAlignment.Center, Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// VerticalTextAlignment
        /// </summary>
        public TextAlignment VerticalTextAlignment
        {
            get { return (TextAlignment)GetValue(VerticalTextAlignmentProperty); }
            set { SetValue(VerticalTextAlignmentProperty, value); }
        }

        /// <summary>
        /// IsDisabled Property
        /// </summary>
        public static readonly BindableProperty IsDisabledProperty = BindableProperty.Create(nameof(IsDisabled), typeof(bool), typeof(CustomEntryPrimary), false, Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// IsDisabled
        /// </summary>
        public bool IsDisabled
        {
            get
            {
                return (bool)GetValue(IsDisabledProperty);
            }

            set
            {
                SetValue(IsDisabledProperty, value);
            }
        }

        /// <summary>
        /// IsUpperCase Property
        /// </summary>
        public static readonly BindableProperty IsUpperCaseProperty = BindableProperty.Create(nameof(IsUpperCase), typeof(bool), typeof(CustomEntryPrimary), false, Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// IsUpperCase
        /// </summary>
        public bool IsUpperCase
        {
            get
            {
                return (bool)GetValue(IsUpperCaseProperty);
            }

            set
            {
                SetValue(IsUpperCaseProperty, value);
            }
        }

        /// <summary>
        /// IsNoSpace Property
        /// </summary>
        public static readonly BindableProperty IsNoSpaceProperty = BindableProperty.Create(nameof(IsNoSpace), typeof(bool), typeof(CustomEntryPrimary), false, Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// IsNoSpace
        /// </summary>
        public bool IsNoSpace
        {
            get
            {
                return (bool)GetValue(IsNoSpaceProperty);
            }

            set
            {
                SetValue(IsNoSpaceProperty, value);
            }
        }

        /// <summary>
        /// IsPassword Property
        /// </summary>
        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(CustomEntryPrimary), false, Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// IsPassword
        /// </summary>
        public bool IsPassword
        {
            get
            {
                return (bool)GetValue(IsPasswordProperty);
            }

            set
            {
                SetValue(IsPasswordProperty, value);
            }
        }

        /// <summary>
        /// DisabledImageSource Property
        /// </summary>
        public static readonly BindableProperty DisabledImageSourceProperty = BindableProperty.Create(nameof(DisabledImageSource), typeof(string), typeof(CustomEntryPrimary), "LockGray", Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// DisabledImageSource
        /// </summary>
        public string DisabledImageSource
        {
            get
            {
                return (string)GetValue(DisabledImageSourceProperty);
            }

            set
            {
                SetValue(DisabledImageSourceProperty, value);
            }
        }

        /// <summary>
        /// DisabledImageSource
        /// </summary>
        public object ReturnCommandParameter
        {
            get
            {
                return (object)GetValue(ReturnCommandParameterProperty);
            }

            set
            {
                SetValue(ReturnCommandParameterProperty, value);
            }
        }

        /// <summary>
        /// Entry Focused
        /// </summary>
        public event EventHandler<FocusEventArgs> EntryFocused;

        /// <summary>
        /// Entry Text Change
        /// </summary>
        public event EventHandler<TextChangedEventArgs> EntryTextChanged;

        /// <summary>
        /// Image Button Pressed
        /// </summary>
        public event EventHandler<EventArgs> ImageButtonPressed;

        /// <summary>
        /// Image Button Released
        /// </summary>
        public event EventHandler<EventArgs> ImageButtonReleased;

        public CustomEntryPrimary()
        {
            InitializeComponent();

            try
            {
                ItemLabel.Text = Title;
                ItemEntry.Keyboard = Keyboard;
                ItemEntry.Text = Text;
                ItemEntry.Placeholder = Placeholder;
                ItemEntry.VerticalTextAlignment = VerticalTextAlignment;
                ItemEntry.HorizontalTextAlignment = HorizontalTextAlignment;
                ItemImage.Source = DisabledImageSource;
                ItemEntry.ReturnCommandParameter = ReturnCommandParameter;
                ItemEntry.IsPassword = IsPassword;

                if (IsUpperCase)
                    ItemEntry.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);

                CheckIsDisabled();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        /// <summary>
        /// On Focused Entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnEntryFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (EntryFocused != null)
                {
                    EntryFocused(sender, e);
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        /// <summary>
        /// On Text Change Entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //Renderer.CustomEntry entry = (Renderer.CustomEntry)sender;

                if (Text != null && Text.Equals(e.NewTextValue))
                    return;

                Text = e.NewTextValue;

                if (IsUpperCase)
                {
                    //entry.Text = Text.ToUpper().Replace("İ", "I");
                    Text = Text.ToUpper().Replace("İ", "I");
                }

                if (IsNoSpace)
                {
                    //entry.Text = Text.Replace(" ", "");
                    Text = Text.Replace(" ", "");
                }

                if (EntryTextChanged != null)
                {
                    EntryTextChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        /// <summary>
        /// OnPropertyChanged
        /// Work Property Change
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            try
            {
                base.OnPropertyChanged(propertyName);

                if (propertyName == TitleProperty.PropertyName)
                {
                    ItemLabel.Text = Title;
                }
                else if (propertyName == TextProperty.PropertyName)
                {
                    ItemEntry.Text = Text;
                }
                else if (propertyName == KeyboardProperty.PropertyName)
                {
                    ItemEntry.Keyboard = Keyboard;
                }
                else if (propertyName == PlaceholderProperty.PropertyName)
                {
                    ItemEntry.Placeholder = Placeholder;
                }
                else if (propertyName == HorizontalTextAlignmentProperty.PropertyName)
                {
                    ItemEntry.HorizontalTextAlignment = HorizontalTextAlignment;
                }
                else if (propertyName == VerticalTextAlignmentProperty.PropertyName)
                {
                    ItemEntry.VerticalTextAlignment = VerticalTextAlignment;
                }
                else if (propertyName == IsDisabledProperty.PropertyName)
                {
                    CheckIsDisabled();
                }
                else if (propertyName == DisabledImageSourceProperty.PropertyName)
                {
                    ItemImage.Source = DisabledImageSource;
                }
                else if (propertyName == ReturnCommandParameterProperty.PropertyName)
                {
                    ItemEntry.ReturnCommandParameter = ReturnCommandParameter;
                }
                else if (propertyName == IsPasswordProperty.PropertyName)
                {
                    ItemEntry.IsPassword = IsPassword;
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        /// <summary>
        /// Entry'ler için üstünde bulunan alana tıklanılınca focuslama yapar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnTappedEntryArea(object sender, EventArgs args)
        {
            try
            {
                if (IsDisabled) return;

                var target = sender as PancakeView;
                var grid = target.Children.FirstOrDefault(x => x is Grid) as Grid;
                var entry = grid.Children.FirstOrDefault(x => x is Renderer.CustomEntry) as Renderer.CustomEntry;
                entry.Focus();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        /// <summary>
        /// Is Disabled Check Function
        /// </summary>
        void CheckIsDisabled()
        {
            try
            {
                Application.Current.Resources.TryGetValue("Sky.Lighter", out var disabledBgColor);

                BorderMain.BackgroundColor = (IsDisabled ? (Color)disabledBgColor : Color.Transparent);
                ItemImage.IsVisible = IsDisabled;
                ItemEntry.IsReadOnly = IsDisabled;
                //ItemImage.Margin = IsDisabled ? new Thickness(0, 0, 12, 0) : new Thickness(0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        private void ItemImage_Pressed(object sender, EventArgs e)
        {
            try
            {
                if (IsPassword == true)
                {
                    ItemEntry.IsPassword = false;
                    ItemImage.Source = "EyeGray";
                }

                if (ImageButtonPressed != null)
                {
                    ImageButtonPressed(sender, e);
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        private void ItemImage_Released(object sender, EventArgs e)
        {
            try
            {
                ItemEntry.IsPassword = true;
                ItemImage.Source = "EyeGrayOff";

                if (ImageButtonReleased != null)
                {
                    ImageButtonReleased(sender, e);
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }
    }
}