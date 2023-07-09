using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProsysMobile.CustomControls.Backdrop;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.Models
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomBackdrop : BottomToTopBackdropPopupPage
    {
        public CustomBackdrop()
        {
            InitializeComponent();
        }
    }
}