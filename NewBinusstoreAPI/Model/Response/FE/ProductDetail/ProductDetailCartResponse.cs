using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class ProductDetailCartResponse
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal Subtotal { get; set; }
        public Int32 Qty { get; set; }


    }
}
