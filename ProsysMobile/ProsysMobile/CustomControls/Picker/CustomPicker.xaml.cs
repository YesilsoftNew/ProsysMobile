using System;
using System.Collections;
using System.Linq;
using ProsysMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Picker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomPicker : PancakeView
    {
        /// <summary>
        /// The Parameter property.
        /// </summary>
        public static readonly BindableProperty ParameterProperty = BindableProperty.Create(
            nameof(Parameter),
            typeof(string),
            typeof(CustomPicker),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// The label title property.
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(CustomPicker),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// The label title property.
        /// </summary>
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource),
            typeof(string),
            typeof(CustomPicker),
            "ChevronDownBlack",
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// The title property.
        /// </summary>
        public static readonly BindableProperty PickerTitleProperty = BindableProperty.Create(
            nameof(PickerTitle), typeof(string),
            typeof(CustomPicker),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);
        /// <summary>
        /// The placeholder property.
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(CustomPicker),
            propertyChanged: (bindable, oldVal, newVal) => ((CustomPicker)bindable).OnPlaceholderChanged((string)newVal)
        );

        /// <summary>
        /// The items source property.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IList),
            typeof(CustomPicker),
            null,
            propertyChanged: (bindable, oldVal, newVal) => ((CustomPicker)bindable).OnItemsSourceChanged((IList)newVal)
        );

        /// <summary>
        /// The selected item property.
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem),
            typeof(object),
            typeof(CustomPicker),
            null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) => ((CustomPicker)bindable).OnSelectedItemChanged((object)newVal)
        );

        /// <summary>
        /// The item display binding property
        /// NOTE: Use the name of the property, you do not need to bind to this property.
        /// </summary>
        public static readonly BindableProperty ItemDisplayBindingProperty = BindableProperty.Create(
            nameof(ItemDisplayBinding),
            typeof(string),
            typeof(CustomPicker),
            null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) => ((CustomPicker)bindable).OnItemDisplayBindingChanged((string)newVal)
        );

        /// <summary>
        /// IsDisabled Property
        /// </summary>
        public static readonly BindableProperty IsDisabledProperty = BindableProperty.Create(
            nameof(IsDisabled),
            typeof(bool),
            typeof(CustomPicker),
            false,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// The label title property.
        /// </summary>
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
            nameof(SelectedIndex),
            typeof(int),
            typeof(CustomPicker),
            -1,
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// The cell's Parameter
        /// </summary>
        public string Parameter
        {
            get
            {
                return (string)GetValue(ParameterProperty);
            }

            set
            {
                SetValue(ParameterProperty, value);
            }
        }

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
        /// The cell's PickerTitle
        /// </summary>
        public string PickerTitle
        {
            get
            {
                return (string)GetValue(PickerTitleProperty);
            }

            set
            {
                SetValue(PickerTitleProperty, value);
            }
        }

        /// <summary>
        /// The cell's placeholder (select a ....).
        /// </summary>
        /// <value>The placeholder.</value>
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        /// <summary>
        /// The cell's picker item source.
        /// </summary>
        /// <value>The items source.</value>
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the item display binding.
        /// </summary>
        /// <value>The item display binding.</value>
        public string ItemDisplayBinding
        {
            get { return (string)GetValue(ItemDisplayBindingProperty); }
            set { SetValue(ItemDisplayBindingProperty, value); }
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


        /// <summary>
        /// SelectedIndex
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return (int)GetValue(SelectedIndexProperty);
            }

            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }

        /// <summary>
        /// Picker SelectedIndexChanged
        /// </summary>
        public event EventHandler<EventArgs> PickerSelectedIndexChanged;

        /// <summary>
        /// Called when PlaceholderProperty changes.
        /// </summary>
        /// <param name="newVal">New value.</param>
        private void OnPlaceholderChanged(string newVal)
        {
            ItemPicker.Title = newVal;
        }

        /// <summary>
        /// Called when ItemSourceProperty changes.
        /// </summary>
        private void OnItemsSourceChanged(IList list)
        {
            ItemPicker.ItemsSource = list;
        }

        /// <summary>
        /// Called when SelectedItemProperty changes.
        /// </summary>
        /// <param name="obj">Object.</param>
        private void OnSelectedItemChanged(object obj)
        {
            ItemPicker.SelectedItem = obj;
        }

        /// <summary>
        /// Called when ItemDisplayBindingProperty changes.
        /// </summary>
        /// <param name="newvalue">Newvalue.</param>
        private void OnItemDisplayBindingChanged(string newvalue)
        {
            ItemPicker.ItemDisplayBinding = new Binding(newvalue);
        }

        public CustomPicker()
        {
            InitializeComponent();

            try
            {
                ItemLabel.Text = Title;
                ItemPicker.ItemsSource = ItemsSource;
                ItemPicker.SelectedItem = SelectedItem;
                ItemPicker.Title = PickerTitle;
                ItemImage.Source = ImageSource;
                CheckIsDisabled();

                ItemPicker.SelectedIndexChanged += OnSelectedIndexChanged;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        /// <summary>
        /// OnPropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            try
            {
                if (propertyName == TitleProperty.PropertyName)
                    ItemLabel.Text = Title;
                else if (propertyName == ItemsSourceProperty.PropertyName)
                    ItemPicker.ItemsSource = ItemsSource;
                else if (propertyName == ImageSourceProperty.PropertyName)
                    ItemImage.Source = ImageSource;
                else if (propertyName == PickerTitleProperty.PropertyName)
                    ItemPicker.Title = PickerTitle;
                else if (propertyName == IsDisabledProperty.PropertyName)
                    CheckIsDisabled();
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        /// <summary>
        /// Calle when ItemPicker's SelectedIndexChanged event fires.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedItem = (ItemPicker.SelectedIndex < 0 || ItemPicker.SelectedIndex > ItemPicker.Items.Count - 1) ? null : ItemsSource[ItemPicker.SelectedIndex];
            SelectedIndex = ItemPicker.SelectedIndex;

            if (PickerSelectedIndexChanged != null)
                PickerSelectedIndexChanged(this, e);
        }

        /// <summary>
        /// Picker'lar için üstünde bulunan alana tıklanılınca focuslama yapar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnTappedPickerArea(object sender, EventArgs args)
        {
            try
            {
                if (IsDisabled) return;

                var target = sender as PancakeView;
                var grid = target.Children.FirstOrDefault(x => x is Grid) as Grid;
                var entry = grid.Children.FirstOrDefault(x => x is Renderer.CustomPicker) as Renderer.CustomPicker;
                entry.Focus();
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        /// <summary>
        /// Is Disabled Check Function
        /// </summary>
        void CheckIsDisabled()
        {
            Application.Current.Resources.TryGetValue("Sky.Lighter", out var disabledBgColor);

            BorderMain.BackgroundColor = (IsDisabled ? (Color)disabledBgColor : Color.Transparent);
            ItemPicker.IsEnabled = !IsDisabled;
        }
    }
}