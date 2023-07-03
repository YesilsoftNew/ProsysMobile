using WiseMobile.Helper;
using WiseMobile.Models.CommonModels.Enums;

namespace WiseMobile.Models.APIModels.RequestModels
{
    public class ApiFilterRequestModel
    {
        public ApiFilterRequestModel()
        {
            Filter = string.Empty;
            Sort = string.Empty;

            Page = 1;
            PageSize = 6000;
            ShowDeleted = false;
            Lng = "TR";
        }

        public long? ModifiedDate { get; set; }
        public string CustomGuid { get; set; }
        public long UserId { get; set; }
        public string Lng { get; set; }
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool ShowDeleted { get; set; }
    }
}
