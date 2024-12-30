using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class ProductSearchCategoryResponse
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ProductSKU { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public string ProductPhoto { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public Int32 TotalSale { get; set; }
        public string ProductCategory { get; set; }
        public string MerchantID { get; set; }
        public string MerchantName { get; set; }


    }
}
