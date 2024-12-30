using Binus.WS.Pattern.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBinusstoreAPI.Model
{
    [DatabaseName("DB")]
    [Table("MsBanner")]
    public class MsBanner : BaseModel
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string BannerImgUrl { get; set; }
        public string BannerMimeType { get; set; }
        public string BannerType { get; set; }
        public string TargetUrl { get; set; }
        public DateTime EffStartDate { get; set; }
        public DateTime EffEndDate { get; set; }
        public Int32 SequenceNo { get; set; }
        public bool IsBTSPeriod { get; set; }
        public bool IsActive { get; set; }




        public string Stsrc { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
    }
}

