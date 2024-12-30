using Binus.WS.Pattern.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBinusstoreAPI.Model
{
    [DatabaseName("DB")]
    [Table("MsProductVariant")]
    public class MsProductVariant : BaseModel
    {
        [Key]
        public string ID { get; set; }
        public string ProductID { get; set; }
        public string VariantID { get; set; }
        public string VariantOptionID { get; set; }
        public decimal AdditionalPrice { get; set; }
        public string ProductPhotoUrl { get; set; }
        public string ProductPhotoMimeType { get; set; }
        public bool IsActive { get; set; }

        public string Stsrc { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
    }
}

