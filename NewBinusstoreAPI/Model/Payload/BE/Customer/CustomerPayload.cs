using System;
using System.Collections.Generic;

namespace NewBinusstoreAPI.Model
{
    public class CustomerPayload
    {
        public string ID { get; set; }
        public string Action { get; set; }


        public string Name { get; set; }
        public string CustomerType { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public string[] StudentID { get; set; }



    }
}
