using System;
using System.Collections.Generic;
using System.Linq;
using ProsysMobile.Helper;
using ProsysMobile.Helper.SQLite;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.SQLite
{
    public class UserSQLiteService : SqliteBaseHelper, IUserSQLiteService
    {
        public int DeleteUser(USERMOBILE u)
        {
            return Database.SQLConnection.Delete(u);
        }

        public USERMOBILE GetUser(long? userId)
        {
            sSQL.Clear();

            sSQL.Append(" SELECT * FROM User ");
            sSQL.Append(" where Id=" + userId);

            var user = Database.SQLConnection.Query<USERMOBILE>(sSQL.ToString()).FirstOrDefault();

            if (user == null)
                return null;

            return user;
        }

        public int UpdateUser(USERMOBILE u)
        {
            return Database.SQLConnection.Update(u);
        }
    }
}