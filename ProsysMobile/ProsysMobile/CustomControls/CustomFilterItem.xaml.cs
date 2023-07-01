using System;
using WiseMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WiseMobile.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomFilterItem : Grid
    {
        /// <summary>
        /// Description Property
        /// </summary>
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            nameof(Source),
            typeof(string),
            typeof(CustomFilterItem),
            "ChevronRightBlack",
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Description Property
        /// </summary>
        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
            nameof(Description),
            typeof(string),
            typeof(CustomFilterItem),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Title Property
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(CustomFilterItem),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Source
        /// </summary>
        public string Source
        {
            get
            {
                return (string)GetValue(SourceProperty);
            }

            set
            {
                SetValue(SourceProperty, value);
            }
        }

        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get
            {
                return (string)GetValue(DescriptionProperty);
            }

            set
            {
                SetValue(DescriptionProperty, value);
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

        /// <summary>
        /// Grid Click
        /// </summary>
        public event EventHandler<EventArgs> Clicked;

        public CustomFilterItem()
        {
            InitializeComponent();

            ItemTitleLabel.Text = Title;
            ItemDescLabel.Text = Description;
            ItemImage.Source = Source;
            checkDescription();
        }

        /// <summary>
        /// On Change Property
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
            {
                ItemTitleLabel.Text = Title;
            }
            else if (propertyName == DescriptionProperty.PropertyName)
            {
                ItemDescLabel.Text = Description;
                checkDescription();
            }
            else if (propertyName == SourceProperty.PropertyName)
            {
                ItemImage.Source = Source;
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

                if (Clicked != null)
                {
                    Clicked(sender, e);
                }
                
                DoubleTapping.ResumeTap();
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);

                DoubleTapping.ResumeTap();
                return;
            }
        }
        
        /// <summary>
        /// Açıklama alanı yoksa tasarım değişir.
        /// </summary>
        private void checkDescription()
        {
            if (string.IsNullOrWhiteSpace(ItemDescLabel.Text))
            {
                ItemTitleLabel.Margin = new Thickness(0, 22, 0, 0);
                ItemLine.Margin = new Thickness(0, 22, 0, 0);
                ItemDescLabel.IsVisible = false;
            }
            else
            {
                ItemLine.Margin = new Thickness(0, 12, 0, 0);
                ItemTitleLabel.Margin = new Thickness(0, 12, 0, 0);
                ItemDescLabel.IsVisible = true;
            }
        }

    }
}