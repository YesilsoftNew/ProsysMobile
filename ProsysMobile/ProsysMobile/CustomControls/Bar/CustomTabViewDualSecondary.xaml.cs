using System;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Bar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTabViewDualSecondary : PancakeView
    {
        /// <summary>
        /// First Button Enable
        /// </summary>
        public static readonly BindableProperty FirstButtonEnableProperty = BindableProperty.Create(nameof(FirstButtonEnable),
            typeof(bool),
            typeof(CustomTabViewDualSecondary),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Second Button Enable
        /// </summary>
        public static readonly BindableProperty SecondButtonEnableProperty = BindableProperty.Create(nameof(SecondButtonEnable),
            typeof(bool),
            typeof(CustomTabViewDualSecondary),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// First Button Visible
        /// </summary>
        public static readonly BindableProperty FirstButtonVisibleProperty = BindableProperty.Create(nameof(FirstButtonVisible),
            typeof(bool),
            typeof(CustomTabViewDualSecondary),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Second Button Visible
        /// </summary>
        public static readonly BindableProperty SecondButtonVisibleProperty = BindableProperty.Create(nameof(SecondButtonVisible),
            typeof(bool),
            typeof(CustomTabViewDualSecondary),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Text Property
        /// Button 1 text
        /// </summary>
        public static readonly BindableProperty Button1TextProperty = BindableProperty.Create(nameof(Button1Text),
            typeof(string),
            typeof(CustomTabViewDualSecondary),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Text Property
        /// Button 2 text
        /// </summary>
        public static readonly BindableProperty Button2TextProperty = BindableProperty.Create(nameof(Button2Text),
            typeof(string),
            typeof(CustomTabViewDualSecondary),
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
        /// FirstButtonVisible
        /// </summary>
        public bool FirstButtonVisible
        {
            get
            {
                return (bool)GetValue(FirstButtonVisibleProperty);
            }
            set
            {
                SetValue(FirstButtonVisibleProperty, value);
            }
        }

        /// <summary>
        /// SecondButtonVisible
        /// </summary>
        public bool SecondButtonVisible
        {
            get
            {
                return (bool)GetValue(SecondButtonVisibleProperty);
            }
            set
            {
                SetValue(SecondButtonVisibleProperty, value);
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

        public CustomTabViewDualSecondary()
        {
            InitializeComponent();

            btnItem1.IsEnabled = FirstButtonEnable;
            btnItem2.IsEnabled = SecondButtonEnable;

            btnItem1.IsVisible = FirstButtonVisible;
            btnItem2.IsVisible = SecondButtonVisible;

            btnItem1.Text = Button1Text;
            btnItem2.Text = Button2Text;

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
                btnItem1.IsEnabled = FirstButtonEnable;
            else if (propertyName == SecondButtonEnableProperty.PropertyName)
                btnItem2.IsEnabled = SecondButtonEnable;
            else if (propertyName == FirstButtonVisibleProperty.PropertyName)
                btnItem1.IsVisible = FirstButtonVisible;
            else if (propertyName == SecondButtonVisibleProperty.PropertyName)
                btnItem2.IsVisible = SecondButtonVisible;
            else if (propertyName == Button1TextProperty.PropertyName)
                btnItem1.Text = Button1Text;
            else if (propertyName == Button2TextProperty.PropertyName)
                btnItem2.Text = Button2Text;
            else if (propertyName == ActiveTabIndexProperty.PropertyName)
            {
                if (ActiveTabIndex == 0)
                    focusedItem1();
                else if (ActiveTabIndex == 1)
                    focusedItem2();
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

            bxItem1.IsVisible = true;
            bxItem2.IsVisible = false;
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

            bxItem1.IsVisible = false;
            bxItem2.IsVisible = true;
        }
    }
}