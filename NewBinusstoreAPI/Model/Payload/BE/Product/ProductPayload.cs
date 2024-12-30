using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class ProductPayload
    {
        public string ID { get; set; }
        public string Action { get; set; }


        public string ProductSKU { get; set; }
        public string MerchantID { get; set; }
        public string ProductName { get; set; }
        public string ProductCategoryID { get; set; }
        public string MainPhoto { get; set; }
        public string[] VariantOptiontID { get; set; }
        public string[] VariantID { get; set; }
        public decimal[] AdditionalPrice { get; set; }
        public string[] ProductPhotoUrl { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }


    }
}
