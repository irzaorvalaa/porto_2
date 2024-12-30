using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class ProductResponse
    {
        public string ID { get; set; }
        public string ProductSKU { get; set; }
        public string MerchantName { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public Int32 Qty { get; set; }
        public decimal Price { get; set; }


    }
}
