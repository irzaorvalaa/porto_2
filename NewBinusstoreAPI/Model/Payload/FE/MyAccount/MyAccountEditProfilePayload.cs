using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Binus.WS.Pattern.Model;

namespace NewBinusstoreAPI.Model
{
    public class MyAccountEditProfilePayload
    {
        public string ID { get; set; }
        public string Action { get; set; }

        public string Name { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

    }
}
