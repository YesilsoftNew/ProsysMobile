using System;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Bar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTabViewTriple : PancakeView
    {
        /// <summary>
        /// First Button Enable
        /// </summary>
        public static readonly BindableProperty FirstButtonEnableProperty = BindableProperty.Create(nameof(FirstButtonEnable),
            typeof(bool),
            typeof(CustomTabViewTriple),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Second Button Enable
        /// </summary>
        public static readonly BindableProperty SecondButtonEnableProperty = BindableProperty.Create(nameof(SecondButtonEnable),
            typeof(bool),
            typeof(CustomTabViewTriple),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Third Button Enable
        /// </summary>
        public static readonly BindableProperty ThirdButtonEnableProperty = BindableProperty.Create(nameof(ThirdButtonEnable),
            typeof(bool),
            typeof(CustomTabViewTriple),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// First Button Visible
        /// </summary>
        public static readonly BindableProperty FirstButtonVisibleProperty = BindableProperty.Create(nameof(FirstButtonVisible),
            typeof(bool),
            typeof(CustomTabViewTriple),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Second Button Visible
        /// </summary>
        public static readonly BindableProperty SecondButtonVisibleProperty = BindableProperty.Create(nameof(SecondButtonVisible),
            typeof(bool),
            typeof(CustomTabViewTriple),
            true,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Third Button Visible
        /// </summary>
        public static readonly BindableProperty ThirdButtonVisibleProperty = BindableProperty.Create(nameof(ThirdButtonVisible),
            typeof(bool),
            typeof(CustomTabViewTriple),
            true,
            Xamarin.Forms.BindingMode.OneWay);
          
        /// <summary>
        /// Text Property
        /// Button 1 text
        /// </summary>
        public static readonly BindableProperty Button1TextProperty = BindableProperty.Create(nameof(Button1Text),
            typeof(string),
            typeof(CustomTabViewTriple),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Text Property
        /// Button 2 text
        /// </summary>
        public static readonly BindableProperty Button2TextProperty = BindableProperty.Create(nameof(Button2Text),
            typeof(string),
            typeof(CustomTabViewTriple),
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
        /// ThirdButtonVisible
        /// </summary>
        public bool ThirdButtonVisible
        {
            get
            {
                return (bool)GetValue(ThirdButtonVisibleProperty);
            }
            set
            {
                SetValue(ThirdButtonVisibleProperty, value);
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

        public CustomTabViewTriple()
        {
            InitializeComponent();

            btnItem1.IsEnabled = FirstButtonEnable;
            btnItem2.IsEnabled = SecondButtonEnable;
            btnItem3.IsEnabled = ThirdButtonEnable;

            btnItem1.IsVisible = FirstButtonVisible;
            btnItem2.IsVisible = SecondButtonVisible;
            btnItem3.IsVisible = ThirdButtonVisible;

            btnItem1.Text = Button1Text;
            btnItem2.Text = Button2Text;
            btnItem3.Text = Button3Text;

            ActiveTabIndex = 0;
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
            else if (propertyName == FirstButtonVisibleProperty.PropertyName)
            {
                btnItem1.IsVisible = FirstButtonVisible;
            }
            else if (propertyName == SecondButtonVisibleProperty.PropertyName)
            {
                btnItem2.IsVisible = SecondButtonVisible;
            }
            else if (propertyName == ThirdButtonVisibleProperty.PropertyName)
            {
                btnItem3.IsVisible = ThirdButtonVisible;
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
            else if (propertyName == ActiveTabIndexProperty.PropertyName)
            {
                if (ActiveTabIndex == 0)
                    focusedItem1();
                else if (ActiveTabIndex == 1)
                    focusedItem2();
                else if (ActiveTabIndex == 2)
                    focusedItem3(); 
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
            btnItem3.BackgroundColor = (Color)disableColor;
            btnItem3.TextColor = (Color)disableTextColor;
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
            btnItem3.BackgroundColor = (Color)disableColor;
            btnItem3.TextColor = (Color)disableTextColor;
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
            Application.Current.Resources.TryGetValue("Sky.White", out var enableColor);
            Application.Current.Resources.TryGetValue("Sky.Lighter", out var disableColor);
            Application.Current.Resources.TryGetValue("Ink.Darkest", out var enableTextColor);
            Application.Current.Resources.TryGetValue("Ink.Light", out var disableTextColor);

            btnItem1.BackgroundColor = (Color)disableColor;
            btnItem1.TextColor = (Color)disableTextColor;
            btnItem2.BackgroundColor = (Color)disableColor;
            btnItem2.TextColor = (Color)disableTextColor;
            btnItem3.BackgroundColor = (Color)enableColor;
            btnItem3.TextColor = (Color)enableTextColor;
        }
    }
}