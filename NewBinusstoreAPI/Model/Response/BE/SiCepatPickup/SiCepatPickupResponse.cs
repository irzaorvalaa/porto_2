using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class SiCepatPickupResponse
    {
        public string ID { get; set; }
        public string OrderNo { get; set; }
        public DateTime DateIn { get; set; }
        public string ProductName { get; set; }
        public string RecipientName { get; set; }
        public string DeliveryAddress { get; set; }
        public string TrackingNo { get; set; }
        public string Status { get; set; }

    }
}
