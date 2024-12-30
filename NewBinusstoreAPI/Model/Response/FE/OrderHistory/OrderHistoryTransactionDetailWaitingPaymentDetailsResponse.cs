using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class OrderHistoryTransactionDetailWaitingPaymentDetailsResponse
    {
        public string ID { get; set; }
        public string Status { get; set; }
        public string VANumber { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountedSubtotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TotalPayment { get; set; }




    }
}
