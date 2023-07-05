using System.Windows.Input;

namespace ProsysMobile.Models.CommonModels
{
    public class NavigationModel<T>
    {
        public T Model { get; set; }
        public ICommand ClosedPageEventCommand { get; set; }
        public ICommand ClosedMainPageEventCommand { get; set; }
    }
}