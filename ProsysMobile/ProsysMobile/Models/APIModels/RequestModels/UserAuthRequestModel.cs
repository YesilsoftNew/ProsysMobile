using System;
using System.Collections.Generic;
using System.Text;

namespace ProsysMobile.Models.APIModels.RequestModels
{
    public  class UserAuthRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DeviceGuid { get; set; }
        public int UserType { get; set; }
        public string Token { get; set; }
    }
}
