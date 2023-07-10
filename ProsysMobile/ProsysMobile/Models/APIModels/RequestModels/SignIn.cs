using System;

namespace ProsysMobile.Models.APIModels.RequestModels
{
    public  class SignIn
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid? DeviceGuid { get; set; }
        public string Token { get; set; }
    }
}
