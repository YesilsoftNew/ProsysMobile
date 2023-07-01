using ProsysMobile.Helper;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Services;
using System;
using System.ComponentModel;
using System.Linq;
using WiseMobile.Pages.System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Backdrop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomToTopBackdropPopupPage : BackdropPage, INotifyPropertyChanged
    {
        public BottomToTopBackdropPopupPage()
        {
            InitializeComponent();

            try
            {
                // Toast message sayfasını dikkate almıyoruz
                var isActiveToastMessagePage = PopupNavigation.Instance.PopupStack.Count(x => x.ToString() == new ToastMessagePage().ToString());

                if (PopupNavigation.Instance.PopupStack.Count - isActiveToastMessagePage > 0) // bir tane acık var ise devamında acılan backdrop'ların araka planını karartmıyorum bi sure sonra sim siyah oluyor
                    this.BackgroundColor = Color.Transparent;

                //Animation = new MoveAnimation
                //{
                //    PositionIn = MoveAnimationOptions.Bottom,
                //    PositionOut = MoveAnimationOptions.Bottom,
                //};
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }

            SetPanListener();

            Indictor.IsVisible = IndictorVisible;
        }

        /// <summary>
        /// IndictorVisible property
        /// </summary>
        public static readonly BindableProperty IndictorVisibleProperty = BindableProperty.Create(
            nameof(IndictorVisible),
            typeof(bool),
            typeof(BottomToTopBackdropPopupPage),
            false,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// IsSetPanListener property
        /// </summary>
        public static readonly BindableProperty IsSetPanListenerProperty = BindableProperty.Create(
            nameof(IsSetPanListener),
            typeof(bool),
            typeof(BottomToTopBackdropPopupPage),
            false,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// SetPageAnimationRight Property
        /// </summary>
        public static readonly BindableProperty SetPageAnimationRightProperty = BindableProperty.Create(
            nameof(SetPageAnimationRight),
            typeof(bool),
            typeof(BottomToTopBackdropPopupPage),
            false,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// IsSetPanListener
        /// </summary>
        public bool IsSetPanListener
        {
            get
            {
                return (bool)GetValue(IsSetPanListenerProperty);
            }
            set
            {
                SetValue(IsSetPanListenerProperty, value);
            }
        }

        /// <summary>
        /// SetPageAnimationRight
        /// </summary>
        public bool SetPageAnimationRight
        {
            get
            {
                return (bool)GetValue(SetPageAnimationRightProperty);
            }
            set
            {
                SetValue(SetPageAnimationRightProperty, value);
            }
        }

        /// <summary>
        /// IndictorVisible
        /// </summary>
        public bool IndictorVisible
        {
            get
            {
                return (bool)GetValue(IndictorVisibleProperty);
            }
            set
            {
                SetValue(IndictorVisibleProperty, value);
            }
        }

        // simdilik kapatıyorum bunu full page olcak mıs hepsi
        //protected override void OnAppearingAnimationBegin()
        //{
        //    if (ViewContent.Content is ContentView _contentView)
        //    {
        //        if (_contentView.Content is Xamarin.Forms.Grid grdMain)
        //        {
        //            if (((Application.Current.MainPage.Height - 0) - grdMain.Height) > 0)
        //                this.Padding = new Thickness(0, (Application.Current.MainPage.Height - 0) - grdMain.Height, 0, 0); grdMain.HeightRequest = grdMain.Height;

        //            base.OnAppearingAnimationBegin();
        //        }
        //    }
        //}

        /// <summary>
        /// Property Change
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsSetPanListenerProperty.PropertyName)
                SetPanListener();
            else if (propertyName == IndictorVisibleProperty.PropertyName)
                Indictor.IsVisible = IndictorVisible;
            else if (propertyName == SetPageAnimationRightProperty.PropertyName)
            {
                if (SetPageAnimationRight)
                {
                    Animation = new MoveAnimation
                    {
                        PositionIn = MoveAnimationOptions.Right,
                        PositionOut = MoveAnimationOptions.Right,
                    };
                }
                else
                {
                    Animation = new MoveAnimation
                    {
                        PositionIn = MoveAnimationOptions.Bottom,
                        PositionOut = MoveAnimationOptions.Bottom,
                    };
                }
            }
        }

        void SetPanListener()
        {
            try
            {
                if (IsSetPanListener)
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        CoverView.IsVisible = true;
                        PanGestureRecognizer panGestureRecognizer = new PanGestureRecognizer();
                        panGestureRecognizer.PanUpdated += AndroidPanGestureRecognizer_PanUpdated;
                        CoverView.GestureRecognizers.Add(panGestureRecognizer);
                    }
                    else if (Device.RuntimePlatform == Device.iOS)
                    {
                        CoverView.IsVisible = false;
                        PanGestureRecognizer panGestureRecognizer = new PanGestureRecognizer();
                        panGestureRecognizer.PanUpdated += iOSPanGestureRecognizer_PanUpdated;
                        FullView.GestureRecognizers.Add(panGestureRecognizer);
                    }
                }
                else
                {
                    CoverView.IsVisible = false;

                    if (Device.RuntimePlatform == Device.Android)
                    {
                        if (CoverView.GestureRecognizers.Count > 0)
                            CoverView.GestureRecognizers.RemoveAt(0);
                    }
                    else if (Device.RuntimePlatform == Device.iOS)
                    {
                        if (FullView.GestureRecognizers.Count > 0)
                            FullView.GestureRecognizers.RemoveAt(0);
                    }
                }
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        private async void iOSPanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    StartPanDownTime = DateTimeOffset.Now;
                    break;

                case GestureStatus.Running:
                    if (e.TotalY > 0)
                    {
                        Indictor.TranslateTo(0, e.TotalY + StartY, 20, Easing.Linear);
                        PageView.TranslateTo(0, e.TotalY, 20, Easing.Linear);
                    }

                    break;

                case GestureStatus.Completed:
                    EndPanDownTime = DateTimeOffset.Now;
                    if (EndPanDownTime.Value.ToUnixTimeMilliseconds() - StartPanDownTime.Value.ToUnixTimeMilliseconds() < SwipeToCloseTime)
                    {
                        await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();

                        if (this.BackgroundClickedCommand != null && this.CloseWhenBackgroundIsClicked)
                            this.BackgroundClickedCommand.Execute(this.BackgroundClickedCommandParameter);
                    }
                    else
                    {
                        Indictor.TranslateTo(0, 0, 250, Easing.Linear);
                        PageView.TranslateTo(0, 0, 250, Easing.Linear);
                    }
                    break;
            }

            if (e.StatusType == GestureStatus.Completed || e.StatusType == GestureStatus.Canceled)
            {
                StartPanDownTime = null;
                EndPanDownTime = null;
            }
        }

        private async void AndroidPanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            ContentView view = sender as ContentView;
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    StartPanDownTime = DateTimeOffset.Now;
                    StartY = view.Y;
                    break;

                case GestureStatus.Running:
                    if (e.TotalY > 0)
                    {
                        Indictor.TranslateTo(0, e.TotalY + StartY, 20, Easing.Linear);
                        PageView.TranslateTo(0, e.TotalY + StartY, 20, Easing.Linear);
                    }
                    break;

                case GestureStatus.Completed:
                    EndPanDownTime = DateTimeOffset.Now;
                    if (EndPanDownTime.Value.ToUnixTimeMilliseconds() - StartPanDownTime.Value.ToUnixTimeMilliseconds() < SwipeToCloseTime)
                    {
                        await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();

                        if (this.BackgroundClickedCommand != null && this.CloseWhenBackgroundIsClicked)
                            this.BackgroundClickedCommand.Execute(this.BackgroundClickedCommandParameter);
                    }
                    else
                    {
                        PageView.TranslateTo(0, 0, 250, Easing.Linear);
                        Indictor.TranslateTo(0, 0, 250, Easing.Linear);
                    }
                    break;
            }

            if (e.StatusType == GestureStatus.Completed || e.StatusType == GestureStatus.Canceled)
            {
                StartPanDownTime = null;
                EndPanDownTime = null;
            }
        }
    }
}