using ProsysMobile.Helper;
using ProsysMobile.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages
{
    public class AppShellViewModel : ViewModelBase
    {
        public AppShellViewModel()
        {
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await loadPage();
        }

        async Task loadPage()
        {
            try
            {
                //IsBusy = true;


                //IsBusy = false;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);

                IsBusy = false;
            }
        }

        #region Propertys                                   
        //private string _connectionStatus;
        //public string ConnectionStatus { get => _connectionStatus; set { _connectionStatus = value; PropertyChanged(() => ConnectionStatus); } }

        //private Color _statusColor;
        //public Color StatusColor { get => _statusColor; set { _statusColor = value; PropertyChanged(() => StatusColor); } }
        #endregion

        #region Commands 
        public ICommand testCommand => new Command(async () =>
        {
            //await ExitApp();
        });

        #endregion

    }
}