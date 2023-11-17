using System;
using System.Windows.Input;
using ProsysMobile.Models.CommonModels.OtherModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Other
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketCounter
    {
        public static readonly BindableProperty ItemIdProperty = BindableProperty.Create(nameof(ItemId),
            typeof(int),
            typeof(BasketCounter),
            default(int),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string),
            typeof(BasketCounter),
            "1",
            BindingMode.TwoWay);
        
        public static readonly BindableProperty StockCountProperty = BindableProperty.Create(nameof(StockCount),
            typeof(int),
            typeof(BasketCounter),
            default(int),
            BindingMode.TwoWay);
        
        public static readonly BindableProperty ChangeCountCommandProperty = BindableProperty.Create(nameof(ChangeCountCommand),
            typeof(ICommand),
            typeof(BasketCounter),
            default(ICommand),
            BindingMode.TwoWay);
        
        public static BindableProperty ChangeCountCommandParameterProperty = BindableProperty.CreateAttached(
            nameof(ChangeCountCommandParameter),
            typeof(object),
            typeof(BasketCounter),
            null,
            BindingMode.TwoWay);

        public int ItemId
        {
            get => (int)GetValue(ItemIdProperty);
            set => SetValue(ItemIdProperty, value);
        }
        
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        
        public int StockCount
        {
            get => (int)GetValue(StockCountProperty);
            set => SetValue(StockCountProperty, value);
        }
        
        public ICommand ChangeCountCommand
        {
            get => (Command)GetValue(ChangeCountCommandProperty);
            set => SetValue(ChangeCountCommandProperty, value);
        }
        
        public object ChangeCountCommandParameter
        {
            get => GetValue(ChangeCountCommandParameterProperty);
            set => SetValue(ChangeCountCommandParameterProperty, value);
        }

        private string focusedBeforeEntryCounterText = string.Empty;
        
        public BasketCounter()
        {
            InitializeComponent();

            EntryCounter.Text = Text;

            MinusAndTrashGrid.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    var entryValue = Convert.ToInt32(EntryCounter.Text);
                    
                    if (entryValue > 1)
                    {
                        entryValue -= 1;
                        EntryCounter.Text = entryValue.ToString();
                        Text = EntryCounter.Text;
                        
                        ChangeCountCommand?.Execute(new ChangeItemCountCommandParameterModel
                        {
                            ItemId = ItemId,
                            Count = entryValue,
                            IsDeleteItem = false
                        });
                        
                        SetImageMinusOrTrash();
                    }
                    else
                    {
                        ChangeCountCommand?.Execute(new ChangeItemCountCommandParameterModel
                        {
                            ItemId = ItemId,
                            Count = 0,
                            IsDeleteItem = true
                        });
                    }
                })
            });
            
            PlusGrid.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    var entryValue = Convert.ToInt32(EntryCounter.Text);

                    entryValue += 1;

                    if (entryValue > StockCount + entryValue -1)
                    {
                        entryValue = StockCount + entryValue -1;
                    }
                    
                    EntryCounter.Text = entryValue.ToString();
                    Text = EntryCounter.Text;
                    
                    SetImageMinusOrTrash();

                    ChangeCountCommand?.Execute(new ChangeItemCountCommandParameterModel
                    {
                        ItemId = ItemId,
                        Count = entryValue,
                        IsDeleteItem = false
                    });
                })
            });
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                EntryCounter.Text = Text;

                SetImageMinusOrTrash();
            }
        }
        
        private void EntryCounter_OnFocused(object sender, FocusEventArgs e)
        {
            focusedBeforeEntryCounterText = EntryCounter.Text;
            
            Device.BeginInvokeOnMainThread(() =>
            {
                EntryCounter.CursorPosition = 0;
                EntryCounter.SelectionLength = EntryCounter.Text.Length;
            });
        }

        private void EntryCounter_OnUnfocused(object sender, FocusEventArgs e)
        {
            var entryText = EntryCounter.Text;
            
            EntryCounter.Text = string.IsNullOrWhiteSpace(entryText) || Convert.ToInt64(entryText) < 0 ? "1" : entryText;
            Text = EntryCounter.Text;

            var entryTextInt = Convert.ToInt32(EntryCounter.Text);
            
            if (entryTextInt > StockCount + Convert.ToInt32(focusedBeforeEntryCounterText))
            {
                entryTextInt = StockCount + Convert.ToInt32(focusedBeforeEntryCounterText);
                EntryCounter.Text = string.IsNullOrWhiteSpace(entryTextInt.ToString()) || entryTextInt < 0 ? "1" : entryTextInt.ToString();
                Text = EntryCounter.Text;
            }
            
            if (focusedBeforeEntryCounterText == entryTextInt.ToString()) return;
            
            ChangeCountCommand?.Execute(new ChangeItemCountCommandParameterModel
            {
                ItemId = ItemId,
                Count = entryTextInt,
                IsDeleteItem = false
            });
            
            SetImageMinusOrTrash();
        }

        private void EntryCounter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var newTextValue = e.NewTextValue; 
            
            newTextValue = newTextValue.Replace(",", "");
            newTextValue = newTextValue.Replace(".", "");
            newTextValue = newTextValue.Replace("-", "");

            EntryCounter.Text = newTextValue;
        }

        private void SetImageMinusOrTrash()
        {
            var entryValue = Convert.ToInt64(EntryCounter.Text);

            ImageMinusOrTrash.Source = entryValue <= 1 ? "TrashCounter" : "MinusCounter";
        }
    }
}