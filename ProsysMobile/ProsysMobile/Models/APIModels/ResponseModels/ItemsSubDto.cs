using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class ItemsSubDto : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private bool _isFavorite;
        public bool IsFavorite { get => _isFavorite; set { _isFavorite = value; OnPropertyChanged(); } }
        
        public int Id { get; set;}
        public int CategoryId { get; set;}
        public string Name { get; set; }
        [JsonPropertyName("count")]
        public string Pieces { get; set; }
        public string Price { get; set; }
        public string CurrencyType { get; set; }
        public string Image { get; set; }
        public List<string> Images { get; set; }
        public string Amount { get; set; } = string.Empty;
        public string UnitDesc { get; set; } = string.Empty;
        public string Tag1Text { get; set; } = string.Empty;
        public string Tag2Text { get; set; } = string.Empty;
        public string Tag3Text { get; set; } = string.Empty;
        public string Tag4Text { get; set; } = string.Empty;
        public string Tag1ColorStr { get; set; }
        public string Tag2ColorStr { get; set; }
        public string Tag3ColorStr { get; set; }
        public string Tag4ColorStr { get; set; }

        public Color Tag1Color => Color.FromHex(Tag1ColorStr);
        public Color Tag2Color => Color.FromHex(Tag2ColorStr);
        public Color Tag3Color => Color.FromHex(Tag3ColorStr);
        public Color Tag4Color => Color.FromHex(Tag4ColorStr);
    }
}
