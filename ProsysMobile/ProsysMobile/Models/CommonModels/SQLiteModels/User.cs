using ProsysMobile.Models.CommonModels.SQLiteModels.Base;

namespace ProsysMobile.Models.CommonModels.SQLiteModels
{
    public class User : Entity
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public string CustomerGuid { get; set; }
        public string FleetGuid { get; set; }
        public string FleetBranchGuid { get; set; }
        public long? RoleId { get; set; }
        public bool IsTMS { get; set; }
        public string Photo { get; set; }
        public short UserState { get; set; }
        public string ServiceProviderDefinitionGuid { get; set; }

    }
}