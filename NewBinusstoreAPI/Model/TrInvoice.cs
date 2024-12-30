using Binus.WS.Pattern.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBinusstoreAPI.Model
{
    [DatabaseName("DB")]
    [Table("TrInvoice")]
    public class TrInvoice : BaseModel
    {
        [Key]
        public string ID { get; set; }
        public string CustomerID { get; set; }
        public string InvoiceNo { get; set; }
        public string PaymentMethodName { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal DiscountedSubtotal { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal TotalPaid { get; set; }
        public string CCNumber { get; set; }
        public string VANumber { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime PaidDate { get; set; }
        public string Status { get; set; }


        public string Stsrc { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
    }
}

