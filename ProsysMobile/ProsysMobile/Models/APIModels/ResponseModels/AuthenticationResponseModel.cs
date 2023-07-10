using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.CommonModels.SQLiteModels;

namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class AuthenticationResponseModel
    {
        public SignIn SignIn { get; set; }
        public USERMOBILE UserMobile { get; set; }
    }
}
