using SQLite;
using System;
using System.Text;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels.Enums;

namespace ProsysMobile.Models.CommonModels.SQLiteModels.Base
{
    public class Entity
    {
        [PrimaryKey]
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public string ActionUser { get; set; } = "Mobile";
        public bool IsDeleted { get; set; } 
        public long CreatedDate { get; set; }
        public long? ModifiedDate { get; set; }
        public DateTime RecordDate { get; set; } = DateTime.Now;
        public int isSync { get; set; } = (int)enIsSync.Waiting;
        public long? UserId { get; set; } 

        [Ignore]
        public StringBuilder sSQL { get; set; } = new StringBuilder();
    }
}