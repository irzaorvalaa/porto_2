using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class OrderHistoryTransactionDetailDeliveryPaymentResponse
    {
        public string ID { get; set; }
        public string TrackingNo { get; set; }
        public string DeliveryAddress { get; set; }
        public string CourierName { get; set; }
        public string PaymentMethodName { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountedSubtotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TotalPayment { get; set; }




    }
}
