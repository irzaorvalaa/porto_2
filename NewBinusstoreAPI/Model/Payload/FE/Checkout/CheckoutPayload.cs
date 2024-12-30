using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Binus.WS.Pattern.Model;

namespace NewBinusstoreAPI.Model
{
    public class CheckoutPayload
    {
        public string ID { get; set; }
        public string Action { get; set; }

        public string AddressID { get; set; }
        public string DeliveryType { get; set; }
        public string DeliveryMethod { get; set; }




    }
}
