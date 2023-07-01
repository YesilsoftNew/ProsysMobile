using System;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace WiseMobile.CustomControls.Bar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTabViewQuaternary : PancakeView
    {
        /// <summary>
        /// First Button Enable
        /// </summary>
        public static readonly BindableProperty FirstButtonEnableProperty = BindableProperty.Create(nameof(FirstButtonEnable),
            typeof(bool),
            typeof(CustomTabViewQuaternary),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Second Button Enable
        /// </summary>
        public static readonly BindableProperty SecondButtonEnableProperty = BindableProperty.Create(nameof(SecondButtonEnable),
            typeof(bool),
            typeof(CustomTabViewQuaternary),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Third Button Enable
        /// </summary>
        public static readonly BindableProperty ThirdButtonEnableProperty = BindableProperty.Create(nameof(ThirdButtonEnable),
            typeof(bool),
            typeof(CustomTabViewQuaternary),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Quaternary Button Enable
        /// </summary>
        public static readonly BindableProperty QuaternaryButtonEnableProperty = BindableProperty.Create(nameof(QuaternaryButtonEnable),
            typeof(bool),
            typeof(CustomTabViewQuaternary),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Fifth Button Enable
        /// </summary>
        public static readonly BindableProperty FifthButtonEnableProperty = BindableProperty.Create(nameof(FifthButtonEnable),
            typeof(bool),
            typeof(CustomTabViewQuaternary),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Text Property
        /// Button 1 text
        /// </summary>
        public static readonly BindableProperty Button1TextProperty = BindableProperty.Create(nameof(Button1Text),
            typeof(string),
            typeof(CustomTabViewQuaternary),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Text Property
        /// Button 2 text
        /// </summary>
        public static readonly BindableProperty Button2TextProperty = BindableProperty.Create(nameof(Button2Text),
            typeof(string),
            typeof(CustomTabViewQuaternary),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Text Property
        /// Button 3 text
        /// </summary>
        public static readonly BindableProperty Button3TextProperty = BindableProperty.Create(nameof(Button3Text),
            typeof(string),
            typeof(CustomTabViewDual),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Text Property
        /// Button 4 text
        /// </summary>
        public static readonly BindableProperty Button4TextProperty = BindableProperty.Create(nameof(Button4Text),
            typeof(string),
            typeof(CustomTabViewDual),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Text Property
        /// Button 5 text
        /// </summary>
        public static readonly BindableProperty Button5TextProperty = BindableProperty.Create(nameof(Button5Text),
            typeof(string),
            typeof(CustomTabViewDual),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// ActiveTabIndexProperty
        /// Active Tab Index
        /// </summary>
        public static readonly BindableProperty ActiveTabIndexProperty = BindableProperty.Create(nameof(ActiveTabIndex),
            typeof(int),
            typeof(CustomTabViewDual),
            default(int),
            Xamarin.Forms.BindingMode.TwoWay);

        /// <summary>
        /// FirstButtonEnable
        /// </summary>
        public bool FirstButtonEnable
        {
            get
            {
                return (bool)GetValue(FirstButtonEnableProperty);
            }
            set
            {
                SetValue(FirstButtonEnableProperty, value);
            }
        }

        /// <summary>
        /// SecondButtonEnable
        /// </summary>
        public bool SecondButtonEnable
        {
            get
            {
                return (bool)GetValue(SecondButtonEnableProperty);
            }
            set
            {
                SetValue(SecondButtonEnableProperty, value);
            }
        }

        /// <summary>
        /// ThirdButtonEnable
        /// </summary>
        public bool ThirdButtonEnable
        {
            get
            {
                return (bool)GetValue(ThirdButtonEnableProperty);
            }
            set
            {
                SetValue(ThirdButtonEnableProperty, value);
            }
        }

        /// <summary>
        /// QuaternaryButtonEnable
        /// </summary>
        public bool QuaternaryButtonEnable
        {
            get
            {
                return (bool)GetValue(QuaternaryButtonEnableProperty);
            }
            set
            {
                SetValue(QuaternaryButtonEnableProperty, value);
            }
        }

        /// <summary>
        /// FifthButtonEnable
        /// </summary>
        public bool FifthButtonEnable
        {
            get
            {
                return (bool)GetValue(FifthButtonEnableProperty);
            }
            set
            {
                SetValue(FifthButtonEnableProperty, value);
            }
        }

        /// <summary>
        /// Button1Text
        /// </summary>
        public string Button1Text
        {
            get
            {
                return (string)GetValue(Button1TextProperty);
            }

            set
            {
                SetValue(Button1TextProperty, value);
            }
        }

        /// <summary>
        /// Button2Text
        /// </summary>
        public string Button2Text
        {
            get
            {
                return (string)GetValue(Button2TextProperty);
            }

            set
            {
                SetValue(Button2TextProperty, value);
            }
        }

        /// <summary>
        /// Button3Text
        /// </summary>
        public string Button3Text
        {
            get
            {
                return (string)GetValue(Button3TextProperty);
            }

            set
            {
                SetValue(Button3TextProperty, value);
            }
        }

        /// <summary>
        /// Button4Text
        /// </summary>
        public string Button4Text
        {
            get
            {
                return (string)GetValue(Button4TextProperty);
            }

            set
            {
                SetValue(Button4TextProperty, value);
            }
        }

        /// <summary>
        /// Button5Text
        /// </summary>
        public string Button5Text
        {
            get
            {
                return (string)GetValue(Button5TextProperty);
            }

            set
            {
                SetValue(Button5TextProperty, value);
            }
        }

        /// <summary>
        /// ActiveTabIndex
        /// </summary>
        public int ActiveTabIndex
        {
            get
            {
                return (int)GetValue(ActiveTabIndexProperty);
            }

            set
            {
                SetValue(ActiveTabIndexProperty, value);
            }
        }

        /// <summary>
        /// Tab1 Clicked
        /// </summary>
        public event EventHandler<EventArgs> Button1Clicked;

        /// <summary>
        /// Tab2 Clicked
        /// </summary>
        public event EventHandler<EventArgs> Button2Clicked;

        /// <summary>
        /// Tab3 Clicked
        /// </summary>
        public event EventHandler<EventArgs> Button3Clicked;

        /// <summary>
        /// Tab4 Clicked
        /// </summary>
        public event EventHandler<EventArgs> Button4Clicked;

        /// <summary>
        /// Tab5 Clicked
        /// </summary>
        public event EventHandler<EventArgs> Button5Clicked;

        /// <summary>
        /// Tab6 Clicked
        /// </summary>
        public event EventHandler<EventArgs> Button6Clicked;

        public CustomTabViewQuaternary()
        {
            InitializeComponent();

            btnItem1.IsEnabled = FirstButtonEnable;
            btnItem2.IsEnabled = SecondButtonEnable;
            btnItem3.IsEnabled = ThirdButtonEnable;
            btnItem4.IsEnabled = QuaternaryButtonEnable;
            btnItem5.IsEnabled = FifthButtonEnable;

            btnItem1.Text = Button1Text;
            btnItem2.Text = Button2Text;
            btnItem3.Text = Button3Text;
            btnItem4.Text = Button4Text;
            btnItem5.Text = Button5Text;

            ActiveTabIndex = 0;

            focusedItem1();
        }

        /// <summary>
        /// Property Change
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == FirstButtonEnableProperty.PropertyName)
            {
                btnItem1.IsEnabled = FirstButtonEnable;
            }
            else if (propertyName == SecondButtonEnableProperty.PropertyName)
            {
                btnItem2.IsEnabled = SecondButtonEnable;
            }
            else if (propertyName == ThirdButtonEnableProperty.PropertyName)
            {
                btnItem3.IsEnabled = ThirdButtonEnable;
            }
            else if (propertyName == QuaternaryButtonEnableProperty.PropertyName)
            {
                btnItem4.IsEnabled = QuaternaryButtonEnable;
            }
            else if (propertyName == FifthButtonEnableProperty.PropertyName)
            {
                btnItem5.IsEnabled = FifthButtonEnable;
            }
            else if (propertyName == Button1TextProperty.PropertyName)
            {
                btnItem1.Text = Button1Text;
            }
            else if (propertyName == Button2TextProperty.PropertyName)
            {
                btnItem2.Text = Button2Text;
            }
            else if (propertyName == Button3TextProperty.PropertyName)
            {
                btnItem3.Text = Button3Text;
            }
            else if (propertyName == Button4TextProperty.PropertyName)
            {
                btnItem4.Text = Button4Text;
            }
            else if (propertyName == Button5TextProperty.PropertyName)
            {
                btnItem5.Text = Button5Text;
            }
            else if (propertyName == ActiveTabIndexProperty.PropertyName)
            {
                if (ActiveTabIndex == 0)
                    focusedItem1();
                else if (ActiveTabIndex == 1)
                    focusedItem2();
                else if (ActiveTabIndex == 2)
                    focusedItem3();
                else if (ActiveTabIndex == 3)
                    focusedItem4();
                else if (ActiveTabIndex == 4)
                    focusedItem5();
            }
        }

        /// <summary>
        /// Tab1 Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnItem1_Clicked(object sender, EventArgs e)
        {
            ActiveTabIndex = 0;

            if (Button1Clicked != null)
            {
                Button1Clicked(sender, e);
            }

            focusedItem1();
        }

        void focusedItem1()
        {
            Application.Current.Resources.TryGetValue("Primary.Base", out var enableTextColor);
            Application.Current.Resources.TryGetValue("Ink.Lighter", out var disableTextColor);

            btnItem1.TextColor = (Color)enableTextColor;
            btnItem2.TextColor = (Color)disableTextColor;
            btnItem3.TextColor = (Color)disableTextColor;
            btnItem4.TextColor = (Color)disableTextColor;
            btnItem5.TextColor = (Color)disableTextColor;

            bxItem1.IsVisible = true;
            bxItem2.IsVisible = false;
            bxItem3.IsVisible = false;
            bxItem4.IsVisible = false;
            bxItem5.IsVisible = false;
        }

        /// <summary>
        /// Tab2 Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnItem2_Clicked(object sender, EventArgs e)
        {
            ActiveTabIndex = 1;

            if (Button2Clicked != null)
            {
                Button2Clicked(sender, e);
            }

            focusedItem2();
        }

        void focusedItem2()
        {
            Application.Current.Resources.TryGetValue("Primary.Base", out var enableTextColor);
            Application.Current.Resources.TryGetValue("Ink.Lighter", out var disableTextColor);

            btnItem1.TextColor = (Color)disableTextColor;
            btnItem2.TextColor = (Color)enableTextColor;
            btnItem3.TextColor = (Color)disableTextColor;
            btnItem4.TextColor = (Color)disableTextColor;
            btnItem5.TextColor = (Color)disableTextColor;

            bxItem1.IsVisible = false;
            bxItem2.IsVisible = true;
            bxItem3.IsVisible = false;
            bxItem4.IsVisible = false;
            bxItem5.IsVisible = false;
        }

        /// <summary>
        /// Tab4 Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnItem3_Clicked(object sender, EventArgs e)
        {
            ActiveTabIndex = 2;

            if (Button3Clicked != null)
            {
                Button3Clicked(sender, e);
            }

            focusedItem3();
        }

        void focusedItem3()
        {
            Application.Current.Resources.TryGetValue("Primary.Base", out var enableTextColor);
            Application.Current.Resources.TryGetValue("Ink.Lighter", out var disableTextColor);

            btnItem1.TextColor = (Color)disableTextColor;
            btnItem2.TextColor = (Color)disableTextColor;
            btnItem3.TextColor = (Color)enableTextColor;
            btnItem4.TextColor = (Color)disableTextColor;
            btnItem5.TextColor = (Color)disableTextColor;

            bxItem1.IsVisible = false;
            bxItem2.IsVisible = false;
            bxItem3.IsVisible = true;
            bxItem4.IsVisible = false;
            bxItem5.IsVisible = false;
        }

        /// <summary>
        /// Tab4 Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnItem4_Clicked(object sender, EventArgs e)
        {
            ActiveTabIndex = 3;

            if (Button4Clicked != null)
            {
                Button4Clicked(sender, e);
            }

            focusedItem4();
        }

        void focusedItem4()
        {
            Application.Current.Resources.TryGetValue("Primary.Base", out var enableTextColor);
            Application.Current.Resources.TryGetValue("Ink.Lighter", out var disableTextColor);

            btnItem1.TextColor = (Color)disableTextColor;
            btnItem2.TextColor = (Color)disableTextColor;
            btnItem3.TextColor = (Color)disableTextColor;
            btnItem4.TextColor = (Color)enableTextColor;
            btnItem5.TextColor = (Color)disableTextColor;

            bxItem1.IsVisible = false;
            bxItem2.IsVisible = false;
            bxItem3.IsVisible = false;
            bxItem4.IsVisible = true;
            bxItem5.IsVisible = false;
        }

        /// <summary>
        /// Tab5 Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnItem5_Clicked(object sender, EventArgs e)
        {
            ActiveTabIndex = 4;

            if (Button5Clicked != null)
            {
                Button5Clicked(sender, e);
            }

            focusedItem5();
        }

        void focusedItem5()
        {
            Application.Current.Resources.TryGetValue("Primary.Base", out var enableTextColor);
            Application.Current.Resources.TryGetValue("Ink.Lighter", out var disableTextColor);

            btnItem1.TextColor = (Color)disableTextColor;
            btnItem2.TextColor = (Color)disableTextColor;
            btnItem3.TextColor = (Color)disableTextColor;
            btnItem4.TextColor = (Color)disableTextColor;
            btnItem5.TextColor = (Color)enableTextColor;

            bxItem1.IsVisible = false;
            bxItem2.IsVisible = false;
            bxItem3.IsVisible = false;
            bxItem4.IsVisible = false;
            bxItem5.IsVisible = true;
        }
    }
}