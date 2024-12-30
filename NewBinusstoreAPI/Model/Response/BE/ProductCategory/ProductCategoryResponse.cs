using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class ProductCategoryResponse
    {
        public string ID { get; set; }
        public string UpperCategoryID { get; set; }
        public string Name { get; set; }
        public bool IsBTSPEriod { get; set; }
        public bool IsActive { get; set; }


    }
}
