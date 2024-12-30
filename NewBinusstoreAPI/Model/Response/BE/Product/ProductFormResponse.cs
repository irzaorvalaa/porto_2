using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class ProductFormResponse
    {
        public string ID { get; set; }
        public string VariantName { get; set; }
        public decimal AdditionalPrice { get; set; }
        public string ProductPhotoUrl { get; set; }
        public string MediaUrl { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }


    }
}
