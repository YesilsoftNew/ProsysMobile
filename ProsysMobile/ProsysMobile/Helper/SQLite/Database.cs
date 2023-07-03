using SQLite;
using System;
using System.IO;

namespace ProsysMobile.Helper.SQLite
{
    public static class Database
    {
        public static string DBName = "ProsysMobile.Prosys20230701";
        public static SQLiteConnection SQLConnection = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DBName));
    }
}
