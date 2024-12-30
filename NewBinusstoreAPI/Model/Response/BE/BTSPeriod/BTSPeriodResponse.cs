using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class BTSPeriodResponse
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime EffStartDate { get; set; }
        public DateTime EffEndDate { get; set; }
        public DateTime DeliveryStartDate { get; set; }
        public DateTime DeliveryEndDate { get; set; }
        public bool IsActive { get; set; }

    }
}
