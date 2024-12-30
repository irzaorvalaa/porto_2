using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class CheckoutPaymentSummaryResponse
    {
        public string ID { get; set; }
        public string Subtotal { get; set; }
        public string DiscountedSubtotal { get; set; }
        public string DeliveryFee { get; set; }
        public string TotalPayment { get; set; }



}
}
