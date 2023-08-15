using System;
using System.Windows.Input;
using ProsysMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Button
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomStandardButtonSecondary : Label
    {
        /// <summary>
        /// Label Click
        /// </summary>
        public event EventHandler<EventArgs> Clicked;
        
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(CustomStandardButtonSecondary),
            default(ICommand),
            BindingMode.TwoWay);
        
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public CustomStandardButtonSecondary()
        {
            InitializeComponent();

            ItemLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    try
                    {
                        if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;
                        
                        await ItemLabel.ScaleTo(.7, 100, Easing.CubicIn);
                    
                        Clicked?.Invoke(ItemLabel, null);
                        Command?.Execute(null);
                    
                        await ItemLabel.ScaleTo(1, 100, Easing.CubicOut);
                        
                    }
                    catch (Exception ex)
                    {
                        ProsysLogger.Instance.CrashLog(ex);
                    }
                    
                    DoubleTapping.ResumeTap();
                })
            });
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            
        }

    }
}