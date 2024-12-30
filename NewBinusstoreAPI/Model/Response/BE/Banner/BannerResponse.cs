using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class BannerResponse
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string BannerType { get; set; }
        public DateTime EffStartDate { get; set; }
        public DateTime EffEndDate { get; set; }
        public string TargetUrl { get; set; }
        public bool IsActive { get; set; }


    }
}
