using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class ValidTokenModel
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AccessID { get; set; }
        public string ExternalID { get; set; }
        public string IsExternal { get; set; }
    }
}
