using ProsysMobile.Helper;
using ProsysMobile.Helper.SQLite;
using ProsysMobile.Models.CommonModels.Enums;
using System;
using System.Text;

namespace ProsysMobile.Services.Base
{
    public class SqliteBaseHelper
    {
        public static StringBuilder sSQL = new StringBuilder();

        public bool setIsSync(string modelName, string guid, enIsSync isSync)
        {
            sSQL.Clear();

            try
            {
                sSQL.Append($"UPDATE {modelName} SET isSync={(int)isSync} where Guid='{guid}' COLLATE NOCASE ");

                int result = Database.SQLConnection.Execute(sSQL.ToString());

                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            { 
                ProsysLogger.Instance.CrashLog(ex);
                return false;
            }
        }

        public long? getBaseModifiedDateMax(string modelName)
        {
            sSQL.Clear();

            try
            {
                sSQL.Append($"select max(ModifiedDate) from {modelName} ");

                string Retval = Database.SQLConnection.ExecuteScalar<string>(sSQL.ToString());

                if (string.IsNullOrWhiteSpace(TOOLS.ToString(Retval)))
                    return 0;
                else
                    return TOOLS.ToLong(Retval); 
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
                return 0;
            }
        }
    }
}