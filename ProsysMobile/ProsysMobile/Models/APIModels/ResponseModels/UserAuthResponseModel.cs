using System;
using System.Collections.Generic;
using System.Text;
using WiseMobile.Models.CommonModels.SQLiteModels;

namespace WiseMobile.Models.APIModels.ResponseModels
{
    public class UserAuthResponseModel
    {
        public string Token { get; set; }
        public User usersDto { get; set; }
    }
}
