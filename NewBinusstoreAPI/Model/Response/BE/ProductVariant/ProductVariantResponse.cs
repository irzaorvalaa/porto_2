using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class ProductVariantResponse
    {
        public string ID { get; set; }
        public string VariantName { get; set; }
        public string VariantOptionName { get; set; }
        public string ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public bool IsActive { get; set; }


    }
}
