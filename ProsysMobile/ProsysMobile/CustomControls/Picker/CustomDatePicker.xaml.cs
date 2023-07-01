using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace WiseMobile.CustomControls.Picker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomDatePicker : PancakeView
    {
        /// <summary>
        /// The label title property.
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(CustomDatePicker),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// The image source property.
        /// </summary>
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource),
            typeof(string),
            typeof(CustomDatePicker),
            "DateGray",
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// The date picker "Date" property.
        /// </summary>
        public static readonly BindableProperty DateProperty = BindableProperty.Create(
            nameof(Date),
            typeof(DateTime),
            typeof(CustomDatePicker),
            DateTime.Now,
            Xamarin.Forms.BindingMode.TwoWay);

        /// <summary>
        /// The date picker "MinDate" property.
        /// </summary>
        public static readonly BindableProperty MinDateProperty = BindableProperty.Create(
            nameof(MinDate),
            typeof(DateTime),
            typeof(CustomDatePicker),
            Convert.ToDateTime("1900-01-01"),
            Xamarin.Forms.BindingMode.TwoWay);

        /// <summary>
        /// The date picker "MaxDate" property.
        /// </summary>
        public static readonly BindableProperty MaxDateProperty = BindableProperty.Create(
            nameof(MaxDate),
            typeof(DateTime),
            typeof(CustomDatePicker),
            Convert.ToDateTime("2079-06-06"),
            Xamarin.Forms.BindingMode.TwoWay);

        /// <summary>
        /// IsDisabled Property
        /// </summary>
        public static readonly BindableProperty IsDisabledProperty = BindableProperty.Create(
            nameof(IsDisabled),
            typeof(bool),
            typeof(CustomDatePicker),
            false,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Date Selected
        /// </summary>
        public event EventHandler<DateChangedEventArgs> DatePickerDateSelected;


        /// <summary>
        /// The cell's Title
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
        /// The cell's ImageSource
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
        /// The cell's Date
        /// </summary>
        public DateTime Date
        {
            get
            {
                return (DateTime)GetValue(DateProperty);
            }

            set
            {
                SetValue(DateProperty, value);
            }
        }

        /// <summary>
        /// The cell's MinDate
        /// </summary>
        public DateTime MinDate
        {
            get
            {
                return (DateTime)GetValue(MinDateProperty);
            }

            set
            {
                SetValue(MinDateProperty, value);
            }
        }

        /// <summary>
        /// The cell's MaxDate
        /// </summary>
        public DateTime MaxDate
        {
            get
            {
                return (DateTime)GetValue(MaxDateProperty);
            }

            set
            {
                SetValue(MaxDateProperty, value);
            }
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
            }
        }

        public CustomDatePicker()
        {
            InitializeComponent();

            ItemLabel.Text = Title;
            ItemImage.Source = ImageSource;
            ItemDatePicker.Date = Date;
            ItemDatePicker.MinimumDate = MinDate;
            ItemDatePicker.MaximumDate = MaxDate;

            ItemDatePicker.MaximumDate = DateTime.Now;

            CheckIsDisabled();

            ItemDatePicker.DateSelected += OnDatePickerDateSelected;

        }

        // private void DatePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)

        /// <summary>
        /// On Date Picker Date Selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnDatePickerDateSelected(object sender, DateChangedEventArgs e)
        {
            Date = e.NewDate
                ;
            if (DatePickerDateSelected != null)
            {
                DatePickerDateSelected(sender, e);
            }
        }

        void OnTappedDatePickerArea(object sender, EventArgs args)
        {
            if (IsDisabled) return;

            var target = sender as PancakeView;
            var grid = target.Children.FirstOrDefault(x => x is Grid) as Grid;
            var entry = grid.Children.FirstOrDefault(x => x is Renderer.CustomDatePicker) as Renderer.CustomDatePicker;
            entry.Focus();
        }

        /// <summary>
        /// OnPropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
            {
                ItemLabel.Text = Title;
            }
            else if (propertyName == ImageSourceProperty.PropertyName)
            {
                ItemImage.Source = ImageSource;

            }else if (propertyName == DateProperty.PropertyName)
            {
                ItemDatePicker.Date = Date;
            }
            else if (propertyName == MinDateProperty.PropertyName)
            {
                ItemDatePicker.MinimumDate = MinDate;
            }
            else if (propertyName == MaxDateProperty.PropertyName)
            {
                ItemDatePicker.MaximumDate = MaxDate;
            }
            else if (propertyName == IsDisabledProperty.PropertyName)
            {
                CheckIsDisabled();
            }
        }

        /// <summary>
        /// Is Disabled Check Function
        /// </summary>
        void CheckIsDisabled()
        {
            Application.Current.Resources.TryGetValue("Sky.Lighter", out var disabledBgColor);

            BorderMain.BackgroundColor = (IsDisabled ? (Color)disabledBgColor : Color.Transparent);
            ItemDatePicker.IsEnabled = !IsDisabled;
        }
    }
}