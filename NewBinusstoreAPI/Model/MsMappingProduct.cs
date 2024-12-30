using Binus.WS.Pattern.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBinusstoreAPI.Model
{
    [DatabaseName("DB")]
    [Table("MsMappingProduct")]
    public class MsMappingProduct : BaseModel
    {
        [Key]
        public string ID { get; set; }
        public string MerchantID { get; set; }
        public string StreamingID { get; set; }
        public string ProductCategoryID { get; set; }
        public string Gender { get; set; }
        public string Grade { get; set; }
        public string Level { get; set; }
        public bool IsContinuity { get; set; }



        public string Stsrc { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
    }
}

