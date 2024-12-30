using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Binus.WS.Pattern.Model;

namespace NewBinusstoreAPI.Model
{
    public class CartPayload
    {
        public string ID { get; set; }
        public string Action { get; set; }

        public string ProductID { get; set; }
        public string CustomerID { get; set; }
        public Int32 Qty { get; set; }



    }
}
