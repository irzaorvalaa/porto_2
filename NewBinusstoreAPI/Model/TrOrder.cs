using Binus.WS.Pattern.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBinusstoreAPI.Model
{
    [DatabaseName("DB")]
    [Table("TrOrder")]
    public class TrOrder : BaseModel
    {
        [Key]
        public string ID { get; set; }
        public string CustomerID { get; set; }
        public string ShippingServiceID { get; set; }
        public string BTSPeriodID { get; set; }
        public string DeliveryMethodID { get; set; }
        public string ProductCategoryID { get; set; }
        public string PaymentID { get; set; }
        public string InvoiceID { get; set; }
        public string OrderNo { get; set; }
        public string Status { get; set; }
        public string TrackingNo { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal DiscountedSubtotal { get; set; }
        public string RecipientName { get; set; }
        public string Receiver { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public DateTime ScheduleDelivery { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime ActualDeliveryDate { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string Institution { get; set; }
        public bool IsSchool { get; set; }
        public DateTime PaidDate { get; set; }
        public string ProductSKU { get; set; }
        public string Notes { get; set; }

        public string Stsrc { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
    }
}

