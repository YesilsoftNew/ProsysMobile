using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.SQLite
{
    public interface IUserSQLiteService : ISQLiteBase<USERMOBILE>
    {
        USERMOBILE GetUser(long? userId);
        int DeleteUser(USERMOBILE u);
        int UpdateUser(USERMOBILE u);

    }
}