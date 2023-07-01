using System;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Entry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomEntrySecondary : PancakeView
    {
        /// <summary>
        /// Keyboard Property
        /// </summary>
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(CustomEntrySecondary), Keyboard.Default,
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
        /// ReturnCommandParameter
        /// Entry ReturnCommandParameter
        /// </summary>
        public static readonly BindableProperty ReturnCommandParameterProperty = BindableProperty.Create(nameof(ReturnCommandParameter), typeof(object), typeof(CustomEntrySecondary), default(object), Xamarin.Forms.BindingMode.OneWay);

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
        /// Text Property
        /// Entry text
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomEntrySecondary), default(string), BindingMode.TwoWay);

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
        /// PlaceholderProperty Property
        /// Entry PlaceholderProperty
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomEntrySecondary), default(string), Xamarin.Forms.BindingMode.OneWay);

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
        /// HorizontalTextAlignment Property
        /// </summary>
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(CustomEntrySecondary), TextAlignment.Start, Xamarin.Forms.BindingMode.OneWay);

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
        public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(CustomEntrySecondary), TextAlignment.Center, Xamarin.Forms.BindingMode.OneWay);

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
        public static readonly BindableProperty IsDisabledProperty = BindableProperty.Create(nameof(IsDisabled), typeof(bool), typeof(CustomEntrySecondary), false, Xamarin.Forms.BindingMode.OneWay);

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
        /// Entry Focused
        /// </summary>
        public event EventHandler<FocusEventArgs> EntryFocused;

        /// <summary>
        /// Entry Text Change
        /// </summary>
        public event EventHandler<TextChangedEventArgs> EntryTextChanged;

        public CustomEntrySecondary()
        {
            InitializeComponent();

            ItemEntry.Text = Text;
            ItemEntry.Placeholder = Placeholder;
            ItemEntry.VerticalTextAlignment = VerticalTextAlignment;
            ItemEntry.HorizontalTextAlignment = HorizontalTextAlignment;
            ItemEntry.ReturnCommandParameter = ReturnCommandParameter;
            ItemEntry.Keyboard = Keyboard;

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

            if (propertyName == TextProperty.PropertyName)
            {
                ItemEntry.Text = Text;
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
            else if (propertyName == ReturnCommandParameterProperty.PropertyName)
            {
                ItemEntry.ReturnCommandParameter = ReturnCommandParameter;
            }
            else if (propertyName == KeyboardProperty.PropertyName)
            {
                ItemEntry.Keyboard = Keyboard;
            }
        }

        /// <summary>
        /// Is Disabled Check Function
        /// </summary>
        void CheckIsDisabled()
        {
            Application.Current.Resources.TryGetValue("Sky.Lighter", out var disabledBgColor);

            BorderMain.BackgroundColor = (IsDisabled ? (Color)disabledBgColor : Color.Transparent);
            ItemEntry.IsReadOnly = IsDisabled;
        }
    }
}