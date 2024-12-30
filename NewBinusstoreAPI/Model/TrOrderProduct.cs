using Binus.WS.Pattern.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBinusstoreAPI.Model
{
    [DatabaseName("DB")]
    [Table("TrOrderProduct")]
    public class TrOrderProduct : BaseModel
    {
        [Key]
        public string ID { get; set; }
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string MerchantID { get; set; }
        public string MerchantName { get; set; }
        public string ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountedUnitPrice { get; set; }
        public Int32 Qty { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsReligion { get; set; }
        public bool IsContinuity { get; set; }
        public string VariantNotes { get; set; }
        public decimal UnitWeight { get; set; }
        public string ProductSKU { get; set; }


        public string Stsrc { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
    }
}

