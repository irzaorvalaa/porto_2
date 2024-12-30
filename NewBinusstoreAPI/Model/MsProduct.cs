using Binus.WS.Pattern.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBinusstoreAPI.Model
{
    [DatabaseName("DB")]
    [Table("MsProduct")]
    public class MsProduct : BaseModel
    {
        [Key]
        public string ID { get; set; }
        public string VendorID { get; set; }
        public string ProductCategoryID { get; set; }
        public string MerchantID { get; set; }
        public string ProductSKU { get; set; }
        public string Name { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal Price { get; set; }
        public Int32 TotalSale { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsReligion { get; set; }
        public Int32 Stock { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }


        public string Stsrc { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
    }
}

