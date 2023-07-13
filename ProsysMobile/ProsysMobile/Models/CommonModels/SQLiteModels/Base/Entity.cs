using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProsysMobile.Models.CommonModels.SQLiteModels.Base
{
    public class Entity
    {
        [PrimaryKey]
        public int ID { get; set; }
    }
}