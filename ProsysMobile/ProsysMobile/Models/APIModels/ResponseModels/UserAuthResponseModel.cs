using System;
using System.Collections.Generic;
using System.Text;
using ProsysMobile.Models.CommonModels.SQLiteModels;

namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class UserAuthResponseModel
    {
        public string Token { get; set; }
        public User usersDto { get; set; }
    }
}
