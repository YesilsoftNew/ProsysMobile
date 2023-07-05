using System;
using ProsysMobile.Models.CommonModels.SQLiteModels.Base;

namespace ProsysMobile.Models.CommonModels.SQLiteModels
{
    public class DefaultSettings : Entity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
