using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class CartResponse
    {
        public string ID { get; set; }
        public string ProductID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Qty { get; set; }
        public string ProductCategoryID { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsReligion { get; set; }
        public bool IsContinuity { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal FinalPrice { get; set; }


    }
}
