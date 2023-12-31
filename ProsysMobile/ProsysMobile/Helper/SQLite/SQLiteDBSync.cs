﻿using ProsysMobile.Models.CommonModels.SQLiteModels;

namespace ProsysMobile.Helper.SQLite
{
    public class SQLiteDBSync : BaseSync
    {
        public SQLiteDBSync()
        {
            SQLiteCreateTable();
        }

        public void SQLiteCreateTable()
        {
            //Comment alınanlar değerlendirilip açılacak ya da silinecek - açılacak olanlar için service,endpoint vs. yazılacak
            Database.SQLConnection.CreateTable<DefaultSettings>();
            Database.SQLConnection.CreateTable<USERMOBILE>();
        }
        public void SQLiteDropTable()
        {
            Database.SQLConnection.DropTable<USERMOBILE>();
            Database.SQLConnection.DropTable<DefaultSettings>();
        }

    }
}
