using ProsysMobile.Models.CommonModels.SQLiteModels.Base;

namespace ProsysMobile.Models.CommonModels.SQLiteModels
{
    public class USERMOBILE : Entity
    {
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public string COMPANYCODE { get; set; }
        public string PHONE { get; set; }
        public bool ISAPPROVE { get; set; }

    }
}