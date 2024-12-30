using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class UserManagementFormResponse
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserType { get; set; }
        public string GroupRole { get; set; }
        public bool IsActive { get; set; }


    }
}
