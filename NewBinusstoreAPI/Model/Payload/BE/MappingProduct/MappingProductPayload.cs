using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class MappingProductPayload
    {
        public string ID { get; set; }
        public string Action { get; set; }



        public string SchoolID { get; set; }
        public string[] Gender { get; set; }
        public string Grade { get; set; }
        public string StreamingID { get; set; }
        public string[] ProductCategoryID { get; set; }
        public string[] ProductSKU { get; set; }



    }
}
