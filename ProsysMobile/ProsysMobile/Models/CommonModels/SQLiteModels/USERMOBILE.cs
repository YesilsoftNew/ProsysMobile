﻿using System.Text.Json.Serialization;
using ProsysMobile.Models.CommonModels.SQLiteModels.Base;

namespace ProsysMobile.Models.CommonModels.SQLiteModels
{
    public class USERMOBILE : Entity
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyCode { get; set; }
        public string Phone { get; set; }
        public bool IsApprove { get; set; }
        public int? CustomerId { get; set; }
        [JsonPropertyName("CustomerDesc")]
        public string CustomerName { get; set; }

    }
}