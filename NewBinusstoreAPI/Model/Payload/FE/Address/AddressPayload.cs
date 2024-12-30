using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Binus.WS.Pattern.Model;

namespace NewBinusstoreAPI.Model
{
    public class AddressPayload
    {
        public string ID { get; set; }
        public string Action { get; set; }

        public string RecipientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string StateID { get; set; }
        public string CityID { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }


    }
}
