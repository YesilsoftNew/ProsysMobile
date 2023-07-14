using System;
using ProsysMobile.Models.CommonModels.SQLiteModels.Base;
using SQLite;

namespace ProsysMobile.Models.CommonModels.SQLiteModels
{
    public class DefaultSettings
    {
        [PrimaryKey]
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
