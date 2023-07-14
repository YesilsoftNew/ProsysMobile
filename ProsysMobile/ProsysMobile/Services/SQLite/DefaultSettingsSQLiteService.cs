using System;
using System.Collections.Generic;
using ProsysMobile.Helper;
using ProsysMobile.Helper.SQLite;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.SQLite
{
    public class DefaultSettingsSQLiteService : SqliteBaseHelper, IDefaultSettingsSQLiteService
    {
        public DefaultSettings getSettings(string key) //TODO nullable
        {
            return Database.SQLConnection.Table<DefaultSettings>().FirstOrDefault(t => t.Key == key);
        }

        public List<DefaultSettings> getSettingsAll()
        {
            sSQL.Clear();

            sSQL.Append(" SELECT * FROM DefaultSettings ");

            return Database.SQLConnection.Query<DefaultSettings>(sSQL.ToString());
        }

        public void Delete(DefaultSettings defaultSettings)
        {
            Database.SQLConnection.Delete(defaultSettings);
        }

        public void SaveAll(List<DefaultSettings> defaultSettings)
        {
            try
            {
                Database.SQLConnection.InsertAll(defaultSettings, "OR REPLACE");
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        public void Save(DefaultSettings defaultSetting)
        {
            try
            {
                Database.SQLConnection.Insert(defaultSetting, "OR REPLACE");
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);

            }
        }

        public long? getModifiedDateMax(string modelName)
        {
            return getBaseModifiedDateMax(modelName);
        }
    }
}