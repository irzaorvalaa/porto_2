using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class AuthResponse
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ExternalID { get; set; }
        public string IsExternal { get; set; }
        public string IsAdmin { get; set; }
        public List<AuthMenu> AuthMenu { get; set; }
    }

    public class AuthMenu
    {
        public string MenuUrl { get; set; }
        public string Name { get; set; }
        public int SequenceNo { get; set; }
        public string IsParent { get; set; }
    }
}
