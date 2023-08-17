﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProsysMobile.Models.CommonModels.SQLiteModels.Base;

namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class ItemCategory : Entity
    {
        public string CategoryDesc { get; set; }
        public int? MainCategoryId { get; set; }
        public string Image { get; set; }
        public bool? RecordState { get; set; }
        
        private bool _isNotSelected = true;
        public bool IsNotSelected { get => _isNotSelected; set { _isNotSelected = value; OnPropertyChanged(); } }
        
        private bool _isSelected;
        public bool IsSelected { get => _isSelected; set { _isSelected = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }

}
