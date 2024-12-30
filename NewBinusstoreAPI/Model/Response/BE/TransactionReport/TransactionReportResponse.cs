using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class TransactionReportResponse
    {
        public string ID { get; set; }
        public string OrderNo { get; set; }
        public string CustomerType { get; set; }
        public string Institution { get; set; }
        public string Username { get; set; }
        public string RecipientName { get; set; }
        public string DeliveryMethod { get; set; }
        public string TrackingNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime ScheduleDelivery { get; set; }
        public DateTime ActualDeliveryDate { get; set; }
        public decimal Weight { get; set; }
        public string InvoiceNo { get; set; }
        public string PaymentMethodName { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string Notes { get; set; }
        public string Name { get; set; }
        public string ProductSKU { get; set; }
        public string ProductName { get; set; }
        public Int32 Qty { get; set; }
        public decimal UnitWeight { get; set; }
        public decimal TotalPayment { get; set; }
        public string OrderStatus { get; set; }
        public string ProductCategoryName { get; set; }
    }
}
