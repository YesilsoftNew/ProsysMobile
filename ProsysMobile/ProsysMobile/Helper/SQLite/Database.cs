using SQLite;
using System;
using System.IO;

namespace ProsysMobile.Helper.SQLite
{
    public static class Database
    {
        public static string DBName = "WiseDynamicMobile.Wise20210207";
        public static SQLiteConnection SQLConnection = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DBName));
    }
}
