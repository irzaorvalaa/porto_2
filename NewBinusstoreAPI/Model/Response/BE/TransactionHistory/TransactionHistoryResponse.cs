using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class TransactionHistoryResponse
    {
        public string ID { get; set; }
        public string OrderNo { get; set; }
        public string Notes { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public string TrackingNo { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string InvoiceNo { get; set; }
        public string PaymentMethodName { get; set; }
        public string PaymentStatus { get; set; }
        public decimal TotalPayment { get; set; }
        public string Name { get; set; }
        public string OrderStatus { get; set; }
        public string SchoolID { get; set; }
        public string SchoolName { get; set; }
        public string ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public string CustomerType { get; set; }
        public string CustomerName { get; set; }

    }
}
