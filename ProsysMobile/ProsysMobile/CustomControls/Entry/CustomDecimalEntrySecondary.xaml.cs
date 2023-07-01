using System;
using System.Linq;
using WiseMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace WiseMobile.CustomControls.Entry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomDecimalEntrySecondary : AbsoluteLayout
    {

        /// <summary>
        /// ReturnCommandParameterProperty
        /// Entry ReturnCommandParameter
        /// </summary>
        public static readonly BindableProperty ReturnCommandParameterProperty = BindableProperty.Create(nameof(ReturnCommandParameter), typeof(object), typeof(CustomDecimalEntrySecondary), default(object), Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// TitleProperty
        /// Label text
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomDecimalEntrySecondary), default(string), Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// TextProperty
        /// Entry text
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomDecimalEntrySecondary), default(string), BindingMode.TwoWay);

        /// <summary>
        /// KeyboardProperty
        /// </summary>
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(CustomDecimalEntrySecondary), Keyboard.Default,
             coerceValue: (o, v) => (Keyboard)v ?? Keyboard.Default);

        /// <summary>
        /// PlaceholderPropertyProperty
        /// Entry PlaceholderProperty
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomDecimalEntrySecondary), default(string), Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// HorizontalTextAlignmentProperty
        /// </summary>
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(CustomDecimalEntrySecondary), TextAlignment.Start, Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// VerticalTextAlignmentProperty
        /// </summary>
        public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(CustomDecimalEntrySecondary), TextAlignment.Center, Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// IsDisabledProperty
        /// </summary>
        public static readonly BindableProperty IsDisabledProperty = BindableProperty.Create(nameof(IsDisabled), typeof(bool), typeof(CustomDecimalEntrySecondary), false, Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// IsPasswordPropertyProperty
        /// </summary>
        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(CustomDecimalEntrySecondary), false, Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// DisabledImageSourceProperty
        /// </summary>
        public static readonly BindableProperty DisabledImageSourceProperty = BindableProperty.Create(nameof(DisabledImageSource), typeof(string), typeof(CustomDecimalEntrySecondary), "LockGray", Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// EntryBorderColorProperty
        /// </summary>
        public static readonly BindableProperty EntryBorderColorProperty = BindableProperty.Create(nameof(EntryBorderColor), typeof(Color), typeof(CustomDecimalEntry), Color.Transparent, Xamarin.Forms.BindingMode.OneWay);

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
         
        /// Text
        /// </summary>
        public string Text
        {
        /// <summary>
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
        /// Keyboard
        /// </summary>
        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
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
        /// HorizontalTextAlignment
        /// </summary>
        public TextAlignment HorizontalTextAlignment
        {
            get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
            set { SetValue(HorizontalTextAlignmentProperty, value); }
        }

        /// <summary>
        /// VerticalTextAlignment
        /// </summary>
        public TextAlignment VerticalTextAlignment
        {
            get { return (TextAlignment)GetValue(VerticalTextAlignmentProperty); }
            set { SetValue(VerticalTextAlignmentProperty, value); }
        }

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
                CheckIsDisabled();
            }
        }

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
        /// ReturnCommandParameter
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
        /// EntryBorderColor
        /// </summary>
        public Color EntryBorderColor
        {
            get
            {
                return (Color)GetValue(EntryBorderColorProperty);
            }
            set
            {
                SetValue(EntryBorderColorProperty, value);

                BorderMain.BackgroundColor = EntryBorderColor;
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

        public CustomDecimalEntrySecondary()
        {
            InitializeComponent();

            ItemLabel.Text = Title;
            ItemEntry.Keyboard = Keyboard;
            ItemEntry.Text = Text;
            ItemEntry.Placeholder = Placeholder;
            ItemEntry.VerticalTextAlignment = VerticalTextAlignment;
            ItemEntry.HorizontalTextAlignment = HorizontalTextAlignment;
            ItemImage.Source = DisabledImageSource;
            ItemEntry.ReturnCommandParameter = ReturnCommandParameter;
            ItemEntry.IsPassword = IsPassword;

            stcBorder.GestureRecognizers.Add(new TapGestureRecognizer(stcBorder_Clicked));

            CheckIsDisabled();
        }

        /// <summary>
        /// On Focused Entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnEntryFocused(object sender, FocusEventArgs e)
        {
            if (EntryFocused != null)
            {
                EntryFocused(sender, e);
            }
        }

        /// <summary>
        /// On Text Change Entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
            if (EntryTextChanged != null)
            {
                EntryTextChanged(sender, e);
            }
        }

        /// <summary>
        /// OnPropertyChanged
        /// Work Property Change
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
                ItemLabel.Text = Title;
            else if (propertyName == TextProperty.PropertyName)
                ItemEntry.Text = Text;
            else if (propertyName == KeyboardProperty.PropertyName)
                ItemEntry.Keyboard = Keyboard;
            else if (propertyName == PlaceholderProperty.PropertyName)
                ItemEntry.Placeholder = Placeholder;
            else if (propertyName == HorizontalTextAlignmentProperty.PropertyName)
                ItemEntry.HorizontalTextAlignment = HorizontalTextAlignment;
            else if (propertyName == VerticalTextAlignmentProperty.PropertyName)
                ItemEntry.VerticalTextAlignment = VerticalTextAlignment;
            else if (propertyName == IsDisabledProperty.PropertyName)
                CheckIsDisabled();
            else if (propertyName == DisabledImageSourceProperty.PropertyName)
                ItemImage.Source = DisabledImageSource;
            else if (propertyName == ReturnCommandParameterProperty.PropertyName)
                ItemEntry.ReturnCommandParameter = ReturnCommandParameter;
            else if (propertyName == IsPasswordProperty.PropertyName)
                ItemEntry.IsPassword = IsPassword;
            else if (propertyName == EntryBorderColorProperty.PropertyName)
                BorderMain.BackgroundColor = EntryBorderColor;
        }

        /// <summary>
        /// Entry'ler için üstünde bulunan alana tıklanılınca focuslama yapar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnTappedEntryArea(object sender, EventArgs args)
        {
            if (IsDisabled) return;

            var target = sender as PancakeView;
            var grid = target.Children.FirstOrDefault(x => x is Grid) as Grid;
            var entry = grid.Children.FirstOrDefault(x => x is Renderer.CustomDecimalEntry) as Renderer.CustomDecimalEntry;
            entry.Focus();
        }

        /// <summary>
        /// Is Disabled Check Function
        /// </summary>
        void CheckIsDisabled()
        {
            Application.Current.Resources.TryGetValue("Sky.Lighter", out var disabledBgColor);

            BorderMain.BackgroundColor = (IsDisabled ? (Color)disabledBgColor : Color.Transparent);
            ItemImage.IsVisible = IsDisabled;
            ItemEntry.IsReadOnly = IsDisabled;

            stcBorder.IsVisible = IsDisabled;
        }

        void stcBorder_Clicked(View arg1, object arg2)
        {
            try
            {
                if (EntryFocused != null)
                    EntryFocused(null, null);
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private void ItemImage_Pressed(object sender, EventArgs e)
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

        private void ItemImage_Released(object sender, EventArgs e)
        {
            ItemEntry.IsPassword = true;
            ItemImage.Source = "EyeGrayOff";

            if (ImageButtonReleased != null)
            {
                ImageButtonReleased(sender, e);
            }
        }
    }
}