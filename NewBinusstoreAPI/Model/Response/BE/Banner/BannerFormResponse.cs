using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class BannerFormResponse
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string BannerType { get; set; }
        public string BannerImgUrl { get; set; }
        public string BannerMimeType { get; set; }
        public string TargetUrl { get; set; }
        public DateTime EffStartDate { get; set; }
        public DateTime EffEndDate { get; set; }
        public Int32 SequenceNo { get; set; }
        public bool IsBtsPeriod { get; set; }
        public bool IsActive { get; set; }


    }
}
