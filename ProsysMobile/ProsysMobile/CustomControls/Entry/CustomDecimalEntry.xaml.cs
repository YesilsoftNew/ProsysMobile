using System;
using WiseDynamicMobile.Helper;
using WiseMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WiseMobile.CustomControls.Entry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomDecimalEntry : AbsoluteLayout
    {
        /// <summary>
        /// EntryFocus
        /// </summary>
        public static readonly BindableProperty EntryFocusProperty = BindableProperty.Create(
            nameof(EntryFocus),
            typeof(bool),
            typeof(CustomDecimalEntry),
            false,
            Xamarin.Forms.BindingMode.TwoWay);

        public bool EntryFocus
        {
            get
            {
                return (bool)GetValue(EntryFocusProperty);
            }
            set
            {
                SetValue(EntryFocusProperty, value);
            }
        }

        /// <summary>
        /// EntryFocus
        /// </summary>
        public static readonly BindableProperty EntryUnfocusProperty = BindableProperty.Create(
            nameof(EntryUnfocus),
            typeof(bool),
            typeof(CustomDecimalEntry),
            false,
            Xamarin.Forms.BindingMode.TwoWay);

        public bool EntryUnfocus
        {
            get
            {
                return (bool)GetValue(EntryUnfocusProperty);
            }
            set
            {
                SetValue(EntryUnfocusProperty, value);
            }
        }


        /// <summary>
        /// EntryTextColor
        /// </summary>
        public static readonly BindableProperty EntryTextColorProperty = BindableProperty.Create(
            nameof(EntryTextColor),
            typeof(Color),
            typeof(CustomDecimalEntry),
            Color.Black,
            Xamarin.Forms.BindingMode.OneWay);

        public Color EntryTextColor
        {
            get
            {
                return (Color)GetValue(EntryTextColorProperty);
            }
            set
            {
                SetValue(EntryTextColorProperty, value);
            }
        }

        /// <summary>
        /// EntryBorderColor
        /// </summary>
        public static readonly BindableProperty EntryBorderColorProperty = BindableProperty.Create(
            nameof(EntryBorderColor),
            typeof(Color),
            typeof(CustomDecimalEntry),
            Color.Transparent,
            Xamarin.Forms.BindingMode.OneWay);

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
        /// EntryPlaceHolderColor
        /// </summary>
        public static readonly BindableProperty EntryPlaceHolderColorProperty = BindableProperty.Create(
            nameof(EntryPlaceHolderColor),
            typeof(Color),
            typeof(CustomDecimalEntry),
            Color.Gray,
            Xamarin.Forms.BindingMode.OneWay);

        public Color EntryPlaceHolderColor
        {
            get
            {
                return (Color)GetValue(EntryPlaceHolderColorProperty);
            }

            set
            {
                SetValue(EntryPlaceHolderColorProperty, value);
            }
        }

        /// <summary>
        /// ReturnCommandParameter
        /// Entry ReturnCommandParameter
        /// </summary>
        public static readonly BindableProperty ReturnCommandParameterProperty = BindableProperty.Create(nameof(ReturnCommandParameter), typeof(object), typeof(CustomDecimalEntry), default(object), Xamarin.Forms.BindingMode.OneWay);

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
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomDecimalEntry), default(string), BindingMode.TwoWay);

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

        ///// <summary>
        ///// Text Property
        ///// Entry text
        ///// </summary>
        //public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomDecimalEntry), default(double), BindingMode.TwoWay);

        ///// <summary>
        ///// Text
        ///// </summary>
        //public double FontSize
        //{
        //    get
        //    {
        //        return (double)GetValue(FontSizeProperty);
        //    }

        //    set
        //    {
        //        SetValue(FontSizeProperty, value);
        //    }
        //}

        /// <summary>
        /// PlaceholderProperty Property
        /// Entry PlaceholderProperty
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomDecimalEntry), default(string), Xamarin.Forms.BindingMode.OneWay);

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
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(CustomDecimalEntry), TextAlignment.Center, Xamarin.Forms.BindingMode.OneWay);

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
        public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(CustomDecimalEntry), TextAlignment.Center, Xamarin.Forms.BindingMode.OneWay);

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
        public static readonly BindableProperty IsDisabledProperty = BindableProperty.Create(nameof(IsDisabled), typeof(bool), typeof(CustomDecimalEntry), false, Xamarin.Forms.BindingMode.OneWay);

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
        /// Entry Focused
        /// </summary>
        public event EventHandler<FocusEventArgs> EntryFocused;

        /// <summary>
        /// Entry Text Change
        /// </summary>
        public event EventHandler<TextChangedEventArgs> EntryTextChanged;

        public CustomDecimalEntry()
        {
            InitializeComponent();

            ItemEntry.Text = Text;
            ItemEntry.Placeholder = Placeholder;
            ItemEntry.VerticalTextAlignment = VerticalTextAlignment;
            ItemEntry.HorizontalTextAlignment = HorizontalTextAlignment;
            ItemEntry.ReturnCommandParameter = ReturnCommandParameter;
            ItemEntry.TextColor = EntryTextColor;
            ItemEntry.PlaceholderColor = EntryPlaceHolderColor;
            //ItemEntry.FontSize = FontSize;

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
                EntryFocused(sender, e);
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
                if (!string.IsNullOrWhiteSpace(e.NewTextValue))
                    Text = TOOLS.ToString(string.IsNullOrWhiteSpace(e.NewTextValue) ? 0 : Convert.ToDouble(e.NewTextValue.Replace(".", System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString())));
                else
                    Text = "";
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }

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
                ItemEntry.Text = Text;
            else if (propertyName == PlaceholderProperty.PropertyName)
                ItemEntry.Placeholder = Placeholder;
            else if (propertyName == HorizontalTextAlignmentProperty.PropertyName)
                ItemEntry.HorizontalTextAlignment = HorizontalTextAlignment;
            else if (propertyName == VerticalTextAlignmentProperty.PropertyName)
                ItemEntry.VerticalTextAlignment = VerticalTextAlignment;
            else if (propertyName == IsDisabledProperty.PropertyName)
                CheckIsDisabled();
            else if (propertyName == ReturnCommandParameterProperty.PropertyName)
                ItemEntry.ReturnCommandParameter = ReturnCommandParameter;
            else if (propertyName == EntryTextColorProperty.PropertyName)
                ItemEntry.TextColor = EntryTextColor;
            else if (propertyName == EntryPlaceHolderColorProperty.PropertyName)
                ItemEntry.TextColor = EntryPlaceHolderColor;
            else if (propertyName == EntryBorderColorProperty.PropertyName)
                BorderMain.BackgroundColor = EntryBorderColor;
            else if (propertyName == EntryFocusProperty.PropertyName)
            {
                if (EntryFocus)
                {
                    EntryFocus = false;                    
                    ItemEntry.Focus();

                    if (EntryFocused != null)
                        EntryFocused(null, null);
                }
            }
            else if (propertyName == EntryUnfocusProperty.PropertyName)
            {
                if (EntryUnfocus)
                {
                    EntryUnfocus = false;
                    ItemEntry.Unfocus();
                }
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
    }
}