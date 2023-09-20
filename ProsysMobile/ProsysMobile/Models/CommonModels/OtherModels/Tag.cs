using Xamarin.Forms;

namespace ProsysMobile.Models.CommonModels.OtherModels
{
    public class Tag
    {
        public string Name { get; set; }
        public string ColorStr { get; set; }
        public Color Color => Color.FromHex(ColorStr);
    }
}