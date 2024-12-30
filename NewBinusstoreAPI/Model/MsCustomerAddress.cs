using Binus.WS.Pattern.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBinusstoreAPI.Model
{
    [DatabaseName("DB")]
    [Table("MsCustomerAddress")]
    public class MsCustomerAddress : BaseModel
    {
        [Key]
        public string ID { get; set; }
        public string StateID { get; set; }
        public string CityID { get; set; }
        public string CustomerID { get; set; }
        public string RecipientName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Honorable { get; set; }
        public string PostalCode { get; set; }
        public bool IsPrimary { get; set; }


        public string Stsrc { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
    }
}

