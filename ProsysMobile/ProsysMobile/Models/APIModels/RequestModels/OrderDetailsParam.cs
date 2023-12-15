using ProsysMobile.Resources.Language;

namespace ProsysMobile.Models.APIModels.RequestModels
{
    public class OrderDetailsParam
    {
        public int UserId { get; set;}
        public int ItemId { get; set;}
        public int Amount { get; set;}
        public string Culture { get; set; } = Resource.Language;
    }
}