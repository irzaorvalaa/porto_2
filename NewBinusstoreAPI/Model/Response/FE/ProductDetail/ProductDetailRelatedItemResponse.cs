using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class ProductDetailRelatedItemResponse
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ProductSKU { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal TotalSale { get; set; }
        public string ProductCategory { get; set; }
        public string MerchantName { get; set; }




    }
}
