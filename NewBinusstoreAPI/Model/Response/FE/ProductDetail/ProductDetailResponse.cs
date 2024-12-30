using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class ProductDetailResponse
    {
        public string ID { get; set; }
        public string ProductSKU { get; set; }
        public string Name { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal TotalSale { get; set; }
        public Int32 Stock { get; set; }
        public string Description { get; set; }
        public string ProductCategory { get; set; }
        public string MerchantName { get; set; }
        public string MediaUrl { get; set; }
        public string YTEmbed { get; set; }





    }
}
