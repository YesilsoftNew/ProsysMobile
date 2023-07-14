using System;
using System.Collections.Generic;
using System.Text;
using ProsysMobile.Helper.SQLite;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.SQLite
{
    public interface IDefaultSettingsSQLiteService : ISQLiteBase<DefaultSettings>
    {
        void SaveAll(List<DefaultSettings> defaultSettings);
        void Save(DefaultSettings defaultSettings);

        DefaultSettings getSettings(string key);
        List<DefaultSettings> getSettingsAll();

        void Delete(DefaultSettings defaultSettings);
        long? getModifiedDateMax(string modelName);
    }
}