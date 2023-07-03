using System.Windows.Input;

namespace WiseMobile.Models.CommonModels
{
    public class NavigationModel<T>
    {
        public T Model { get; set; }
        public ICommand ClosedPageEventCommand { get; set; }
        public ICommand ClosedMainPageEventCommand { get; set; }
    }
}