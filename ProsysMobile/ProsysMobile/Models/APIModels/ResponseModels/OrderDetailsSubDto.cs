using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class OrderDetailsSubDto : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public int Id { get; set; }
        public int OrderDetailId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Amount { get; set; }
        public string UnitPrice { get; set; } = string.Empty;
        public string CurrencyType { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal MaxOrderCount { get; set; }
        
        private string _stockCount = string.Empty;
        public string StockCount { get => _stockCount; set { _stockCount = value; OnPropertyChanged(); } }
        
        private int _stockCountInt;
        public int StockCountInt { get => _stockCountInt; set { _stockCountInt = value; OnPropertyChanged(); } }
        
        private string _price;
        public string Price { get => _price; set { _price = value; OnPropertyChanged(); } }
    }
}