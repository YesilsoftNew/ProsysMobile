using System;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Bar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTabViewDual : PancakeView
    {
        /// <summary>
        /// Second Button Enable
        /// </summary>
        public static readonly BindableProperty SecondButtonEnableProperty = BindableProperty.Create(nameof(SecondButtonEnable),
            typeof(bool),
            typeof(CustomTabViewDual),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Text Property
        /// Button 1 text
        /// </summary>
        public static readonly BindableProperty Button1TextProperty = BindableProperty.Create(nameof(Button1Text),
            typeof(string),
            typeof(CustomTabViewDual),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Text Property
        /// Button 2 text
        /// </summary>
        public static readonly BindableProperty Button2TextProperty = BindableProperty.Create(nameof(Button2Text),
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
        /// IsButtonVisible
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
        /// Placeholder
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
        /// Placeholder
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
        /// Tab2 Clicked
        /// </summary>
        public event EventHandler<EventArgs> Button1Clicked;

        /// <summary>
        /// Tab2 Clicked
        /// </summary>
        public event EventHandler<EventArgs> Button2Clicked;

        public CustomTabViewDual()
        {
            InitializeComponent();

            btnItem2.IsEnabled = SecondButtonEnable;
            btnItem1.Text = Button1Text;
            btnItem2.Text = Button2Text;
            ActiveTabIndex = 0;
        }

        /// <summary>
        /// Property Change
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == SecondButtonEnableProperty.PropertyName)
            {
                btnItem2.IsEnabled = SecondButtonEnable;
            }
            else if (propertyName == Button1TextProperty.PropertyName)
            {
                btnItem1.Text = Button1Text;
            }
            else if (propertyName == Button2TextProperty.PropertyName)
            {
                btnItem2.Text = Button2Text;
            }
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
            Application.Current.Resources.TryGetValue("Sky.White", out var enableColor);
            Application.Current.Resources.TryGetValue("Sky.Lighter", out var disableColor);
            Application.Current.Resources.TryGetValue("Ink.Darkest", out var enableTextColor);
            Application.Current.Resources.TryGetValue("Ink.Light", out var disableTextColor);

            btnItem1.BackgroundColor = (Color)enableColor;
            btnItem1.TextColor = (Color)enableTextColor;
            btnItem2.BackgroundColor = (Color)disableColor;
            btnItem2.TextColor = (Color)disableTextColor;
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
            Application.Current.Resources.TryGetValue("Sky.White", out var enableColor);
            Application.Current.Resources.TryGetValue("Sky.Lighter", out var disableColor);
            Application.Current.Resources.TryGetValue("Ink.Darkest", out var enableTextColor);
            Application.Current.Resources.TryGetValue("Ink.Light", out var disableTextColor);

            btnItem1.BackgroundColor = (Color)disableColor;
            btnItem1.TextColor = (Color)disableTextColor;
            btnItem2.BackgroundColor = (Color)enableColor;
            btnItem2.TextColor = (Color)enableTextColor;
        }
    }
}