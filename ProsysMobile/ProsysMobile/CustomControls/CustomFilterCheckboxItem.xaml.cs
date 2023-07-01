using System;
using ProsysMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomFilterCheckboxItem : Grid
    {

        /// <summary>
        /// IsChecked Property
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
            nameof(IsChecked),
            typeof(bool),
            typeof(CustomFilterCheckboxItem),
            default(bool),
            Xamarin.Forms.BindingMode.TwoWay);

        /// <summary>
        /// Title Property
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(CustomFilterCheckboxItem),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// IsChecked
        /// </summary>
        public bool IsChecked
        {
            get
            {
                return (bool)GetValue(IsCheckedProperty);
            }

            set
            {
                SetValue(IsCheckedProperty, value);
            }
        }

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

        public CustomFilterCheckboxItem()
        {
            InitializeComponent();

            ItemLabel.Text = Title;
            ItemCheckbox.IsChecked = IsChecked;
        }

        /// <summary>
        /// On Change Property
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsCheckedProperty.PropertyName)
            {
                ItemCheckbox.IsChecked = IsChecked;
            }
            else if (propertyName == TitleProperty.PropertyName)
            {
                ItemLabel.Text = Title;
            }
        }

        /// <summary>
        /// Grid Tapped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                ItemCheckbox.IsChecked = !ItemCheckbox.IsChecked;

                DoubleTapping.ResumeTap();
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);

                DoubleTapping.ResumeTap();
                return;
            }
        }

        private void ItemCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            IsChecked = e.Value;
        }
    }
}