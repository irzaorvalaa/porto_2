using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Binus.WS.Pattern.Model;

namespace NewBinusstoreAPI.Model
{
    public class ProductVariantPayload
    {
        public string ID { get; set; }
        public string Action { get; set; }

        public string Name { get; set; }
        public string VariantOption { get; set; }
        public string ProductCategoryID { get; set; }
        public string[] ProductCategoryName { get; set; }
        public bool IsActive { get; set; }




    }
}
