using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Binus.WS.Pattern.Model;

namespace NewBinusstoreAPI.Model
{
    public class UserManagementPayload
    {
        public string ID { get; set; }
        public string Action { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string RoleID { get; set; }
        public string[] MerchantID { get; set; }
        public bool IsActive { get; set; }




    }
}
