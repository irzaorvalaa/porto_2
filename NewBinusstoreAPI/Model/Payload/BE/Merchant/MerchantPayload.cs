using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Binus.WS.Pattern.Model;

namespace NewBinusstoreAPI.Model
{
    public class MerchantPayload
    {
        public string ID { get; set; }
        public string Action { get; set; }

        public string Name { get; set; }
        public string MerchantType { get; set; }
        public bool IsActive { get; set; }




    }
}
