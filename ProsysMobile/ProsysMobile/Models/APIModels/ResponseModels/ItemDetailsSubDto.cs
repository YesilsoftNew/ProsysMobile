using System.Collections.Generic;
using ProsysMobile.Models.CommonModels.OtherModels;

namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class ItemDetailsSubDto
    {
        public ItemsSubDto Item { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
