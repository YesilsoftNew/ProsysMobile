using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class ItemsSubDto
    {
        public int Id { get; set;}
        public int CategoryId { get; set;}
        public string Name { get; set; }
        [JsonPropertyName("count")]
        public string Pieces { get; set; }
        public string Price { get; set; }
        public string CurrencyType { get; set; }
        public string Image { get; set; }
        public string Amount { get; set; } = string.Empty;
        public string UnitDesc { get; set; } = string.Empty;
    }
}
