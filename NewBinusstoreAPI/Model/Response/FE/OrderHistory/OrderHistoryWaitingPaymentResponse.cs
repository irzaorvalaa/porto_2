using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class OrderHistoryWaitingPaymentResponse
    {
        public string ID { get; set; }
        public string RecipientName { get; set; }
        public string OrderNo { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string VANumber { get; set; }
        public decimal TotalPayment { get; set; }
        public string Status { get; set; }
        public string PaymentMethodName { get; set; }
        public string PaymentMediaUrl { get; set; }




    }
}
