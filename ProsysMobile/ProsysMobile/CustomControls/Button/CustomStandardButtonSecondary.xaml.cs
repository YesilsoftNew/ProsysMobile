using System;
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

        public CustomStandardButtonSecondary()
        {
            InitializeComponent();

            ItemLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await ItemLabel.ScaleTo(.8, 100, Easing.CubicIn);
                    if (Clicked != null)
                    {
                        Clicked(ItemLabel, null);
                    }
                    await ItemLabel.ScaleTo(1, 100, Easing.CubicOut);
                })
            });
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

    }
}