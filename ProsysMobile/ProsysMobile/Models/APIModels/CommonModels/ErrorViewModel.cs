using System;
using System.Collections.Generic;
using System.Text;

namespace ProsysMobile.Models.APIModels.CommonModels
{
    public class ErrorViewModel
    {
        public string DefinedErrorCategory { get; set; }
        public string DefinedErrorDtoName { get; set; }
        public string DefinedErrorDetail { get; set; }
        public Exception Error { get; set; }
    }
}
